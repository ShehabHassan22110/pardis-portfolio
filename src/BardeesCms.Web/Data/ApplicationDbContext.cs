using BardeesCms.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Data;

/// <summary>EF Core context for the whole CMS, including ASP.NET Core Identity.</summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Singletons
    public DbSet<SiteSettings> SiteSettings => Set<SiteSettings>();
    public DbSet<HeroSection> HeroSections => Set<HeroSection>();
    public DbSet<HeroSlide> HeroSlides => Set<HeroSlide>();
    public DbSet<AboutSection> AboutSections => Set<AboutSection>();
    public DbSet<AboutFact> AboutFacts => Set<AboutFact>();
    public DbSet<ContactSettings> ContactSettings => Set<ContactSettings>();
    public DbSet<PageSection> PageSections => Set<PageSection>();

    // Content collections
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<TrustedBrand> TrustedBrands => Set<TrustedBrand>();
    public DbSet<MarketPosition> MarketPositions => Set<MarketPosition>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<DisciplineSubItem> DisciplineSubItems => Set<DisciplineSubItem>();
    public DbSet<PortfolioProject> PortfolioProjects => Set<PortfolioProject>();
    public DbSet<PortfolioMedia> PortfolioMedia => Set<PortfolioMedia>();
    public DbSet<Abaya> Abayas => Set<Abaya>();
    public DbSet<Video> Videos => Set<Video>();
    public DbSet<VideoCategory> VideoCategories => Set<VideoCategory>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<CollaborationStep> CollaborationSteps => Set<CollaborationStep>();
    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<SeoPage> SeoPages => Set<SeoPage>();
    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();
    public DbSet<NavigationItem> NavigationItems => Set<NavigationItem>();

    // Communication / system
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply IEntityTypeConfiguration<T> classes in this assembly.
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // --- Unique / lookup indexes ------------------------------------------------
        // Indexed string columns must have a bounded length (SQL Server can't index nvarchar(max)).
        builder.Entity<Discipline>(e => { e.Property(d => d.Slug).HasMaxLength(200); e.HasIndex(d => d.Slug).IsUnique(); });
        builder.Entity<Service>(e => { e.Property(s => s.Slug).HasMaxLength(200); e.HasIndex(s => s.Slug).IsUnique(); });
        builder.Entity<PortfolioProject>(e => { e.Property(p => p.Slug).HasMaxLength(200); e.HasIndex(p => p.Slug).IsUnique(); });
        builder.Entity<Abaya>(e => { e.Property(a => a.Slug).HasMaxLength(200); e.HasIndex(a => a.Slug).IsUnique(); });
        builder.Entity<PageSection>(e => { e.Property(p => p.Key).HasMaxLength(120); e.HasIndex(p => p.Key).IsUnique(); });
        builder.Entity<VideoCategory>(e => { e.Property(v => v.Key).HasMaxLength(120); e.HasIndex(v => v.Key).IsUnique(); });
        builder.Entity<SeoPage>(e => { e.Property(s => s.Route).HasMaxLength(300); e.HasIndex(s => s.Route).IsUnique(); });
        builder.Entity<ActivityLog>().HasIndex(a => a.CreatedAt);
        builder.Entity<ContactMessage>().HasIndex(m => m.CreatedAt);

        // --- Relationships & delete behaviour --------------------------------------
        builder.Entity<HeroSlide>()
            .HasOne(s => s.HeroSection).WithMany(h => h.Slides)
            .HasForeignKey(s => s.HeroSectionId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<AboutFact>()
            .HasOne(f => f.AboutSection).WithMany(a => a.Facts)
            .HasForeignKey(f => f.AboutSectionId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DisciplineSubItem>()
            .HasOne(s => s.Discipline).WithMany(d => d.SubItems)
            .HasForeignKey(s => s.DisciplineId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PortfolioMedia>()
            .HasOne(m => m.PortfolioProject).WithMany(p => p.Media)
            .HasForeignKey(m => m.PortfolioProjectId).OnDelete(DeleteBehavior.Cascade);

        // Keep projects when their discipline is removed (just detach the category).
        builder.Entity<PortfolioProject>()
            .HasOne(p => p.Discipline).WithMany(d => d.Projects)
            .HasForeignKey(p => p.DisciplineId).OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Video>()
            .HasOne(v => v.VideoCategory).WithMany(c => c.Videos)
            .HasForeignKey(v => v.VideoCategoryId).OnDelete(DeleteBehavior.SetNull);
    }
}
