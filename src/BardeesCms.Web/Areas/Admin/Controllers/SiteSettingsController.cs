using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageSettings)]
public class SiteSettingsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public SiteSettingsController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index()
    {
        var e = await _db.SiteSettings.FirstOrDefaultAsync();
        return View(ToVm(e));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(SiteSettingsFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var e = await _db.SiteSettings.FirstOrDefaultAsync();
        var isNew = e is null;
        if (e is null) { e = new SiteSettings { CreatedAt = DateTime.UtcNow }; _db.SiteSettings.Add(e); }

        // Branding
        e.SiteName = vm.SiteName; e.SiteNameAr = vm.SiteNameAr;
        e.BrandName = vm.BrandName; e.BrandNameAr = vm.BrandNameAr;
        e.BrandNameShort = vm.BrandNameShort; e.BrandNameShortAr = vm.BrandNameShortAr;
        e.Role = vm.Role; e.RoleAr = vm.RoleAr;
        e.Tagline = vm.Tagline; e.TaglineAr = vm.TaglineAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Favicon = vm.Favicon;

        // Logo upload
        if (vm.LogoFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.LogoFile))
            {
                ModelState.AddModelError(nameof(vm.LogoFile), "Please upload a valid image.");
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.LogoFile, "site");
            await _files.DeleteAsync(e.Logo);
            e.Logo = stored.WebPath;
        }
        else e.Logo = vm.Logo;

        // Theme logos (light + dark). Uploaded file wins; otherwise keep existing.
        if (vm.LogoLightFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.LogoLightFile))
            {
                ModelState.AddModelError(nameof(vm.LogoLightFile), "Please upload a valid image.");
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.LogoLightFile, "site");
            await _files.DeleteAsync(e.LogoLight);
            e.LogoLight = stored.WebPath;
        }
        else e.LogoLight = vm.LogoLight;

        if (vm.LogoDarkFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.LogoDarkFile))
            {
                ModelState.AddModelError(nameof(vm.LogoDarkFile), "Please upload a valid image.");
                return View(vm);
            }
            var stored = await _files.SaveAsync(vm.LogoDarkFile, "site");
            await _files.DeleteAsync(e.LogoDark);
            e.LogoDark = stored.WebPath;
        }
        else e.LogoDark = vm.LogoDark;

        // Contact
        e.Email = vm.Email; e.Phone = vm.Phone; e.WhatsApp = vm.WhatsApp;
        e.Location = vm.Location; e.LocationAr = vm.LocationAr;
        e.CopyrightText = vm.CopyrightText; e.CopyrightTextAr = vm.CopyrightTextAr;

        // SEO
        e.DefaultMetaTitle = vm.DefaultMetaTitle; e.DefaultMetaDescription = vm.DefaultMetaDescription;
        e.DefaultOgImage = vm.DefaultOgImage;

        // Social
        e.InstagramUrl = vm.InstagramUrl; e.TikTokUrl = vm.TikTokUrl; e.FacebookUrl = vm.FacebookUrl;
        e.SnapchatUrl = vm.SnapchatUrl; e.LinkedInUrl = vm.LinkedInUrl; e.YouTubeUrl = vm.YouTubeUrl;

        // Booking
        e.BookingEmail = vm.BookingEmail; e.BookingWhatsApp = vm.BookingWhatsApp;

        // Analytics
        e.GoogleAnalyticsId = vm.GoogleAnalyticsId; e.GoogleTagManagerId = vm.GoogleTagManagerId;

        // Advanced — SuperAdmin only
        if (User.IsInRole(Roles.SuperAdmin))
        {
            e.CustomCss = vm.CustomCss; e.CustomJs = vm.CustomJs;
        }

        if (!isNew) e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(SiteSettings), e.Id.ToString(), e.SiteName);
        Flash("Site settings saved.");
        return View(ToVm(e));
    }

    private static SiteSettingsFormVm ToVm(SiteSettings? e) => new()
    {
        Id = e?.Id ?? 0,
        SiteName = e?.SiteName ?? "", SiteNameAr = e?.SiteNameAr,
        BrandName = e?.BrandName ?? "", BrandNameAr = e?.BrandNameAr,
        BrandNameShort = e?.BrandNameShort, BrandNameShortAr = e?.BrandNameShortAr,
        Role = e?.Role, RoleAr = e?.RoleAr,
        Tagline = e?.Tagline, TaglineAr = e?.TaglineAr,
        Description = e?.Description, DescriptionAr = e?.DescriptionAr,
        Logo = e?.Logo, LogoLight = e?.LogoLight, LogoDark = e?.LogoDark, Favicon = e?.Favicon,
        Email = e?.Email, Phone = e?.Phone, WhatsApp = e?.WhatsApp,
        Location = e?.Location, LocationAr = e?.LocationAr,
        CopyrightText = e?.CopyrightText, CopyrightTextAr = e?.CopyrightTextAr,
        DefaultMetaTitle = e?.DefaultMetaTitle, DefaultMetaDescription = e?.DefaultMetaDescription,
        DefaultOgImage = e?.DefaultOgImage,
        InstagramUrl = e?.InstagramUrl, TikTokUrl = e?.TikTokUrl, FacebookUrl = e?.FacebookUrl,
        SnapchatUrl = e?.SnapchatUrl, LinkedInUrl = e?.LinkedInUrl, YouTubeUrl = e?.YouTubeUrl,
        BookingEmail = e?.BookingEmail, BookingWhatsApp = e?.BookingWhatsApp,
        GoogleAnalyticsId = e?.GoogleAnalyticsId, GoogleTagManagerId = e?.GoogleTagManagerId,
        CustomCss = e?.CustomCss, CustomJs = e?.CustomJs
    };
}
