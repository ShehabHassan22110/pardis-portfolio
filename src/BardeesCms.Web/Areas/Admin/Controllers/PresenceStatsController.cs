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
public class PresenceStatsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public PresenceStatsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.PresenceStats.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Label.Contains(s) || (x.LabelAr != null && x.LabelAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "label" => query.OrderBy(x => x.Label),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<PresenceStat>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new PresenceStatFormVm
    {
        DisplayOrder = (_db.PresenceStats.Max(p => (int?)p.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PresenceStatFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new PresenceStat();
        Map(vm, e);
        _db.PresenceStats.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(PresenceStat), e.Id.ToString(), e.Label);
        Flash($"Stat “{e.Label}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.PresenceStats.FindAsync(id);
        if (e is null) return NotFound();
        return View(new PresenceStatFormVm
        {
            Id = e.Id, Value = e.Value, Label = e.Label, LabelAr = e.LabelAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PresenceStatFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.PresenceStats.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(PresenceStat), e.Id.ToString(), e.Label);
        Flash($"Stat “{e.Label}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.PresenceStats.FindAsync(id);
        if (e is null) return NotFound();
        _db.PresenceStats.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(PresenceStat), id.ToString(), e.Label);
        Flash($"Stat “{e.Label}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.PresenceStats.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.PresenceStats.Where(p => ids.Contains(p.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(PresenceStatFormVm vm, PresenceStat e)
    {
        e.Value = vm.Value; e.Label = vm.Label; e.LabelAr = vm.LabelAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
