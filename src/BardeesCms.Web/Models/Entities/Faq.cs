namespace BardeesCms.Web.Models.Entities;

/// <summary>A frequently-asked question / answer pair.</summary>
public class Faq : AuditableEntity, IOrderable, IActivatable
{
    public string Question { get; set; } = "";
    public string? QuestionAr { get; set; }
    public string Answer { get; set; } = "";
    public string? AnswerAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
