using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// A design in Bardees' own abaya collection — a separate product brand from her
/// modelling services. Shown in the /abayas gallery and teased on the home page.
/// Ordering is handled off-site over WhatsApp ("Request Order"), so there is no price
/// or checkout here — just the design, its look, and how it's made (ready-to-wear vs custom).
/// </summary>
public class Abaya : AuditableEntity, IOrderable, IActivatable
{
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public string Slug { get; set; } = "";

    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }

    /// <summary>Fabric / material note, e.g. "Crêpe · Hand embroidery".</summary>
    public string? Fabric { get; set; }
    public string? FabricAr { get; set; }

    public string? CoverImage { get; set; }
    public string? ThumbnailImage { get; set; }

    /// <summary>Ready-to-wear (buy as shown) or made-to-measure / custom tailoring.</summary>
    public AbayaType Type { get; set; } = AbayaType.ReadyToWear;

    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
}
