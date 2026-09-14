namespace BardeesCms.Web.Models.Entities;

/// <summary>A service offering (e.g. "Brand Ambassador", "Advertising Model").</summary>
public class Service : AuditableEntity, IOrderable, IActivatable
{
    /// <summary>Display number, e.g. "01".</summary>
    public string? Number { get; set; }
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string Slug { get; set; } = "";
    public string? ShortDescription { get; set; }
    public string? ShortDescriptionAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
}
