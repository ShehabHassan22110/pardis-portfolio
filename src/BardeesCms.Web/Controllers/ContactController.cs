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
    public ContactController(IPublicContentService content, ApplicationDbContext db)
    { _content = content; _db = db; }

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

        vm.Sent = true;
        vm.Form = new ContactFormInput();
        ViewData["Seo"] = await _content.GetSeoAsync("/contact");
        return View(vm);
    }
}
