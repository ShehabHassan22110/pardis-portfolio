using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;
using BardeesCms.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BardeesCms.Web.Services;

/// <summary>Read-optimized queries for the public site, lightly cached in memory.</summary>
public interface IPublicContentService
{
    Task<LayoutData> GetLayoutAsync();
    Task<HomeViewModel> GetHomeAsync();
    Task<WorkIndexViewModel> GetWorkAsync(string? discipline);
    Task<ProjectDetailViewModel?> GetProjectAsync(string slug);
    Task<ServicesViewModel> GetServicesAsync();
    Task<Service?> GetServiceAsync(string slug);
    Task<VideosViewModel> GetVideosAsync();
    Task<AbayasViewModel> GetAbayasAsync();
    Task<ContactViewModel> GetContactAsync();
    Task<SeoPage?> GetSeoAsync(string route);
    void Invalidate();
}

public class PublicContentService : IPublicContentService
{
    private readonly ApplicationDbContext _db;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(5);

    public PublicContentService(ApplicationDbContext db, IMemoryCache cache) { _db = db; _cache = cache; }

    private Task<T> Cached<T>(string key, Func<Task<T>> factory) =>
        _cache.GetOrCreateAsync(key, e => { e.AbsoluteExpirationRelativeToNow = Ttl; return factory(); })!;

    public void Invalidate()
    {
        // NB: "contact" is intentionally NOT cached — GetContactAsync returns a VM whose Form is
        // mutated per request by ContactController, so a shared cached instance would leak form state.
        foreach (var k in new[] { "layout", "home", "services", "videos", "abayas" })
            _cache.Remove(k);
    }

    public Task<LayoutData> GetLayoutAsync() => Cached("layout", async () =>
    {
        var nav = await _db.NavigationItems.AsNoTracking().Where(n => n.IsActive)
            .OrderBy(n => n.DisplayOrder).ToListAsync();
        return new LayoutData
        {
            Settings = await _db.SiteSettings.AsNoTracking().FirstOrDefaultAsync() ?? new SiteSettings(),
            HeaderNav = nav.Where(n => n.Location == NavLocation.Header).ToList(),
            FooterNav = nav.Where(n => n.Location == NavLocation.Footer)
                .GroupBy(n => n.Group ?? "").ToList()
        };
    });

    public Task<HomeViewModel> GetHomeAsync() => Cached("home", async () =>
    {
        var hero = await _db.HeroSections.AsNoTracking().Include(h => h.Slides)
            .FirstOrDefaultAsync(h => h.IsActive);
        var about = await _db.AboutSections.AsNoTracking().Include(a => a.Facts)
            .FirstOrDefaultAsync(a => a.IsActive);
        var sections = await _db.PageSections.AsNoTracking().Where(s => s.IsActive).ToListAsync();

        return new HomeViewModel
        {
            Hero = hero,
            HeroSlides = hero?.Slides.Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToList() ?? new(),
            TrustedBrands = await _db.TrustedBrands.AsNoTracking().Where(b => b.IsActive).OrderBy(b => b.DisplayOrder).ToListAsync(),
            About = about,
            AboutFacts = about?.Facts.Where(f => f.IsActive).OrderBy(f => f.DisplayOrder).ToList() ?? new(),
            Clients = await _db.Brands.AsNoTracking().Where(b => b.IsActive).OrderBy(b => b.DisplayOrder).ToListAsync(),
            MarketPositions = await _db.MarketPositions.AsNoTracking().Where(m => m.IsActive).OrderBy(m => m.DisplayOrder).ToListAsync(),
            Disciplines = await _db.Disciplines.AsNoTracking().Include(d => d.SubItems).Where(d => d.IsActive).OrderBy(d => d.DisplayOrder).ToListAsync(),
            FeaturedWork = await _db.PortfolioProjects.AsNoTracking().Include(p => p.Discipline)
                .Where(p => p.IsPublished).OrderByDescending(p => p.IsFeatured).ThenBy(p => p.DisplayOrder).ThenBy(p => p.Id).Take(8).ToListAsync(),
            Showreel = await _db.Videos.AsNoTracking().Include(v => v.VideoCategory)
                .Where(v => v.IsPublished).OrderByDescending(v => v.IsFeatured).ThenBy(v => v.DisplayOrder).ThenBy(v => v.Id).Take(3).ToListAsync(),
            Services = await _db.Services.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync(),
            Steps = await _db.CollaborationSteps.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync(),
            Faqs = await _db.Faqs.AsNoTracking().Where(f => f.IsActive).OrderBy(f => f.DisplayOrder).ToListAsync(),
            Abayas = await _db.Abayas.AsNoTracking().Where(a => a.IsActive)
                .OrderByDescending(a => a.IsFeatured).ThenBy(a => a.DisplayOrder).ThenBy(a => a.Id).Take(4).ToListAsync(),
            Sections = sections.ToDictionary(s => s.Key, s => s)
        };
    });

