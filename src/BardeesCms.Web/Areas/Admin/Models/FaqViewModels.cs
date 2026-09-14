using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="Faq"/>.</summary>
public class FaqFormVm
{
    public int Id { get; set; }

    [Required, StringLength(300)]
    public string Question { get; set; } = "";

    [Display(Name = "Question (Arabic)"), StringLength(300)]
    public string? QuestionAr { get; set; }

    [Required]
    public string Answer { get; set; } = "";

    [Display(Name = "Answer (Arabic)")]
    public string? AnswerAr { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
