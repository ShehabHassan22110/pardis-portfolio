using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;
using BardeesCms.Web.Models.ViewModels;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageContent)]
public class PortfolioProjectsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public PortfolioProjectsController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.PortfolioProjects.AsNoTracking().Include(x => x.Discipline).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Title.Contains(s) || (x.TitleAr != null && x.TitleAr.Contains(s)));
        }
        if (q.Filter == "published") query = query.Where(x => x.IsPublished);
        else if (q.Filter == "draft") query = query.Where(x => !x.IsPublished);
        else if (q.Filter == "featured") query = query.Where(x => x.IsFeatured);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Title),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<PortfolioProject>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create()
    {
        LoadDisciplines();
        return View("Edit", new PortfolioProjectFormVm
        {
            DisplayOrder = (_db.PortfolioProjects.Max(p => (int?)p.DisplayOrder) ?? -1) + 1,
            IsPublished = true
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PortfolioProjectFormVm vm)
    {
        if (!ModelState.IsValid) { LoadDisciplines(); return View("Edit", vm); }
        var e = new PortfolioProject { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { LoadDisciplines(); return View("Edit", vm); }
        _db.PortfolioProjects.Add(e);
        await _db.SaveChangesAsync();
        await AppendGalleryAsync(vm.GalleryFiles, e.Id);
        await _log.LogAsync("Created", nameof(PortfolioProject), e.Id.ToString(), e.Title);
        Flash($"Project “{e.Title}” created.");
        return RedirectToAction(nameof(Edit), new { id = e.Id });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.PortfolioProjects.Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id);
        if (e is null) return NotFound();
        LoadDisciplines();
        return View(new PortfolioProjectFormVm
        {
            Id = e.Id, Title = e.Title, TitleAr = e.TitleAr, Slug = e.Slug,
            ShortDescription = e.ShortDescription, ShortDescriptionAr = e.ShortDescriptionAr,
            Description = e.Description, DescriptionAr = e.DescriptionAr,
            ClientName = e.ClientName, ClientNameAr = e.ClientNameAr, Tag = e.Tag, TagAr = e.TagAr,
            DisciplineId = e.DisciplineId, CoverImage = e.CoverImage, ThumbnailImage = e.ThumbnailImage,
            Year = e.Year, Location = e.Location, LocationAr = e.LocationAr,
            ProjectUrl = e.ProjectUrl, InstagramUrl = e.InstagramUrl,
            DisplayOrder = e.DisplayOrder, IsFeatured = e.IsFeatured, IsPublished = e.IsPublished,
            Media = e.Media.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToList()
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PortfolioProjectFormVm vm)
    {
        var e = await _db.PortfolioProjects.Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (e is null) return NotFound();
        if (!ModelState.IsValid) { LoadDisciplines(); vm.Media = e.Media.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToList(); return View(vm); }
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { LoadDisciplines(); vm.Media = e.Media.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToList(); return View(vm); }
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await AppendGalleryAsync(vm.GalleryFiles, e.Id);
        await _log.LogAsync("Updated", nameof(PortfolioProject), e.Id.ToString(), e.Title);
        Flash($"Project “{e.Title}” saved.");
        return RedirectToAction(nameof(Edit), new { id = e.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.PortfolioProjects.Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.CoverImage);
        await _files.DeleteAsync(e.ThumbnailImage);
        foreach (var m in e.Media) { await _files.DeleteAsync(m.FilePath); await _files.DeleteAsync(m.ThumbnailPath); }
        _db.PortfolioProjects.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(PortfolioProject), id.ToString(), e.Title);
        Flash($"Project “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.PortfolioProjects.FindAsync(id);
        if (e is null) return NotFound();
        e.IsPublished = !e.IsPublished; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { IsActive = e.IsPublished });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.PortfolioProjects.Where(p => ids.Contains(p.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMedia(int id)
    {
        var m = await _db.PortfolioMedia.FindAsync(id);
        if (m is null) return NotFound();
        var projectId = m.PortfolioProjectId;
        await _files.DeleteAsync(m.FilePath);
        await _files.DeleteAsync(m.ThumbnailPath);
        _db.PortfolioMedia.Remove(m);
        await _db.SaveChangesAsync();
        Flash("Media removed.");
        return RedirectToAction(nameof(Edit), new { id = projectId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ReorderMedia([FromBody] int[] ids)
    {
        var items = await _db.PortfolioMedia.Where(m => ids.Contains(m.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(PortfolioProjectFormVm vm, PortfolioProject e)
    {
        e.Slug = await UniqueSlug(vm.Slug, vm.Title, e.Id == 0 ? null : e.Id);
        e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.ShortDescription = vm.ShortDescription; e.ShortDescriptionAr = vm.ShortDescriptionAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.ClientName = vm.ClientName; e.ClientNameAr = vm.ClientNameAr; e.Tag = vm.Tag; e.TagAr = vm.TagAr;
        e.DisciplineId = vm.DisciplineId;
        e.Year = vm.Year; e.Location = vm.Location; e.LocationAr = vm.LocationAr;
        e.ProjectUrl = vm.ProjectUrl; e.InstagramUrl = vm.InstagramUrl;
        e.DisplayOrder = vm.DisplayOrder; e.IsFeatured = vm.IsFeatured; e.IsPublished = vm.IsPublished;

        if (vm.CoverImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.CoverImageFile))
            {
                ModelState.AddModelError(nameof(vm.CoverImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.CoverImageFile, "projects");
            await _files.DeleteAsync(e.CoverImage);
            e.CoverImage = stored.WebPath;
        }
        else { e.CoverImage = vm.CoverImage; }

        if (vm.ThumbnailImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ThumbnailImageFile))
            {
                ModelState.AddModelError(nameof(vm.ThumbnailImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.ThumbnailImageFile, "projects");
            await _files.DeleteAsync(e.ThumbnailImage);
            e.ThumbnailImage = stored.WebPath;
        }
        else { e.ThumbnailImage = vm.ThumbnailImage; }
    }

    /// <summary>Store each uploaded gallery image and append PortfolioMedia rows.</summary>
    private async Task AppendGalleryAsync(IFormFile[]? files, int projectId)
    {
        if (files is null || files.Length == 0) return;
        var order = (await _db.PortfolioMedia.Where(m => m.PortfolioProjectId == projectId)
                                             .MaxAsync(m => (int?)m.DisplayOrder) ?? -1) + 1;
        foreach (var file in files)
        {
            if (file is not { Length: > 0 } || !_files.IsAllowedImage(file)) continue;
            var stored = await _files.SaveAsync(file, "projects");
            _db.PortfolioMedia.Add(new PortfolioMedia
            {
                PortfolioProjectId = projectId, MediaType = MediaType.Image,
                FilePath = stored.WebPath, DisplayOrder = order++
            });
        }
        await _db.SaveChangesAsync();
    }

    private void LoadDisciplines() =>
        ViewBag.Disciplines = _db.Disciplines.AsNoTracking()
            .OrderBy(d => d.DisplayOrder).ThenBy(d => d.Id).ToList();

    private async Task<string> UniqueSlug(string? provided, string title, int? excludeId)
    {
        var baseSlug = SlugHelper.Slugify(string.IsNullOrWhiteSpace(provided) ? title : provided);
        var slug = baseSlug; var i = 2;
        while (await _db.PortfolioProjects.AnyAsync(p => p.Slug == slug && p.Id != excludeId))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }
}
