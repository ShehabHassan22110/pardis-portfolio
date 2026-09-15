using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Editor for the contact / booking settings singleton.</summary>
public class ContactSettingsFormVm
{
    public int Id { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }
    [Display(Name = "Title (Arabic)")]
    public string? TitleAr { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [EmailAddress, Display(Name = "Email")]
    public string? Email { get; set; }
    [Display(Name = "Phone")]
    public string? Phone { get; set; }
    [Display(Name = "WhatsApp")]
    public string? WhatsApp { get; set; }
    [Display(Name = "Location")]
    public string? Location { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationAr { get; set; }
    [Display(Name = "Instagram")]
    public string? Instagram { get; set; }
    [Display(Name = "TikTok")]
    public string? TikTok { get; set; }

    [Display(Name = "Booking text")]
    public string? BookingText { get; set; }
    [Display(Name = "Booking text (Arabic)")]
    public string? BookingTextAr { get; set; }
    [Display(Name = "Booking button text")]
    public string? BookingButtonText { get; set; }
    [Display(Name = "Booking button text (Arabic)")]
    public string? BookingButtonTextAr { get; set; }
    [Display(Name = "Booking URL")]
    public string? BookingUrl { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
