namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// A client / ambassadorship relationship, shown in the "Clients &amp; ambassadorships"
/// carousel with a logo silhouette. Rich entity (matches the CMS spec's Brand fields).
/// </summary>
public class Brand : AuditableEntity, IOrderable, IActivatable
{
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    /// <summary>Logo image path (silhouette PNG/SVG, tinted via CSS mask on the frontend).</summary>
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    /// <summary>Relationship role, e.g. "Brand Ambassador".</summary>
    public string? Role { get; set; }
    public string? RoleAr { get; set; }
    /// <summary>Sector / category, e.g. "Watches".</summary>
    public string? Sector { get; set; }
    public string? SectorAr { get; set; }
    public string? WebsiteUrl { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// A lightweight brand name for the "Trusted by brands across the Gulf" marquee.
/// Text-only (optional emphasis line), no logo.
/// </summary>
public class TrustedBrand : IOrderable, IActivatable
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    /// <summary>Small emphasis line under the name, e.g. "New York".</summary>
    public string? Emphasis { get; set; }
    public string? EmphasisAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
