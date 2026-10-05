using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.ViewModels;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageContent)]
public class AbayasController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public AbayasController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Abayas.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || (x.NameAr != null && x.NameAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "hidden") query = query.Where(x => !x.IsActive);
        else if (q.Filter == "featured") query = query.Where(x => x.IsFeatured);

        query = q.Sort switch
        {
            "name" => query.OrderBy(x => x.Name),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<Abaya>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() =>
        View("Edit", new AbayaFormVm
        {
            DisplayOrder = (_db.Abayas.Max(a => (int?)a.DisplayOrder) ?? -1) + 1,
            IsActive = true
        });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AbayaFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new Abaya { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) return View("Edit", vm);
        _db.Abayas.Add(e);
        await _db.SaveChangesAsync();
        await AppendGalleryAsync(vm.GalleryFiles, e.Id);
        await _log.LogAsync("Created", nameof(Abaya), e.Id.ToString(), e.Name);
        Flash($"Abaya “{e.Name}” created.");
        return RedirectToAction(nameof(Edit), new { id = e.Id });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Abayas.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == id);
        if (e is null) return NotFound();
        return View(new AbayaFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr, Slug = e.Slug,
            Description = e.Description, DescriptionAr = e.DescriptionAr,
            Fabric = e.Fabric, FabricAr = e.FabricAr, Type = e.Type,
            CoverImage = e.CoverImage, VideoUrl = e.VideoUrl, DisplayOrder = e.DisplayOrder,
            IsFeatured = e.IsFeatured, IsActive = e.IsActive,
            Images = e.Images.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToList()
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AbayaFormVm vm)
    {
        var e = await _db.Abayas.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == vm.Id);
        if (e is null) return NotFound();
        if (!ModelState.IsValid) { vm.Images = CurrentImages(e); return View(vm); }
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { vm.Images = CurrentImages(e); return View(vm); }
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await AppendGalleryAsync(vm.GalleryFiles, e.Id);
        await _log.LogAsync("Updated", nameof(Abaya), e.Id.ToString(), e.Name);
        Flash($"Abaya “{e.Name}” saved.");
        return RedirectToAction(nameof(Edit), new { id = e.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Abayas.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.CoverImage);
        foreach (var m in e.Images) { await _files.DeleteAsync(m.FilePath); }
        _db.Abayas.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Abaya), id.ToString(), e.Name);
        Flash($"Abaya “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var m = await _db.AbayaImages.FindAsync(id);
        if (m is null) return NotFound();
        var abayaId = m.AbayaId;
        await _files.DeleteAsync(m.FilePath);
        _db.AbayaImages.Remove(m);
        await _db.SaveChangesAsync();
        Flash("Image removed.");
        return RedirectToAction(nameof(Edit), new { id = abayaId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ReorderImages([FromBody] int[] ids)
    {
        var items = await _db.AbayaImages.Where(m => ids.Contains(m.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Abayas.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { IsActive = e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Abayas.Where(a => ids.Contains(a.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(AbayaFormVm vm, Abaya e)
    {
        e.Slug = await UniqueSlug(vm.Slug, vm.Name, e.Id == 0 ? null : e.Id);
        e.Name = vm.Name; e.NameAr = vm.NameAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Fabric = vm.Fabric; e.FabricAr = vm.FabricAr; e.Type = vm.Type;
        e.VideoUrl = string.IsNullOrWhiteSpace(vm.VideoUrl) ? null : vm.VideoUrl.Trim();
        e.DisplayOrder = vm.DisplayOrder; e.IsFeatured = vm.IsFeatured; e.IsActive = vm.IsActive;

        if (vm.CoverImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.CoverImageFile))
            {
                ModelState.AddModelError(nameof(vm.CoverImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.CoverImageFile, "abayas");
            await _files.DeleteAsync(e.CoverImage);
            e.CoverImage = stored.WebPath;
        }
        else { e.CoverImage = vm.CoverImage; }
        e.ThumbnailImage = e.CoverImage;
    }

    /// <summary>Store each uploaded gallery image and append AbayaImage rows.</summary>
    private async Task AppendGalleryAsync(IFormFile[]? files, int abayaId)
    {
        if (files is null || files.Length == 0) return;
        var order = (await _db.AbayaImages.Where(m => m.AbayaId == abayaId)
                                          .MaxAsync(m => (int?)m.DisplayOrder) ?? -1) + 1;
        foreach (var file in files)
        {
            if (file is not { Length: > 0 } || !_files.IsAllowedImage(file)) continue;
            var stored = await _files.SaveAsync(file, "abayas");
            _db.AbayaImages.Add(new AbayaImage { AbayaId = abayaId, FilePath = stored.WebPath, DisplayOrder = order++ });
        }
        await _db.SaveChangesAsync();
    }

    private static List<AbayaImage> CurrentImages(Abaya e) =>
        e.Images.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToList();

    private async Task<string> UniqueSlug(string? provided, string name, int? excludeId)
    {
        var baseSlug = SlugHelper.Slugify(string.IsNullOrWhiteSpace(provided) ? name : provided);
        var slug = baseSlug; var i = 2;
        while (await _db.Abayas.AnyAsync(a => a.Slug == slug && a.Id != excludeId))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }
}
