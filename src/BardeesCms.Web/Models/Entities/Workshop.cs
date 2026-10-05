namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// The "Attend a workshop" invitation shown on the /abayas page. Singleton row (Id = 1).
/// Promotes Bardees' tailoring/design workshops; there is no on-site booking — interested
/// visitors reach out over WhatsApp ("Text me on WhatsApp"), so this only owns the copy,
/// a few schedule meta lines, an optional image and the pre-filled WhatsApp message.
/// The WhatsApp number itself is reused from <see cref="SiteSettings.WhatsApp"/>.
/// </summary>
public class WorkshopSection : AuditableEntity, IActivatable
{
    public string? Eyebrow { get; set; }
    public string? EyebrowAr { get; set; }

    public string? Title { get; set; }
    public string? TitleAr { get; set; }

    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }

    /// <summary>Schedule meta line, e.g. "Saturdays · 4–7 PM".</summary>
    public string? ScheduleText { get; set; }
    public string? ScheduleTextAr { get; set; }

    /// <summary>Location / venue meta line.</summary>
    public string? LocationText { get; set; }
    public string? LocationTextAr { get; set; }

    /// <summary>What's covered — one highlight per line; rendered as a bullet list.</summary>
    public string? Highlights { get; set; }
    public string? HighlightsAr { get; set; }

    /// <summary>CTA label; falls back to a localized default when empty.</summary>
    public string? ButtonText { get; set; }
    public string? ButtonTextAr { get; set; }

    /// <summary>Pre-filled text for the WhatsApp message; falls back to a default when empty.</summary>
    public string? WhatsAppMessage { get; set; }
    public string? WhatsAppMessageAr { get; set; }

    public string? Image { get; set; }

    public bool IsActive { get; set; } = true;
}
