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
public class ServicesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public ServicesController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Services.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Title.Contains(s) || (x.TitleAr != null && x.TitleAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);
        else if (q.Filter == "featured") query = query.Where(x => x.IsFeatured);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Title),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<Service>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new ServiceFormVm
    {
        DisplayOrder = (_db.Services.Max(s => (int?)s.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var slug = await UniqueSlug(vm.Slug, vm.Title, null);
        var entity = new Service { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, entity, slug);
        if (!ModelState.IsValid) return View("Edit", vm);
        _db.Services.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Service), entity.Id.ToString(), entity.Title);
        Flash($"Service “{entity.Title}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Services.FindAsync(id);
        if (e is null) return NotFound();
        return View(new ServiceFormVm
        {
            Id = e.Id, Number = e.Number, Title = e.Title, TitleAr = e.TitleAr, Slug = e.Slug,
            ShortDescription = e.ShortDescription, ShortDescriptionAr = e.ShortDescriptionAr,
            Description = e.Description, DescriptionAr = e.DescriptionAr, Icon = e.Icon, Image = e.Image,
            DisplayOrder = e.DisplayOrder, IsFeatured = e.IsFeatured, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ServiceFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.Services.FindAsync(vm.Id);
        if (e is null) return NotFound();
        e.Slug = await UniqueSlug(vm.Slug, vm.Title, e.Id);
        await MapAsync(vm, e, e.Slug);
        if (!ModelState.IsValid) return View(vm);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Service), e.Id.ToString(), e.Title);
        Flash($"Service “{e.Title}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Services.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.Image);
        _db.Services.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Service), id.ToString(), e.Title);
        Flash($"Service “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Services.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Services.Where(s => ids.Contains(s.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(ServiceFormVm vm, Service e, string slug)
    {
        e.Number = vm.Number; e.Title = vm.Title; e.TitleAr = vm.TitleAr; e.Slug = slug;
        e.ShortDescription = vm.ShortDescription; e.ShortDescriptionAr = vm.ShortDescriptionAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Icon = vm.Icon;
        e.DisplayOrder = vm.DisplayOrder; e.IsFeatured = vm.IsFeatured; e.IsActive = vm.IsActive;

        if (vm.ImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ImageFile))
            {
                ModelState.AddModelError(nameof(vm.ImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.ImageFile, "services");
            await _files.DeleteAsync(e.Image);
            e.Image = stored.WebPath;
        }
        else
        {
            e.Image = vm.Image; // keep existing path (posted via hidden field)
        }
    }

    private async Task<string> UniqueSlug(string? provided, string title, int? excludeId)
    {
        var baseSlug = SlugHelper.Slugify(string.IsNullOrWhiteSpace(provided) ? title : provided);
        var slug = baseSlug; var i = 2;
        while (await _db.Services.AnyAsync(s => s.Slug == slug && s.Id != excludeId))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }
}
