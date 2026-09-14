using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>A selected-work portfolio project belonging to a discipline.</summary>
public class PortfolioProject : AuditableEntity, IOrderable
{
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string Slug { get; set; } = "";
    public string? ShortDescription { get; set; }
    public string? ShortDescriptionAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    /// <summary>Client / brand credited on the project (e.g. "Beauty Editorial").</summary>
    public string? ClientName { get; set; }
    public string? ClientNameAr { get; set; }
    /// <summary>Short tag line, e.g. "Makeup · Glam".</summary>
    public string? Tag { get; set; }
    public string? TagAr { get; set; }

    public int? DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }

    public string? CoverImage { get; set; }
    public string? ThumbnailImage { get; set; }
    public string? Year { get; set; }
    public string? Location { get; set; }
    public string? LocationAr { get; set; }
    public string? ProjectUrl { get; set; }
    public string? InstagramUrl { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; } = true;

    public ICollection<PortfolioMedia> Media { get; set; } = new List<PortfolioMedia>();
}

/// <summary>An image or video attached to a portfolio project.</summary>
public class PortfolioMedia : IOrderable
{
    public int Id { get; set; }
    public int PortfolioProjectId { get; set; }
    public PortfolioProject? PortfolioProject { get; set; }

    public MediaType MediaType { get; set; } = MediaType.Image;
    public string FilePath { get; set; } = "";
    public string? ThumbnailPath { get; set; }
    public string? Caption { get; set; }
    public string? CaptionAr { get; set; }
    public string? AltText { get; set; }
    public string? AltTextAr { get; set; }
    public int DisplayOrder { get; set; }
}
