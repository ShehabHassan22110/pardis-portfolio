using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="Platform"/> (digital-presence entry).</summary>
public class PlatformFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";
    [Display(Name = "Name (Arabic)"), StringLength(200)]
    public string? NameAr { get; set; }

    [Display(Name = "Username")]
    public string? Username { get; set; }

    [Display(Name = "URL"), Url]
    public string? Url { get; set; }

    [Display(Name = "Icon (bootstrap-icons class)"), StringLength(60)]
    public string? Icon { get; set; }

    [Display(Name = "Followers")]
    public string? Followers { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
