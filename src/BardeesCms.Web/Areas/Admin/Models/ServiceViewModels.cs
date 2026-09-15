using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="Service"/>.</summary>
public class ServiceFormVm
{
    public int Id { get; set; }

    [Display(Name = "Number")] [StringLength(10)]
    public string? Number { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";

    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    [StringLength(200)]
    [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Lowercase letters, numbers and hyphens only.")]
    public string? Slug { get; set; }

    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }
    [Display(Name = "Short description (Arabic)")]
    public string? ShortDescriptionAr { get; set; }

    [Display(Name = "Full description")]
    public string? Description { get; set; }
    [Display(Name = "Full description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Icon (bootstrap-icons class)"), StringLength(60)]
    public string? Icon { get; set; }

    /// <summary>Existing stored image path (preserved when no new file is uploaded).</summary>
    public string? Image { get; set; }
    /// <summary>Newly uploaded image, if any.</summary>
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Featured")]
    public bool IsFeatured { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
