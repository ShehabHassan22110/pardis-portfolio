using System.Text.RegularExpressions;
using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;
using BardeesCms.Web.Models.ViewModels;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageContent)]
public class VideosController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public VideosController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Videos.AsNoTracking().Include(x => x.VideoCategory).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Title.Contains(s) || (x.TitleAr != null && x.TitleAr.Contains(s)));
        }
        if (q.Filter == "published") query = query.Where(x => x.IsPublished);
        else if (q.Filter == "draft") query = query.Where(x => !x.IsPublished);
        else if (q.Filter == "featured") query = query.Where(x => x.IsFeatured);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Title),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<Video>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create()
    {
        LoadCategories(null);
        return View("Edit", new VideoFormVm
        {
            DisplayOrder = (_db.Videos.Max(v => (int?)v.DisplayOrder) ?? -1) + 1,
            IsPublished = true
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VideoFormVm vm)
    {
        if (!ModelState.IsValid) { LoadCategories(vm.VideoCategoryId); return View("Edit", vm); }
        var e = new Video { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { LoadCategories(vm.VideoCategoryId); return View("Edit", vm); }
        _db.Videos.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Video), e.Id.ToString(), e.Title);
        Flash($"Video “{e.Title}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Videos.FindAsync(id);
        if (e is null) return NotFound();
        LoadCategories(e.VideoCategoryId);
        return View(new VideoFormVm
        {
            Id = e.Id, Title = e.Title, TitleAr = e.TitleAr, Description = e.Description, DescriptionAr = e.DescriptionAr,
            Client = e.Client, ClientAr = e.ClientAr, Provider = e.Provider, VideoUrl = e.VideoUrl,
            ProviderVideoId = e.ProviderVideoId, VideoFile = e.VideoFile, Thumbnail = e.Thumbnail,
            Duration = e.Duration, IsPortrait = e.IsPortrait, VideoCategoryId = e.VideoCategoryId,
            DisplayOrder = e.DisplayOrder, IsFeatured = e.IsFeatured, IsPublished = e.IsPublished
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VideoFormVm vm)
    {
        if (!ModelState.IsValid) { LoadCategories(vm.VideoCategoryId); return View(vm); }
        var e = await _db.Videos.FindAsync(vm.Id);
        if (e is null) return NotFound();
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { LoadCategories(vm.VideoCategoryId); return View(vm); }
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Video), e.Id.ToString(), e.Title);
        Flash($"Video “{e.Title}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Videos.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.VideoFile);
        await _files.DeleteAsync(e.Thumbnail);
        _db.Videos.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Video), id.ToString(), e.Title);
        Flash($"Video “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Videos.FindAsync(id);
        if (e is null) return NotFound();
        e.IsPublished = !e.IsPublished; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsPublished });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Videos.Where(v => ids.Contains(v.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(VideoFormVm vm, Video e)
    {
        e.Title = vm.Title; e.TitleAr = vm.TitleAr; e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Client = vm.Client; e.ClientAr = vm.ClientAr; e.Provider = vm.Provider; e.VideoUrl = vm.VideoUrl;
        e.Duration = vm.Duration; e.IsPortrait = vm.IsPortrait; e.VideoCategoryId = vm.VideoCategoryId;
        e.DisplayOrder = vm.DisplayOrder; e.IsFeatured = vm.IsFeatured; e.IsPublished = vm.IsPublished;
        e.ProviderVideoId = ParseProviderId(vm.Provider, vm.VideoUrl, vm.ProviderVideoId);

        // MP4 upload — only meaningful when Provider is Mp4.
        if (vm.VideoFileUpload is { Length: > 0 })
        {
            if (!_files.IsAllowedVideo(vm.VideoFileUpload))
            {
                ModelState.AddModelError(nameof(vm.VideoFileUpload), "Please upload a valid video (MP4).");
                return;
            }
            var stored = await _files.SaveAsync(vm.VideoFileUpload, "videos");
            await _files.DeleteAsync(e.VideoFile);
            e.VideoFile = stored.WebPath;
        }
        else
        {
            e.VideoFile = vm.VideoFile; // keep existing
        }

        if (vm.ThumbnailFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ThumbnailFile))
            {
                ModelState.AddModelError(nameof(vm.ThumbnailFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.ThumbnailFile, "videos");
            await _files.DeleteAsync(e.Thumbnail);
            e.Thumbnail = stored.WebPath;
        }
        else
        {
            e.Thumbnail = vm.Thumbnail; // keep existing
        }
    }

    /// <summary>Extracts the provider video id from a URL when not supplied explicitly.</summary>
    private static string? ParseProviderId(VideoProvider provider, string? url, string? explicitId)
    {
        if (!string.IsNullOrWhiteSpace(explicitId)) return explicitId.Trim();
        if (string.IsNullOrWhiteSpace(url)) return null;
        var u = url.Trim();

        if (provider == VideoProvider.YouTube)
        {
            var m = Regex.Match(u, @"(?:youtu\.be/|watch\?v=|/embed/|/shorts/)([A-Za-z0-9_-]{6,})",
                RegexOptions.IgnoreCase);
            if (m.Success) return m.Groups[1].Value;
        }
        else if (provider == VideoProvider.Vimeo)
        {
            var m = Regex.Match(u, @"vimeo\.com/(?:video/)?(\d+)", RegexOptions.IgnoreCase);
            if (m.Success) return m.Groups[1].Value;
        }
        return null;
    }

    private void LoadCategories(int? _)
        => ViewBag.Categories = _db.VideoCategories.AsNoTracking()
            .OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name).ToList();
}