    public async Task<WorkIndexViewModel> GetWorkAsync(string? discipline)
    {
        var disciplines = await _db.Disciplines.AsNoTracking().Where(d => d.IsActive)
            .OrderBy(d => d.DisplayOrder).ToListAsync();
        var q = _db.PortfolioProjects.AsNoTracking().Include(p => p.Discipline)
            .Where(p => p.IsPublished);
        if (!string.IsNullOrWhiteSpace(discipline))
            q = q.Where(p => p.Discipline != null && p.Discipline.Slug == discipline);
        return new WorkIndexViewModel
        {
            Disciplines = disciplines,
            ActiveDiscipline = discipline,
            Projects = await q.OrderBy(p => p.DisplayOrder).ToListAsync()
        };
    }

    public async Task<ProjectDetailViewModel?> GetProjectAsync(string slug)
    {
        var project = await _db.PortfolioProjects.AsNoTracking()
            .Include(p => p.Discipline).Include(p => p.Media)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
        if (project is null) return null;
        var related = await _db.PortfolioProjects.AsNoTracking()
            .Where(p => p.IsPublished && p.Id != project.Id && p.DisciplineId == project.DisciplineId)
            .OrderBy(p => p.DisplayOrder).ThenBy(p => p.Id).Take(4).ToListAsync();
        return new ProjectDetailViewModel { Project = project, Related = related };
    }

    public Task<ServicesViewModel> GetServicesAsync() => Cached("services", async () => new ServicesViewModel
    {
        Services = await _db.Services.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync(),
        Steps = await _db.CollaborationSteps.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync(),
        Faqs = await _db.Faqs.AsNoTracking().Where(f => f.IsActive).OrderBy(f => f.DisplayOrder).ToListAsync(),
        Heading = await _db.PageSections.AsNoTracking().FirstOrDefaultAsync(s => s.Key == "services")
    });

    public async Task<Service?> GetServiceAsync(string slug) =>
        await _db.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Slug == slug && s.IsActive);

    public Task<VideosViewModel> GetVideosAsync() => Cached("videos", async () =>
    {
        var videos = await _db.Videos.AsNoTracking().Include(v => v.VideoCategory)
            .Where(v => v.IsPublished).OrderBy(v => v.DisplayOrder).ToListAsync();
        var cats = await _db.VideoCategories.AsNoTracking().Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder).ToListAsync();
        var settings = await _db.SiteSettings.AsNoTracking().FirstOrDefaultAsync();
        return new VideosViewModel
        {
            Videos = videos,
            Categories = cats.Where(c => videos.Any(v => v.VideoCategoryId == c.Id)).ToList(),
            YouTubeUrl = settings?.YouTubeUrl
        };
    });

    public Task<AbayasViewModel> GetAbayasAsync() => Cached("abayas", async () =>
    {
        var settings = await _db.SiteSettings.AsNoTracking().FirstOrDefaultAsync();
        return new AbayasViewModel
        {
            Abayas = await _db.Abayas.AsNoTracking().Where(a => a.IsActive)
                .OrderBy(a => a.DisplayOrder).ThenBy(a => a.Id).ToListAsync(),
            Heading = await _db.PageSections.AsNoTracking().FirstOrDefaultAsync(s => s.Key == "abaya-page"),
            WhatsApp = settings?.WhatsApp
        };
    });

    public async Task<ContactViewModel> GetContactAsync() => new ContactViewModel
    {
        Settings = await _db.ContactSettings.AsNoTracking().FirstOrDefaultAsync(),
        Disciplines = await _db.Disciplines.AsNoTracking().Where(d => d.IsActive).OrderBy(d => d.DisplayOrder).ToListAsync()
    };

    public async Task<SeoPage?> GetSeoAsync(string route) =>
        await _db.SeoPages.AsNoTracking().FirstOrDefaultAsync(s => s.Route == route && s.IsActive);
}
