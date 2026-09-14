using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="NavigationItem"/> (header or footer link).</summary>
public class NavigationFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";
    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    [Required, StringLength(500)]
    public string Url { get; set; } = "";

    [Display(Name = "Target")]
    public string? Target { get; set; } = "_self";

    [Display(Name = "Location")]
    public NavLocation Location { get; set; } = NavLocation.Header;

    [Display(Name = "Group / column heading"), StringLength(120)]
    public string? Group { get; set; }
    [Display(Name = "Group (Arabic)"), StringLength(120)]
    public string? GroupAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
