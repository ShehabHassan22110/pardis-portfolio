using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace BardeesCms.Web.Services;

/// <summary>Persists <see cref="ActivityLog"/> rows, capturing the current user + IP.</summary>
public class ActivityLogger : IActivityLogger
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _http;

    public ActivityLogger(ApplicationDbContext db, IHttpContextAccessor http)
    {
        _db = db;
        _http = http;
    }

    public async Task LogAsync(string action, string? entityName = null, string? entityId = null, string? description = null)
    {
        var ctx = _http.HttpContext;
        _db.ActivityLogs.Add(new ActivityLog
        {
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            Description = description,
            UserId = ctx?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
            UserName = ctx?.User?.Identity?.Name,
            IpAddress = ctx?.Connection?.RemoteIpAddress?.ToString(),
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }
}
