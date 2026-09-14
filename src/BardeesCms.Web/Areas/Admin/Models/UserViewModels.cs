using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Row in the users list — user plus resolved roles.</summary>
public class UserListItem
{
    public string Id { get; set; } = "";
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}

/// <summary>Create form for a new admin user.</summary>
public class UserCreateVm
{
    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = "";

    [Display(Name = "Full name"), StringLength(200)]
    public string? FullName { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; } = "";
}

/// <summary>Edit form for an existing admin user (no password change here).</summary>
public class UserEditVm
{
    public string Id { get; set; } = "";

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Full name"), StringLength(200)]
    public string? FullName { get; set; }

    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; } = "";

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
