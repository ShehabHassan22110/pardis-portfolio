namespace BardeesCms.Web.Models.Entities;

/// <summary>Per-page SEO metadata, resolved by <see cref="Route"/>.</summary>
public class SeoPage : AuditableEntity, IActivatable
{
    public string PageName { get; set; } = "";
    /// <summary>Route this SEO record applies to, e.g. "/", "/about", "/work".</summary>
    public string Route { get; set; } = "";
    public string? MetaTitle { get; set; }
    public string? MetaTitleAr { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaDescriptionAr { get; set; }
    public string? Keywords { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? OgTitle { get; set; }
    public string? OgTitleAr { get; set; }
    public string? OgDescription { get; set; }
    public string? OgDescriptionAr { get; set; }
    public string? OgImage { get; set; }
    public string? Robots { get; set; }
    public bool IsActive { get; set; } = true;
}
