using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageContent)]
public class ContactController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public ContactController(ApplicationDbContext db, IActivityLogger log)
    { _db = db; _log = log; }

    public async Task<IActionResult> Index()
    {
        var e = await _db.ContactSettings.FirstOrDefaultAsync();
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactSettingsFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var e = await _db.ContactSettings.FirstOrDefaultAsync();
        var isNew = e is null;
        if (e is null) { e = new ContactSettings { CreatedAt = DateTime.UtcNow }; _db.ContactSettings.Add(e); }

        e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Email = vm.Email; e.Phone = vm.Phone; e.WhatsApp = vm.WhatsApp;
        e.Location = vm.Location; e.LocationAr = vm.LocationAr;
        e.Instagram = vm.Instagram; e.TikTok = vm.TikTok;
        e.BookingText = vm.BookingText; e.BookingTextAr = vm.BookingTextAr;
        e.BookingButtonText = vm.BookingButtonText; e.BookingButtonTextAr = vm.BookingButtonTextAr;
        e.BookingUrl = vm.BookingUrl;
        e.IsActive = vm.IsActive;

        if (!isNew) e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(ContactSettings), e.Id.ToString(), e.Title ?? "Contact");
        Flash("Contact settings saved.");
        return View(ToVm(e));
    }

    private static ContactSettingsFormVm ToVm(ContactSettings? e) => new()
    {
        Id = e?.Id ?? 0,
        Title = e?.Title, TitleAr = e?.TitleAr,
        Description = e?.Description, DescriptionAr = e?.DescriptionAr,
        Email = e?.Email, Phone = e?.Phone, WhatsApp = e?.WhatsApp,
        Location = e?.Location, LocationAr = e?.LocationAr,
        Instagram = e?.Instagram, TikTok = e?.TikTok,
        BookingText = e?.BookingText, BookingTextAr = e?.BookingTextAr,
        BookingButtonText = e?.BookingButtonText, BookingButtonTextAr = e?.BookingButtonTextAr,
        BookingUrl = e?.BookingUrl,
        IsActive = e?.IsActive ?? true
    };
}
