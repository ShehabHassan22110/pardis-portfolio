using Microsoft.AspNetCore.Identity;

namespace BardeesCms.Web.Models.Entities;

/// <summary>Admin/CMS user. Extends ASP.NET Core Identity with display metadata.</summary>
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? AvatarPath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
}
