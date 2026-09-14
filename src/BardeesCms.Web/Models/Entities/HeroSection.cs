namespace BardeesCms.Web.Models.Entities;

/// <summary>The homepage hero. Singleton row (Id = 1). Slides live in <see cref="HeroSlide"/>.</summary>
public class HeroSection : AuditableEntity, IActivatable
{
    public string? Eyebrow { get; set; }
    public string? EyebrowAr { get; set; }
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    /// <summary>Second line of the masthead (rendered in accent colour).</summary>
    public string? Subtitle { get; set; }
    public string? SubtitleAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    /// <summary>Sector strip under the lead, e.g. "Travel · Tourism · Hospitality".</summary>
    public string? Sectors { get; set; }
    public string? SectorsAr { get; set; }

    public string? PrimaryButtonText { get; set; }
    public string? PrimaryButtonTextAr { get; set; }
    public string? PrimaryButtonUrl { get; set; }
    public string? SecondaryButtonText { get; set; }
    public string? SecondaryButtonTextAr { get; set; }
    public string? SecondaryButtonUrl { get; set; }

    public string? BackgroundImage { get; set; }
    public string? BackgroundVideo { get; set; }

    public string? FeaturedLabel { get; set; }
    public string? FeaturedLabelAr { get; set; }
    public string? IssueNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<HeroSlide> Slides { get; set; } = new List<HeroSlide>();
}

/// <summary>A single rotating look in the hero background.</summary>
public class HeroSlide : IOrderable, IActivatable
{
    public int Id { get; set; }
    public int HeroSectionId { get; set; }
    public HeroSection? HeroSection { get; set; }

    /// <summary>Image base name or media path.</summary>
    public string Image { get; set; } = "";
    public string? Label { get; set; }
    public string? LabelAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
