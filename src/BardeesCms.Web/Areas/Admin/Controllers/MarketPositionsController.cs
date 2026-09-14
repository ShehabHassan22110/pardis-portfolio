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
public class MarketPositionsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public MarketPositionsController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.MarketPositions.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Title.Contains(s) || (x.TitleAr != null && x.TitleAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Title),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<MarketPosition>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new MarketPositionFormVm
    {
        DisplayOrder = (_db.MarketPositions.Max(m => (int?)m.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MarketPositionFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new MarketPosition { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) return View("Edit", vm);
        _db.MarketPositions.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(MarketPosition), e.Id.ToString(), e.Title);
        Flash($"Market position “{e.Title}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.MarketPositions.FindAsync(id);
        if (e is null) return NotFound();
        return View(new MarketPositionFormVm
        {
            Id = e.Id, Title = e.Title, TitleAr = e.TitleAr, Description = e.Description, DescriptionAr = e.DescriptionAr,
            Icon = e.Icon, Image = e.Image, Location = e.Location, LocationAr = e.LocationAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MarketPositionFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.MarketPositions.FindAsync(vm.Id);
        if (e is null) return NotFound();
        await MapAsync(vm, e);
        if (!ModelState.IsValid) return View(vm);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(MarketPosition), e.Id.ToString(), e.Title);
        Flash($"Market position “{e.Title}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.MarketPositions.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.Image);
        _db.MarketPositions.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(MarketPosition), id.ToString(), e.Title);
        Flash($"Market position “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.MarketPositions.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.MarketPositions.Where(m => ids.Contains(m.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(MarketPositionFormVm vm, MarketPosition e)
    {
        e.Title = vm.Title; e.TitleAr = vm.TitleAr; e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.Icon = vm.Icon; e.Location = vm.Location; e.LocationAr = vm.LocationAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;

        if (vm.ImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.ImageFile))
            {
                ModelState.AddModelError(nameof(vm.ImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.ImageFile, "market");
            await _files.DeleteAsync(e.Image);
            e.Image = stored.WebPath;
        }
        else
        {
            e.Image = vm.Image; // keep existing
        }
    }
}
