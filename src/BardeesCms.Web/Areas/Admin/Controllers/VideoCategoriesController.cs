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
public class VideoCategoriesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public VideoCategoriesController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.VideoCategories.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || x.Key.Contains(s) || (x.NameAr != null && x.NameAr.Contains(s)));
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

        ViewBag.Result = new PagedResult<VideoCategory>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new VideoCategoryFormVm
    {
        DisplayOrder = (_db.VideoCategories.Max(c => (int?)c.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VideoCategoryFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        if (await _db.VideoCategories.AnyAsync(c => c.Key == vm.Key))
        {
            ModelState.AddModelError(nameof(vm.Key), "That key is already in use.");
            return View("Edit", vm);
        }
        var entity = new VideoCategory();
        Map(vm, entity);
        _db.VideoCategories.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(VideoCategory), entity.Id.ToString(), entity.Name);
        Flash($"Category “{entity.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.VideoCategories.FindAsync(id);
        if (e is null) return NotFound();
        return View(new VideoCategoryFormVm
        {
            Id = e.Id, Key = e.Key, Name = e.Name, NameAr = e.NameAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VideoCategoryFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.VideoCategories.FindAsync(vm.Id);
        if (e is null) return NotFound();
        if (await _db.VideoCategories.AnyAsync(c => c.Key == vm.Key && c.Id != vm.Id))
        {
            ModelState.AddModelError(nameof(vm.Key), "That key is already in use.");
            return View(vm);
        }
        Map(vm, e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(VideoCategory), e.Id.ToString(), e.Name);
        Flash($"Category “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.VideoCategories.FindAsync(id);
        if (e is null) return NotFound();
        _db.VideoCategories.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(VideoCategory), id.ToString(), e.Name);
        Flash($"Category “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.VideoCategories.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.VideoCategories.Where(c => ids.Contains(c.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(VideoCategoryFormVm vm, VideoCategory e)
    {
        e.Key = vm.Key; e.Name = vm.Name; e.NameAr = vm.NameAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
