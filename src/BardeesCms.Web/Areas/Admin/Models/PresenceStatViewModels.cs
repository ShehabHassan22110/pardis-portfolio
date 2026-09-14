using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="PresenceStat"/> (headline audience statistic).</summary>
public class PresenceStatFormVm
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Value { get; set; } = "";

    [Required, StringLength(200)]
    public string Label { get; set; } = "";
    [Display(Name = "Label (Arabic)"), StringLength(200)]
    public string? LabelAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
