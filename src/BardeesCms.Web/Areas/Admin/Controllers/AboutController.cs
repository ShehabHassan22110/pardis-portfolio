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
public class AboutController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public AboutController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index()
    {
        var e = await _db.AboutSections.Include(a => a.Facts).FirstOrDefaultAsync();
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AboutFormVm vm)
    {
        if (!ModelState.IsValid) { vm.Facts ??= new(); return View(vm); }

        var e = await _db.AboutSections.Include(a => a.Facts).FirstOrDefaultAsync();
        var isNew = e is null;
        if (e is null) { e = new AboutSection { CreatedAt = DateTime.UtcNow }; _db.AboutSections.Add(e); }

        e.Eyebrow = vm.Eyebrow; e.EyebrowAr = vm.EyebrowAr;
        e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.LongDescription = vm.LongDescription; e.LongDescriptionAr = vm.LongDescriptionAr;
        e.Location = vm.Location; e.LocationAr = vm.LocationAr;
        e.Quote = vm.Quote; e.QuoteAr = vm.QuoteAr;
        e.ButtonText = vm.ButtonText; e.ButtonTextAr = vm.ButtonTextAr;
        e.ButtonUrl = vm.ButtonUrl;
        e.IsActive = vm.IsActive;

        // Image upload
        if (vm.ImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ImageFile))
            {
                ModelState.AddModelError(nameof(vm.ImageFile), "Please upload a valid image.");
                vm.Facts ??= new();
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.ImageFile, "about");
            await _files.DeleteAsync(e.Image);
            e.Image = stored.WebPath;
        }
        else e.Image = vm.Image;

        // Replace facts with the non-empty submitted rows.
        e.Facts.Clear();
        var order = 0;
        foreach (var f in (vm.Facts ?? new()).Where(x => !string.IsNullOrWhiteSpace(x.Label) || !string.IsNullOrWhiteSpace(x.Value)))
        {
            e.Facts.Add(new AboutFact
            {
                Label = f.Label ?? "",
                LabelAr = f.LabelAr,
                Value = f.Value ?? "",
                ValueAr = f.ValueAr,
                DisplayOrder = f.DisplayOrder != 0 ? f.DisplayOrder : order++
            });
        }

        if (!isNew) e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(AboutSection), e.Id.ToString(), e.Title);
        Flash("About section saved.");
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFact(int id)
    {
        var fact = await _db.AboutFacts.FindAsync(id);
        if (fact is null) return NotFound();
        _db.AboutFacts.Remove(fact);
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static AboutFormVm ToVm(AboutSection? e) => new()
    {
        Id = e?.Id ?? 0,
        Eyebrow = e?.Eyebrow, EyebrowAr = e?.EyebrowAr,
        Title = e?.Title ?? "", TitleAr = e?.TitleAr,
        Description = e?.Description, DescriptionAr = e?.DescriptionAr,
        LongDescription = e?.LongDescription, LongDescriptionAr = e?.LongDescriptionAr,
        Image = e?.Image,
        Location = e?.Location, LocationAr = e?.LocationAr,
        Quote = e?.Quote, QuoteAr = e?.QuoteAr,
        ButtonText = e?.ButtonText, ButtonTextAr = e?.ButtonTextAr, ButtonUrl = e?.ButtonUrl,
        IsActive = e?.IsActive ?? true,
        Facts = (e?.Facts ?? new List<AboutFact>())
            .OrderBy(f => f.DisplayOrder).ThenBy(f => f.Id)
            .Select(f => new AboutFactVm
            {
                Id = f.Id, Label = f.Label, LabelAr = f.LabelAr,
                Value = f.Value, ValueAr = f.ValueAr, DisplayOrder = f.DisplayOrder
            }).ToList()
    };
}
