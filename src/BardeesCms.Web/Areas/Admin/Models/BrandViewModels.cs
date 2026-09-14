using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a Brand (client / ambassadorship), with logo upload.</summary>
public class BrandFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";
    [Display(Name = "Name (Arabic)"), StringLength(200)]
    public string? NameAr { get; set; }

    [Display(Name = "Role")]
    public string? Role { get; set; }
    [Display(Name = "Role (Arabic)")]
    public string? RoleAr { get; set; }

    [Display(Name = "Sector")]
    public string? Sector { get; set; }
    [Display(Name = "Sector (Arabic)")]
    public string? SectorAr { get; set; }

    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Website URL"), Url]
    public string? WebsiteUrl { get; set; }

    /// <summary>Existing stored logo path.</summary>
    public string? Logo { get; set; }
    /// <summary>Newly uploaded logo, if any.</summary>
    [Display(Name = "Logo")]
    public IFormFile? LogoFile { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Featured")]
    public bool IsFeatured { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
