namespace BardeesCms.Web.Models.Entities;

/// <summary>
/// A "why Bardees for the Saudi market" positioning point. Multiple blocks allowed;
/// the section heading is owned by a <see cref="PageSection"/> keyed "why".
/// </summary>
public class MarketPosition : AuditableEntity, IOrderable, IActivatable
{
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? Location { get; set; }
    public string? LocationAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
