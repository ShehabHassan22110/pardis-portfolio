using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// A discipline / content category (e.g. "Beauty &amp; Fashion Model", "Commercial Model").
/// Portfolio projects belong to a discipline. Has sub-items and its own detail page.
/// </summary>
public class Discipline : AuditableEntity, IOrderable, IActivatable
{
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public string Slug { get; set; } = "";
    /// <summary>Short display number, e.g. "01".</summary>
    public string? Number { get; set; }
    public string? Tagline { get; set; }
    public string? TaglineAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public string? Icon { get; set; }
    public string? CoverImage { get; set; }
    public DisciplineStatus Status { get; set; } = DisciplineStatus.Live;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<DisciplineSubItem> SubItems { get; set; } = new List<DisciplineSubItem>();
    public ICollection<PortfolioProject> Projects { get; set; } = new List<PortfolioProject>();
}

/// <summary>A sub-specialisation listed under a discipline (e.g. "Hand Model — Watches").</summary>
public class DisciplineSubItem : IOrderable
{
    public int Id { get; set; }
    public int DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public int DisplayOrder { get; set; }
}
