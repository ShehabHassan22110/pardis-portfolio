namespace BardeesCms.Web.Services;

/// <summary>Writes audit-trail records for admin actions.</summary>
public interface IActivityLogger
{
    Task LogAsync(string action, string? entityName = null, string? entityId = null, string? description = null);
}
