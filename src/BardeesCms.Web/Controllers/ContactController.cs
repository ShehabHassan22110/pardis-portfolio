using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;
using BardeesCms.Web.Models.ViewModels;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class ContactController : Controller
{
    private readonly IPublicContentService _content;
    private readonly ApplicationDbContext _db;
    private readonly IEmailSender _email;
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<ContactController> _log;
    public ContactController(IPublicContentService content, ApplicationDbContext db,
        IEmailSender email, EmailSettings emailSettings, ILogger<ContactController> log)
    { _content = content; _db = db; _email = email; _emailSettings = emailSettings; _log = log; }

    [HttpGet("/contact")]
    public async Task<IActionResult> Index(string? type)
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/contact");
        var vm = await _content.GetContactAsync();
        if (!string.IsNullOrWhiteSpace(type)) vm.Form.Subject = type;
        return View(vm);
    }

    [HttpPost("/contact"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactFormInput form)
    {
        // Honeypot: bots fill the hidden "Website" field — silently accept, don't store.
        if (!string.IsNullOrWhiteSpace(form.Website))
            return RedirectToAction(nameof(Index), new { sent = true });

        var vm = await _content.GetContactAsync();
        vm.Form = form;

        if (!ModelState.IsValid)
        {
            ViewData["Seo"] = await _content.GetSeoAsync("/contact");
            return View(vm);
        }

        _db.ContactMessages.Add(new ContactMessage
        {
            Name = form.Name.Trim(),
            Email = form.Email.Trim(),
            Phone = form.Phone?.Trim(),
            Company = form.Company?.Trim(),
            Subject = form.Subject?.Trim(),
            Message = form.Message.Trim(),
            CreatedAt = DateTime.UtcNow,
            Status = MessageStatus.Unread,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
        });
        await _db.SaveChangesAsync();

        // Email the enquiry to Bardees. Recipient: Email:ToEmail config, else the dashboard
        // contact email. Never fail the submit if email can't be sent (it's already in the DB).
        var recipient = !string.IsNullOrWhiteSpace(_emailSettings.ToEmail)
            ? _emailSettings.ToEmail
            : (vm.Settings?.Email ?? "").Trim();
        if (!string.IsNullOrWhiteSpace(recipient) && recipient != "#")
        {
            var body =
                $"New enquiry from the website\n\n" +
                $"Name: {form.Name}\n" +
                (string.IsNullOrWhiteSpace(form.Company) ? "" : $"Company: {form.Company}\n") +
                $"Email: {form.Email}\n" +
                (string.IsNullOrWhiteSpace(form.Phone) ? "" : $"Phone: {form.Phone}\n") +
                (string.IsNullOrWhiteSpace(form.Subject) ? "" : $"Type: {form.Subject}\n") +
                $"\nMessage:\n{form.Message}\n";
            try
            {
                await _email.SendAsync($"Website enquiry — {form.Name}", body, recipient, replyTo: form.Email);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Contact email failed to send (message still saved to the dashboard).");
            }
        }

        vm.Sent = true;
        vm.Form = new ContactFormInput();
        ViewData["Seo"] = await _content.GetSeoAsync("/contact");
        return View(vm);
    }
}
