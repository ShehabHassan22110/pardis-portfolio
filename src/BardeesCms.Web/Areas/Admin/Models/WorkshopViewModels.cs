using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Editor for the "Attend a workshop" invitation singleton shown on /abayas.</summary>
public class WorkshopFormVm
{
    public int Id { get; set; }

    [Display(Name = "Eyebrow")]
    public string? Eyebrow { get; set; }
    [Display(Name = "Eyebrow (Arabic)")]
    public string? EyebrowAr { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }
    [Display(Name = "Title (Arabic)")]
    public string? TitleAr { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Schedule")]
    public string? ScheduleText { get; set; }
    [Display(Name = "Schedule (Arabic)")]
    public string? ScheduleTextAr { get; set; }

    [Display(Name = "Location")]
    public string? LocationText { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationTextAr { get; set; }

    [Display(Name = "Highlights")]
    public string? Highlights { get; set; }
    [Display(Name = "Highlights (Arabic)")]
    public string? HighlightsAr { get; set; }

    [Display(Name = "Button text")]
    public string? ButtonText { get; set; }
    [Display(Name = "Button text (Arabic)")]
    public string? ButtonTextAr { get; set; }

    [Display(Name = "WhatsApp message")]
    public string? WhatsAppMessage { get; set; }
    [Display(Name = "WhatsApp message (Arabic)")]
    public string? WhatsAppMessageAr { get; set; }

    /// <summary>Existing stored image path.</summary>
    public string? Image { get; set; }
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
