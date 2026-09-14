namespace BardeesCms.Web.Models.Entities;

/// <summary>A step in the "How a collaboration works" timeline. Unlimited steps allowed.</summary>
public class CollaborationStep : AuditableEntity, IOrderable, IActivatable
{
    /// <summary>Displayed step label, e.g. "01". Falls back to DisplayOrder when empty.</summary>
    public string? StepNumber { get; set; }
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public string? Icon { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
