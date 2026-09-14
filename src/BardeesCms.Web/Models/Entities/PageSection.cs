namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// Editable heading block for a section of the public site (eyebrow + title + note).
/// Keyed by a stable <see cref="Key"/> (e.g. "clients", "why", "work") so views can
/// look up their heading without hardcoding copy. Complex data still lives in
/// dedicated entities — this only owns the section chrome.
/// </summary>
public class PageSection : AuditableEntity, IActivatable, IOrderable
{
    /// <summary>Stable identifier used by views, e.g. "why", "clients", "showreel".</summary>
    public string Key { get; set; } = "";
    /// <summary>Which public page this section belongs to (e.g. "home", "about").</summary>
    public string Page { get; set; } = "home";

    public string? Eyebrow { get; set; }
    public string? EyebrowAr { get; set; }
    public string? Title { get; set; }
    public string? TitleAr { get; set; }
    /// <summary>Sub-heading / note under the title.</summary>
    public string? Note { get; set; }
    public string? NoteAr { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
