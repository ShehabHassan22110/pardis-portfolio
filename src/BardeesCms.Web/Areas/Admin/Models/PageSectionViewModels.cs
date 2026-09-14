using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="PageSection"/> heading block.</summary>
public class PageSectionFormVm
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Lowercase letters, numbers and hyphens only.")]
    public string Key { get; set; } = "";

    [Required, StringLength(60)]
    public string Page { get; set; } = "home";

    [Display(Name = "Eyebrow"), StringLength(120)]
    public string? Eyebrow { get; set; }
    [Display(Name = "Eyebrow (Arabic)"), StringLength(120)]
    public string? EyebrowAr { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }
    [Display(Name = "Title (Arabic)")]
    public string? TitleAr { get; set; }

    [Display(Name = "Note")]
    public string? Note { get; set; }
    [Display(Name = "Note (Arabic)")]
    public string? NoteAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
