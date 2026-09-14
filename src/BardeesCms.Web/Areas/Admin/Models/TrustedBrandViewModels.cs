using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="TrustedBrand"/> (text-only marquee entry).</summary>
public class TrustedBrandFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";
    [Display(Name = "Name (Arabic)"), StringLength(200)]
    public string? NameAr { get; set; }

    [Display(Name = "Emphasis")]
    public string? Emphasis { get; set; }
    [Display(Name = "Emphasis (Arabic)")]
    public string? EmphasisAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
