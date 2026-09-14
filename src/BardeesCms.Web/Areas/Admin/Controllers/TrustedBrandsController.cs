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
public class TrustedBrandsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public TrustedBrandsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.TrustedBrands.AsNoTracking().AsQueryable();
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
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<TrustedBrand>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new TrustedBrandFormVm
    {
        DisplayOrder = (_db.TrustedBrands.Max(t => (int?)t.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrustedBrandFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var e = new TrustedBrand();
        Map(vm, e);
        _db.TrustedBrands.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(TrustedBrand), e.Id.ToString(), e.Name);
        Flash($"Trusted brand “{e.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.TrustedBrands.FindAsync(id);
        if (e is null) return NotFound();
        return View(new TrustedBrandFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr, Emphasis = e.Emphasis, EmphasisAr = e.EmphasisAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TrustedBrandFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.TrustedBrands.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(TrustedBrand), e.Id.ToString(), e.Name);
        Flash($"Trusted brand “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.TrustedBrands.FindAsync(id);
        if (e is null) return NotFound();
        _db.TrustedBrands.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(TrustedBrand), id.ToString(), e.Name);
        Flash($"Trusted brand “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.TrustedBrands.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.TrustedBrands.Where(t => ids.Contains(t.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(TrustedBrandFormVm vm, TrustedBrand e)
    {
        e.Name = vm.Name; e.NameAr = vm.NameAr; e.Emphasis = vm.Emphasis; e.EmphasisAr = vm.EmphasisAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
