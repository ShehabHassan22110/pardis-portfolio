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
public class CollaborationStepsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public CollaborationStepsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.CollaborationSteps.AsNoTracking().AsQueryable();
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
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<CollaborationStep>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new CollaborationStepFormVm
    {
        DisplayOrder = (_db.CollaborationSteps.Max(s => (int?)s.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CollaborationStepFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var entity = new CollaborationStep { CreatedAt = DateTime.UtcNow };
        Map(vm, entity);
        _db.CollaborationSteps.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(CollaborationStep), entity.Id.ToString(), entity.Title);
        Flash($"Step “{entity.Title}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.CollaborationSteps.FindAsync(id);
        if (e is null) return NotFound();
        return View(new CollaborationStepFormVm
        {
            Id = e.Id, StepNumber = e.StepNumber, Title = e.Title, TitleAr = e.TitleAr,
            Description = e.Description, DescriptionAr = e.DescriptionAr, Icon = e.Icon,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CollaborationStepFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.CollaborationSteps.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(CollaborationStep), e.Id.ToString(), e.Title);
        Flash($"Step “{e.Title}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.CollaborationSteps.FindAsync(id);
        if (e is null) return NotFound();
        _db.CollaborationSteps.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(CollaborationStep), id.ToString(), e.Title);
        Flash($"Step “{e.Title}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.CollaborationSteps.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.CollaborationSteps.Where(s => ids.Contains(s.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(CollaborationStepFormVm vm, CollaborationStep e)
    {
        e.StepNumber = vm.StepNumber; e.Title = vm.Title; e.TitleAr = vm.TitleAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr; e.Icon = vm.Icon;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
