using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for an <see cref="BardeesCms.Web.Models.Entities.Abaya"/>.</summary>
public class AbayaFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";
    [Display(Name = "Name (Arabic)"), StringLength(200)]
    public string? NameAr { get; set; }

    [Display(Name = "Slug"), StringLength(200)]
    public string? Slug { get; set; }

    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Fabric / detail"), StringLength(200)]
    public string? Fabric { get; set; }
    [Display(Name = "Fabric / detail (Arabic)"), StringLength(200)]
    public string? FabricAr { get; set; }

    public AbayaType Type { get; set; } = AbayaType.ReadyToWear;

    /// <summary>Existing image reference (base name like "abaya-01" or an uploaded path).</summary>
    public string? CoverImage { get; set; }
    [Display(Name = "Image")]
    public IFormFile? CoverImageFile { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Featured")]
    public bool IsFeatured { get; set; }
    [Display(Name = "Active (visible)")]
    public bool IsActive { get; set; } = true;
}
