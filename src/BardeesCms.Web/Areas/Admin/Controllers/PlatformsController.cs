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
public class PlatformsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public PlatformsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Platforms.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || (x.NameAr != null && x.NameAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "name" => query.OrderBy(x => x.Name),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<Platform>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new PlatformFormVm
    {
        DisplayOrder = (_db.Platforms.Max(p => (int?)p.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PlatformFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new Platform { CreatedAt = DateTime.UtcNow };
        Map(vm, e);
        _db.Platforms.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Platform), e.Id.ToString(), e.Name);
        Flash($"Platform “{e.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Platforms.FindAsync(id);
        if (e is null) return NotFound();
        return View(new PlatformFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr, Username = e.Username, Url = e.Url, Icon = e.Icon,
            Followers = e.Followers, Description = e.Description, DescriptionAr = e.DescriptionAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PlatformFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.Platforms.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Platform), e.Id.ToString(), e.Name);
        Flash($"Platform “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Platforms.FindAsync(id);
        if (e is null) return NotFound();
        _db.Platforms.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Platform), id.ToString(), e.Name);
        Flash($"Platform “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Platforms.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Platforms.Where(p => ids.Contains(p.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(PlatformFormVm vm, Platform e)
    {
        e.Name = vm.Name; e.NameAr = vm.NameAr; e.Username = vm.Username; e.Url = vm.Url; e.Icon = vm.Icon;
        e.Followers = vm.Followers; e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
