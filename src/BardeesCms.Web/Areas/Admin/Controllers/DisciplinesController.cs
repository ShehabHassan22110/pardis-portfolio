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
public class DisciplinesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    private readonly IFileStorageService _files;
    public DisciplinesController(ApplicationDbContext db, IActivityLogger log, IFileStorageService files)
    { _db = db; _log = log; _files = files; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Disciplines.AsNoTracking().AsQueryable();
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
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize)
            .Select(d => new DisciplineListItem
            {
                Discipline = d,
                ProjectCount = d.Projects.Count()
            }).ToListAsync();

        ViewBag.Result = new PagedResult<DisciplineListItem>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new DisciplineFormVm
    {
        DisplayOrder = (_db.Disciplines.Max(d => (int?)d.DisplayOrder) ?? -1) + 1,
        IsActive = true,
        SubItems = BlankRows(new())
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DisciplineFormVm vm)
    {
        if (!ModelState.IsValid) { vm.SubItems = BlankRows(vm.SubItems); return View("Edit", vm); }
        var e = new Discipline { CreatedAt = DateTime.UtcNow };
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { vm.SubItems = BlankRows(vm.SubItems); return View("Edit", vm); }
        _db.Disciplines.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Discipline), e.Id.ToString(), e.Name);
        Flash($"Discipline “{e.Name}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Disciplines.Include(d => d.SubItems).FirstOrDefaultAsync(d => d.Id == id);
        if (e is null) return NotFound();
        return View(new DisciplineFormVm
        {
            Id = e.Id, Name = e.Name, NameAr = e.NameAr, Slug = e.Slug, Number = e.Number,
            Tagline = e.Tagline, TaglineAr = e.TaglineAr, Description = e.Description, DescriptionAr = e.DescriptionAr,
            Icon = e.Icon, CoverImage = e.CoverImage, Status = e.Status,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive,
            SubItems = BlankRows(e.SubItems.OrderBy(s => s.DisplayOrder)
                .Select(s => new DisciplineSubItemVm { Name = s.Name, NameAr = s.NameAr }).ToList())
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DisciplineFormVm vm)
    {
        if (!ModelState.IsValid) { vm.SubItems = BlankRows(vm.SubItems); return View(vm); }
        var e = await _db.Disciplines.Include(d => d.SubItems).FirstOrDefaultAsync(d => d.Id == vm.Id);
        if (e is null) return NotFound();
        await MapAsync(vm, e);
        if (!ModelState.IsValid) { vm.SubItems = BlankRows(vm.SubItems); return View(vm); }
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Discipline), e.Id.ToString(), e.Name);
        Flash($"Discipline “{e.Name}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Disciplines.FindAsync(id);
        if (e is null) return NotFound();
        await _files.DeleteAsync(e.CoverImage);
        _db.Disciplines.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Discipline), id.ToString(), e.Name);
        Flash($"Discipline “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Disciplines.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Disciplines.Where(d => ids.Contains(d.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private async Task MapAsync(DisciplineFormVm vm, Discipline e)
    {
        e.Name = vm.Name; e.NameAr = vm.NameAr; e.Slug = await UniqueSlug(vm.Slug, vm.Name, e.Id == 0 ? null : e.Id);
        e.Number = vm.Number; e.Tagline = vm.Tagline; e.TaglineAr = vm.TaglineAr;
        e.Description = vm.Description; e.DescriptionAr = vm.DescriptionAr; e.Icon = vm.Icon;
        e.Status = vm.Status; e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;

        if (vm.CoverImageFile is { Length: > 0 })
        {
            if (!_files.IsAllowedImage(vm.CoverImageFile))
            {
                ModelState.AddModelError(nameof(vm.CoverImageFile), "Please upload a valid image.");
                return;
            }
            var stored = await _files.SaveAsync(vm.CoverImageFile, "disciplines");
            await _files.DeleteAsync(e.CoverImage);
            e.CoverImage = stored.WebPath;
        }
        else
        {
            e.CoverImage = vm.CoverImage; // keep existing
        }

        // Replace sub-items with the non-empty submitted rows.
        e.SubItems.Clear();
        var order = 0;
        foreach (var row in vm.SubItems)
        {
            if (string.IsNullOrWhiteSpace(row.Name) && string.IsNullOrWhiteSpace(row.NameAr)) continue;
            e.SubItems.Add(new DisciplineSubItem
            {
                Name = (row.Name ?? "").Trim(),
                NameAr = string.IsNullOrWhiteSpace(row.NameAr) ? null : row.NameAr.Trim(),
                DisplayOrder = order++
            });
        }
    }

    private async Task<string> UniqueSlug(string? provided, string name, int? excludeId)
    {
        var baseSlug = SlugHelper.Slugify(string.IsNullOrWhiteSpace(provided) ? name : provided);
        var slug = baseSlug; var i = 2;
        while (await _db.Disciplines.AnyAsync(d => d.Slug == slug && d.Id != excludeId))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }

    /// <summary>Pad a sub-item list with a few blank rows so the admin can add more.</summary>
    private static List<DisciplineSubItemVm> BlankRows(List<DisciplineSubItemVm> existing)
    {
        var rows = existing.Where(r => !string.IsNullOrWhiteSpace(r.Name) || !string.IsNullOrWhiteSpace(r.NameAr)).ToList();
        for (var i = 0; i < 3; i++) rows.Add(new DisciplineSubItemVm());
        return rows;
    }
}
