using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>Contact / booking page settings. Singleton row (Id = 1).</summary>
public class ContactSettings : AuditableEntity, IActivatable
{
    public string? Title { get; set; }
    public string? TitleAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? WhatsApp { get; set; }
    public string? Location { get; set; }
    public string? LocationAr { get; set; }
    public string? Instagram { get; set; }
    public string? TikTok { get; set; }
    public string? BookingText { get; set; }
    public string? BookingTextAr { get; set; }
    public string? BookingButtonText { get; set; }
    public string? BookingButtonTextAr { get; set; }
    public string? BookingUrl { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>An inbound message from the public contact/booking form.</summary>
public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string? Subject { get; set; }
    public string Message { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public MessageStatus Status { get; set; } = MessageStatus.Unread;
    /// <summary>Captured for spam triage; not shown publicly.</summary>
    public string? IpAddress { get; set; }
}
