using System.Net;
using System.Net.Mail;

namespace BardeesCms.Web.Services;

/// <summary>SMTP configuration, bound from the "Email" config section (appsettings / env / user-secrets).</summary>
public class EmailSettings
{
    public string? Host { get; set; }
    public int Port { get; set; } = 587;
    public bool UseSsl { get; set; } = true;
    public string? Username { get; set; }
    public string? Password { get; set; }
    /// <summary>Address the email is sent from (usually the SMTP account).</summary>
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
    /// <summary>Inbox that receives contact-form messages. Falls back to Site Settings email if blank.</summary>
    public string? ToEmail { get; set; }

    public bool IsConfigured => !string.IsNullOrWhiteSpace(Host) && !string.IsNullOrWhiteSpace(Username);
}

public interface IEmailSender
{
    /// <summary>Sends a plain-text email to the configured inbox. Returns false if SMTP isn't configured.</summary>
    Task<bool> SendAsync(string subject, string body, string? toOverride = null, string? replyTo = null, CancellationToken ct = default);
}

/// <summary>System.Net.Mail SMTP sender — no extra dependencies. Works with Gmail/Outlook/host SMTP.</summary>
public class SmtpEmailSender : IEmailSender
{
    private readonly EmailSettings _s;
    private readonly ILogger<SmtpEmailSender> _log;
    public SmtpEmailSender(EmailSettings settings, ILogger<SmtpEmailSender> log) { _s = settings; _log = log; }

    public async Task<bool> SendAsync(string subject, string body, string? toOverride = null, string? replyTo = null, CancellationToken ct = default)
    {
        var to = string.IsNullOrWhiteSpace(toOverride) ? _s.ToEmail : toOverride;
        if (!_s.IsConfigured || string.IsNullOrWhiteSpace(to))
        {
            _log.LogWarning("Email not sent — SMTP not configured (Email:Host/Username) or no recipient.");
            return false;
        }

        using var msg = new MailMessage
        {
            From = new MailAddress(_s.FromEmail ?? _s.Username!, _s.FromName ?? "Bardees Website"),
            Subject = subject,
            Body = body,
            IsBodyHtml = false,
        };
        msg.To.Add(to);
        if (!string.IsNullOrWhiteSpace(replyTo))
        {
            try { msg.ReplyToList.Add(new MailAddress(replyTo)); } catch { /* ignore malformed */ }
        }

        using var client = new SmtpClient(_s.Host, _s.Port)
        {
            EnableSsl = _s.UseSsl,
            Credentials = new NetworkCredential(_s.Username, _s.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
        };
        await client.SendMailAsync(msg, ct);
        _log.LogInformation("Contact email sent to {To}.", to);
        return true;
    }
}
