using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Models.ViewModels;

/// <summary>Bindable public contact/booking form.</summary>
public class ContactFormInput
{
    [Required, StringLength(120)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = "";

    [Phone, StringLength(40)]
    public string? Phone { get; set; }

    [StringLength(160)]
    public string? Company { get; set; }

    [StringLength(160)]
    public string? Subject { get; set; }

    [Required, StringLength(4000)]
    public string Message { get; set; } = "";

    /// <summary>Honeypot — must stay empty. Bots fill it; humans never see it.</summary>
    public string? Website { get; set; }
}
