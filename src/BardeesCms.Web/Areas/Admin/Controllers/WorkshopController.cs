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
public class WorkshopController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public WorkshopController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index()
    {
        var e = await _db.WorkshopSections.FirstOrDefaultAsync();
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(WorkshopFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var e = await _db.WorkshopSections.FirstOrDefaultAsync();
        var isNew = e is null;
        if (e is null) { e = new WorkshopSection { CreatedAt = DateTime.UtcNow }; _db.WorkshopSections.Add(e); }

        e.Eyebrow = vm.Eyebrow; e.EyebrowAr = vm.EyebrowAr;
        e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.ScheduleText = vm.ScheduleText; e.ScheduleTextAr = vm.ScheduleTextAr;
        e.LocationText = vm.LocationText; e.LocationTextAr = vm.LocationTextAr;
        e.Highlights = vm.Highlights; e.HighlightsAr = vm.HighlightsAr;
        e.ButtonText = vm.ButtonText; e.ButtonTextAr = vm.ButtonTextAr;
        e.WhatsAppMessage = vm.WhatsAppMessage; e.WhatsAppMessageAr = vm.WhatsAppMessageAr;
        e.IsActive = vm.IsActive;

        if (vm.ImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ImageFile))
            {
                ModelState.AddModelError(nameof(vm.ImageFile), "Please upload a valid image.");
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.ImageFile, "workshop");
            await _files.DeleteAsync(e.Image);
            e.Image = stored.WebPath;
        }
        else e.Image = vm.Image;

        if (!isNew) e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(WorkshopSection), e.Id.ToString(), e.Title ?? "Workshop");
        Flash("Workshop section saved.");
        return View(ToVm(e));
    }

    private static WorkshopFormVm ToVm(WorkshopSection? e) => new()
    {
        Id = e?.Id ?? 0,
        Eyebrow = e?.Eyebrow, EyebrowAr = e?.EyebrowAr,
        Title = e?.Title, TitleAr = e?.TitleAr,
        Description = e?.Description, DescriptionAr = e?.DescriptionAr,
        ScheduleText = e?.ScheduleText, ScheduleTextAr = e?.ScheduleTextAr,
        LocationText = e?.LocationText, LocationTextAr = e?.LocationTextAr,
        Highlights = e?.Highlights, HighlightsAr = e?.HighlightsAr,
        ButtonText = e?.ButtonText, ButtonTextAr = e?.ButtonTextAr,
        WhatsAppMessage = e?.WhatsAppMessage, WhatsAppMessageAr = e?.WhatsAppMessageAr,
        Image = e?.Image,
        IsActive = e?.IsActive ?? true
    };
}
