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

[Authorize(Policy = Policies.ManageMedia)]
public class MediaLibraryController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public MediaLibraryController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.MediaAssets.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => (x.OriginalFileName != null && x.OriginalFileName.Contains(s)) || x.FileName.Contains(s));
        }
        if (Enum.TryParse<MediaType>(q.Filter, true, out var mt)) query = query.Where(x => x.MediaType == mt);

        query = query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id);

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<MediaAsset>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [RequestSizeLimit(500_000_000)]
    public async Task<IActionResult> Upload(IFormFile[] files)
    {
        if (files is null || files.Length == 0)
        {
            Flash("No files were selected.", "danger");
            return RedirectToAction(nameof(Index));
        }

        var saved = 0;
        foreach (var file in files)
        {
            if (file.Length == 0) continue;
            var isImage = _files.IsAllowedImage(file);
            var isVideo = _files.IsAllowedVideo(file);
            var type = isImage ? MediaType.Image : isVideo ? MediaType.Video : MediaType.Document;

            StoredFile stored;
            try { stored = await _files.SaveAsync(file, "library"); }
            catch { Flash($"“{file.FileName}” could not be uploaded (unsupported type).", "danger"); continue; }

            _db.MediaAssets.Add(new MediaAsset
            {
                FileName = stored.FileName, OriginalFileName = file.FileName, FilePath = stored.WebPath,
                ThumbnailPath = stored.ThumbnailPath,
                MediaType = type, MimeType = stored.MimeType, FileSize = stored.Size,
                Width = stored.Width, Height = stored.Height, Folder = "library", CreatedAt = DateTime.UtcNow
            });
            saved++;
        }

        if (saved > 0)
        {
            await _db.SaveChangesAsync();
            await _log.LogAsync("Uploaded", nameof(MediaAsset), null, $"{saved} file(s) to the media library");
            Flash($"{saved} file(s) uploaded.");
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.MediaAssets.FindAsync(id);
        if (e is null) return NotFound();
        return View(new MediaEditVm
        {
            Id = e.Id, FilePath = e.FilePath, ThumbnailPath = e.ThumbnailPath, OriginalFileName = e.OriginalFileName,
            AltText = e.AltText, AltTextAr = e.AltTextAr, Caption = e.Caption, CaptionAr = e.CaptionAr
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    [RequestSizeLimit(500_000_000)]
    public async Task<IActionResult> Edit(MediaEditVm vm)
    {
        var e = await _db.MediaAssets.FindAsync(vm.Id);
        if (e is null) return NotFound();

        // Optional file replacement — swap the stored file, keep the DB record + URL references.
        if (vm.ReplacementFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ReplacementFile) && !_files.IsAllowedVideo(vm.ReplacementFile))
                ModelState.AddModelError(nameof(vm.ReplacementFile), "Unsupported file type.");
            if (!ModelState.IsValid) { vm.FilePath = e.FilePath; vm.ThumbnailPath = e.ThumbnailPath; return View(vm); }

            var oldPath = e.FilePath;
            var stored = await _files.SaveAsync(vm.ReplacementFile, "library");
            e.FilePath = stored.WebPath; e.FileName = stored.FileName; e.OriginalFileName = vm.ReplacementFile.FileName;
            e.ThumbnailPath = stored.ThumbnailPath; e.MimeType = stored.MimeType; e.FileSize = stored.Size;
            e.Width = stored.Width; e.Height = stored.Height;
            e.MediaType = _files.IsAllowedVideo(vm.ReplacementFile) ? MediaType.Video : MediaType.Image;
            await _files.DeleteAsync(oldPath);
        }

        e.AltText = vm.AltText; e.AltTextAr = vm.AltTextAr; e.Caption = vm.Caption; e.CaptionAr = vm.CaptionAr;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(MediaAsset), e.Id.ToString(), e.OriginalFileName ?? e.FileName);
        Flash("Media updated.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.MediaAssets.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.FilePath);
        _db.MediaAssets.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(MediaAsset), id.ToString(), e.OriginalFileName ?? e.FileName);
        Flash($"“{e.OriginalFileName ?? e.FileName}” deleted.");
        return RedirectToAction(nameof(Index));
    }
}
