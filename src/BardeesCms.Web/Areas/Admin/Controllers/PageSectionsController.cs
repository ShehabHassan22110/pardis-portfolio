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
public class PageSectionsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public PageSectionsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.PageSections.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Key.Contains(s) || (x.Title != null && x.Title.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Key),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<PageSection>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new PageSectionFormVm
    {
        DisplayOrder = (_db.PageSections.Max(p => (int?)p.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PageSectionFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        if (await _db.PageSections.AnyAsync(p => p.Key == vm.Key))
        {
            ModelState.AddModelError(nameof(vm.Key), "That key is already in use.");
            return View("Edit", vm);
        }
        var entity = new PageSection { Key = vm.Key, CreatedAt = DateTime.UtcNow };
        Map(vm, entity);
        _db.PageSections.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(PageSection), entity.Id.ToString(), entity.Key);
        Flash($"Section “{entity.Key}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.PageSections.FindAsync(id);
        if (e is null) return NotFound();
        return View(new PageSectionFormVm
        {
            Id = e.Id, Key = e.Key, Page = e.Page,
            Eyebrow = e.Eyebrow, EyebrowAr = e.EyebrowAr,
            Title = e.Title, TitleAr = e.TitleAr, Note = e.Note, NoteAr = e.NoteAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PageSectionFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.PageSections.FindAsync(vm.Id);
        if (e is null) return NotFound();
        // Key is immutable once created — never remapped from the (read-only) input.
        Map(vm, e);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(PageSection), e.Id.ToString(), e.Key);
        Flash($"Section “{e.Key}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.PageSections.FindAsync(id);
        if (e is null) return NotFound();
        _db.PageSections.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(PageSection), id.ToString(), e.Key);
        Flash($"Section “{e.Key}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.PageSections.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.PageSections.Where(p => ids.Contains(p.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            (items.FirstOrDefault(x => x.Id == ids[i]) ?? new()).DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    // Note: Key is intentionally omitted here — it is set on create and immutable thereafter.
    private static void Map(PageSectionFormVm vm, PageSection e)
    {
        e.Page = vm.Page;
        e.Eyebrow = vm.Eyebrow; e.EyebrowAr = vm.EyebrowAr;
        e.Title = vm.Title; e.TitleAr = vm.TitleAr; e.Note = vm.Note; e.NoteAr = vm.NoteAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
