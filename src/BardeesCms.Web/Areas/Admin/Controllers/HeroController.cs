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
public class HeroController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public HeroController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index()
    {
        var e = await _db.HeroSections.Include(h => h.Slides).FirstOrDefaultAsync();
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(HeroFormVm vm)
    {
        if (!ModelState.IsValid) { vm.Slides ??= new(); return View(vm); }

        var e = await _db.HeroSections.Include(h => h.Slides).FirstOrDefaultAsync();
        var isNew = e is null;
        if (e is null) { e = new HeroSection { CreatedAt = DateTime.UtcNow }; _db.HeroSections.Add(e); }

        e.Eyebrow = vm.Eyebrow; e.EyebrowAr = vm.EyebrowAr;
        e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.Subtitle = vm.Subtitle; e.SubtitleAr = vm.SubtitleAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Sectors = vm.Sectors; e.SectorsAr = vm.SectorsAr;
        e.PrimaryButtonText = vm.PrimaryButtonText; e.PrimaryButtonTextAr = vm.PrimaryButtonTextAr;
        e.PrimaryButtonUrl = vm.PrimaryButtonUrl;
        e.SecondaryButtonText = vm.SecondaryButtonText; e.SecondaryButtonTextAr = vm.SecondaryButtonTextAr;
        e.SecondaryButtonUrl = vm.SecondaryButtonUrl;
        e.BackgroundVideo = vm.BackgroundVideo;
        e.FeaturedLabel = vm.FeaturedLabel; e.FeaturedLabelAr = vm.FeaturedLabelAr;
        e.IssueNumber = vm.IssueNumber;
        e.IsActive = vm.IsActive;

        // Background image upload
        if (vm.BackgroundImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.BackgroundImageFile))
            {
                ModelState.AddModelError(nameof(vm.BackgroundImageFile), "Please upload a valid image.");
                vm.Slides ??= new();
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.BackgroundImageFile, "hero");
            await _files.DeleteAsync(e.BackgroundImage);
            e.BackgroundImage = stored.WebPath;
        }
        else e.BackgroundImage = vm.BackgroundImage;

        // Replace slides with the submitted rows. A row counts if it has an image path OR
        // an uploaded file; uploaded files are stored and win over the text path.
        e.Slides.Clear();
        var order = 0;
        foreach (var s in vm.Slides ?? new())
        {
            var img = s.Image?.Trim();
            if (s.ImageFile is { Length: > 0 })
            {
                if (!_files.IsAllowedImage(s.ImageFile))
                {
                    ModelState.AddModelError(string.Empty, "One of the slide images is not a valid image file.");
                    vm.Slides ??= new();
                    return View(vm);
                }
                var stored = await _files.SaveAsync(s.ImageFile, "hero");
                img = stored.WebPath;
            }
            if (string.IsNullOrWhiteSpace(img)) continue;
            e.Slides.Add(new HeroSlide
            {
                Image = img,
                Label = s.Label,
                LabelAr = s.LabelAr,
                DisplayOrder = s.DisplayOrder != 0 ? s.DisplayOrder : order++,
                IsActive = s.IsActive
            });
        }

        if (!isNew) e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(HeroSection), e.Id.ToString(), e.Title);
        Flash("Hero section saved.");
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSlide(int id)
    {
        var slide = await _db.HeroSlides.FindAsync(id);
        if (slide is null) return NotFound();
        _db.HeroSlides.Remove(slide);
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static HeroFormVm ToVm(HeroSection? e) => new()
    {
        Id = e?.Id ?? 0,
        Eyebrow = e?.Eyebrow, EyebrowAr = e?.EyebrowAr,
        Title = e?.Title ?? "", TitleAr = e?.TitleAr,
        Subtitle = e?.Subtitle, SubtitleAr = e?.SubtitleAr,
        Description = e?.Description, DescriptionAr = e?.DescriptionAr,
        Sectors = e?.Sectors, SectorsAr = e?.SectorsAr,
        PrimaryButtonText = e?.PrimaryButtonText, PrimaryButtonTextAr = e?.PrimaryButtonTextAr,
        PrimaryButtonUrl = e?.PrimaryButtonUrl,
        SecondaryButtonText = e?.SecondaryButtonText, SecondaryButtonTextAr = e?.SecondaryButtonTextAr,
        SecondaryButtonUrl = e?.SecondaryButtonUrl,
        BackgroundImage = e?.BackgroundImage, BackgroundVideo = e?.BackgroundVideo,
        FeaturedLabel = e?.FeaturedLabel, FeaturedLabelAr = e?.FeaturedLabelAr,
        IssueNumber = e?.IssueNumber,
        IsActive = e?.IsActive ?? true,
        Slides = (e?.Slides ?? new List<HeroSlide>())
            .OrderBy(s => s.DisplayOrder).ThenBy(s => s.Id)
            .Select(s => new HeroSlideVm
            {
                Id = s.Id, Image = s.Image, Label = s.Label, LabelAr = s.LabelAr,
                DisplayOrder = s.DisplayOrder, IsActive = s.IsActive
            }).ToList()
    };
}
