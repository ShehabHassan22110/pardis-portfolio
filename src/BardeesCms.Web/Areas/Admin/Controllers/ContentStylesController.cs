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
public class ContentStylesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public ContentStylesController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.ContentStyles.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || (x.NameAr != null && x.NameAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Name),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<ContentStyle>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new ContentStyleFormVm
    {
        DisplayOrder = (_db.ContentStyles.Max(c => (int?)c.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContentStyleFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var entity = new ContentStyle();
        Map(vm, entity);
        _db.ContentStyles.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(ContentStyle), entity.Id.ToString(), entity.Name);
        Flash($"Content style “{entity.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.ContentStyles.FindAsync(id);
        if (e is null) return NotFound();
        return View(new ContentStyleFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ContentStyleFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.ContentStyles.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(ContentStyle), e.Id.ToString(), e.Name);
        Flash($"Content style “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.ContentStyles.FindAsync(id);
        if (e is null) return NotFound();
        _db.ContentStyles.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(ContentStyle), id.ToString(), e.Name);
        Flash($"Content style “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.ContentStyles.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.ContentStyles.Where(c => ids.Contains(c.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            (items.FirstOrDefault(x => x.Id == ids[i]) ?? new()).DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(ContentStyleFormVm vm, ContentStyle e)
    {
        e.Name = vm.Name; e.NameAr = vm.NameAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
