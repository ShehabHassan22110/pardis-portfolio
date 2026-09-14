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
public class BrandsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public BrandsController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Brands.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || (x.NameAr != null && x.NameAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);
        else if (q.Filter == "featured") query = query.Where(x => x.IsFeatured);

        query = q.Sort switch
        {
            "name" => query.OrderBy(x => x.Name),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<Brand>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new BrandFormVm
    {
        DisplayOrder = (_db.Brands.Max(b => (int?)b.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new Brand { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) return View("Edit", vm);
        _db.Brands.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Brand), e.Id.ToString(), e.Name);
        Flash($"Brand “{e.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Brands.FindAsync(id);
        if (e is null) return NotFound();
        return View(new BrandFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr, Role = e.Role, RoleAr = e.RoleAr,
            Sector = e.Sector, SectorAr = e.SectorAr, Description = e.Description, DescriptionAr = e.DescriptionAr,
            WebsiteUrl = e.WebsiteUrl, Logo = e.Logo, DisplayOrder = e.DisplayOrder, IsFeatured = e.IsFeatured, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BrandFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.Brands.FindAsync(vm.Id);
        if (e is null) return NotFound();
        await MapAsync(vm, e);
        if (!ModelState.IsValid) return View(vm);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Brand), e.Id.ToString(), e.Name);
        Flash($"Brand “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Brands.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.Logo);
        _db.Brands.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Brand), id.ToString(), e.Name);
        Flash($"Brand “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Brands.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Brands.Where(b => ids.Contains(b.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(BrandFormVm vm, Brand e)
    {
        e.Name = vm.Name; e.NameAr = vm.NameAr; e.Role = vm.Role; e.RoleAr = vm.RoleAr;
        e.Sector = vm.Sector; e.SectorAr = vm.SectorAr; e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.WebsiteUrl = vm.WebsiteUrl; e.DisplayOrder = vm.DisplayOrder; e.IsFeatured = vm.IsFeatured; e.IsActive = vm.IsActive;

        if (vm.LogoFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.LogoFile))
            {
                ModelState.AddModelError(nameof(vm.LogoFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.LogoFile, "brands");
            await _files.DeleteAsync(e.Logo);
            e.Logo = stored.WebPath;
        }
        else
        {
            e.Logo = vm.Logo; // keep existing
        }
    }
}
