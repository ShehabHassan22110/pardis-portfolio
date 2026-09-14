using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="ContentStyle"/> tag.</summary>
public class ContentStyleFormVm
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = "";

    [Display(Name = "Name (Arabic)"), StringLength(120)]
    public string? NameAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
