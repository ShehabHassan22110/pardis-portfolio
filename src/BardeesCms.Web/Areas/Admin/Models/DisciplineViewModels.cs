using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="Discipline"/>, with cover image upload and sub-items.</summary>
public class DisciplineFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";
    [Display(Name = "Name (Arabic)"), StringLength(200)]
    public string? NameAr { get; set; }

    [StringLength(200)]
    [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Lowercase letters, numbers and hyphens only.")]
    public string? Slug { get; set; }

    [Display(Name = "Number"), StringLength(10)]
    public string? Number { get; set; }

    [Display(Name = "Tagline")]
    public string? Tagline { get; set; }
    [Display(Name = "Tagline (Arabic)")]
    public string? TaglineAr { get; set; }

    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Icon (bootstrap-icons class)"), StringLength(60)]
    public string? Icon { get; set; }

    /// <summary>Existing stored cover image path.</summary>
    public string? CoverImage { get; set; }
    /// <summary>Newly uploaded cover image, if any.</summary>
    [Display(Name = "Cover image")]
    public IFormFile? CoverImageFile { get; set; }

    [Display(Name = "Status")]
    public DisciplineStatus Status { get; set; } = DisciplineStatus.Live;

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    public List<DisciplineSubItemVm> SubItems { get; set; } = new();
}

/// <summary>A single sub-item row in the discipline editor.</summary>
public class DisciplineSubItemVm
{
    public string? Name { get; set; }
    public string? NameAr { get; set; }
}

/// <summary>Index row: a discipline plus its project count.</summary>
public class DisciplineListItem
{
    public Discipline Discipline { get; set; } = null!;
    public int ProjectCount { get; set; }
}
