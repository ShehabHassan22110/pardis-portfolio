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
public class NavigationController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public NavigationController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.NavigationItems.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Title.Contains(s) || (x.TitleAr != null && x.TitleAr.Contains(s)) || x.Url.Contains(s));
        }
        if (q.Filter == "header") query = query.Where(x => x.Location == NavLocation.Header);
        else if (q.Filter == "footer") query = query.Where(x => x.Location == NavLocation.Footer);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Title),
            _ => query.OrderBy(x => x.Location).ThenBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<NavigationItem>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new NavigationFormVm
    {
        DisplayOrder = (_db.NavigationItems.Max(n => (int?)n.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NavigationFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new NavigationItem();
        Map(vm, e);
        _db.NavigationItems.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(NavigationItem), e.Id.ToString(), e.Title);
        Flash($"Navigation link “{e.Title}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.NavigationItems.FindAsync(id);
        if (e is null) return NotFound();
        return View(new NavigationFormVm
        {
            Id = e.Id, Title = e.Title, TitleAr = e.TitleAr, Url = e.Url, Target = e.Target,
            Location = e.Location, Group = e.Group, GroupAr = e.GroupAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(NavigationFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.NavigationItems.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(NavigationItem), e.Id.ToString(), e.Title);
        Flash($"Navigation link “{e.Title}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.NavigationItems.FindAsync(id);
        if (e is null) return NotFound();
        _db.NavigationItems.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(NavigationItem), id.ToString(), e.Title);
        Flash($"Navigation link “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.NavigationItems.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.NavigationItems.Where(n => ids.Contains(n.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(NavigationFormVm vm, NavigationItem e)
    {
        e.Title = vm.Title; e.TitleAr = vm.TitleAr; e.Url = vm.Url; e.Target = vm.Target;
        e.Location = vm.Location; e.Group = vm.Group; e.GroupAr = vm.GroupAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
