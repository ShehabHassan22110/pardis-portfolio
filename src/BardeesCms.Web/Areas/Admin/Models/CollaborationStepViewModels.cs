using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="CollaborationStep"/>.</summary>
public class CollaborationStepFormVm
{
    public int Id { get; set; }

    [Display(Name = "Step number"), StringLength(10)]
    public string? StepNumber { get; set; }

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

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
