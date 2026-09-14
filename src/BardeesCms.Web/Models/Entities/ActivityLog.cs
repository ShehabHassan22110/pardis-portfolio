namespace BardeesCms.Web.Models.Entities;

/// <summary>An audit-trail record of an admin action.</summary>
public class ActivityLog
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    /// <summary>Verb, e.g. "Created", "Updated", "Deleted", "Uploaded".</summary>
    public string Action { get; set; } = "";
    /// <summary>Entity type affected, e.g. "PortfolioProject".</summary>
    public string? EntityName { get; set; }
    public string? EntityId { get; set; }
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}
