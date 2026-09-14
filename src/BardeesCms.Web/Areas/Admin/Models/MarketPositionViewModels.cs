using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="MarketPosition"/>, with image upload.</summary>
public class MarketPositionFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";
    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Icon (bootstrap-icons class)"), StringLength(60)]
    public string? Icon { get; set; }

    /// <summary>Existing stored image path.</summary>
    public string? Image { get; set; }
    /// <summary>Newly uploaded image, if any.</summary>
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Location")]
    public string? Location { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
