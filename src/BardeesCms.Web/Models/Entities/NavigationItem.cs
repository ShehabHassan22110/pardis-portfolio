using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>A navigation link for the header or footer.</summary>
public class NavigationItem : IOrderable, IActivatable
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string Url { get; set; } = "";
    /// <summary>Anchor target, e.g. "_self" or "_blank".</summary>
    public string? Target { get; set; }
    public NavLocation Location { get; set; } = NavLocation.Header;
    /// <summary>Optional grouping heading for footer columns (e.g. "Explore", "Contact").</summary>
    public string? Group { get; set; }
    public string? GroupAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
