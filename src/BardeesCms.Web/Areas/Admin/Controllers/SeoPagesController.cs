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

[Authorize(Policy = Policies.ManageSeo)]
public class SeoPagesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public SeoPagesController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.SeoPages.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.PageName.Contains(s) || x.Route.Contains(s) || (x.MetaTitle != null && x.MetaTitle.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "name" => query.OrderBy(x => x.PageName),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Route).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<SeoPage>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new SeoPageFormVm { IsActive = true, Robots = "index,follow" });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SeoPageFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var route = await UniqueRoute(vm.Route, null);
        if (route is null) { ModelState.AddModelError(nameof(vm.Route), "Another page already uses this route."); return View("Edit", vm); }
        var e = new SeoPage { CreatedAt = DateTime.UtcNow };
        Map(vm, e, route);
        _db.SeoPages.Add(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(SeoPage), e.Id.ToString(), e.PageName);
        Flash($"SEO page “{e.PageName}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.SeoPages.FindAsync(id);
        if (e is null) return NotFound();
        return View(new SeoPageFormVm
        {
            Id = e.Id, PageName = e.PageName, Route = e.Route,
            MetaTitle = e.MetaTitle, MetaTitleAr = e.MetaTitleAr,
            MetaDescription = e.MetaDescription, MetaDescriptionAr = e.MetaDescriptionAr,
            Keywords = e.Keywords, CanonicalUrl = e.CanonicalUrl,
            OgTitle = e.OgTitle, OgTitleAr = e.OgTitleAr,
            OgDescription = e.OgDescription, OgDescriptionAr = e.OgDescriptionAr,
            OgImage = e.OgImage, Robots = e.Robots, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SeoPageFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.SeoPages.FindAsync(vm.Id);
        if (e is null) return NotFound();
        var route = await UniqueRoute(vm.Route, e.Id);
        if (route is null) { ModelState.AddModelError(nameof(vm.Route), "Another page already uses this route."); return View(vm); }
        Map(vm, e, route);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(SeoPage), e.Id.ToString(), e.PageName);
        Flash($"SEO page “{e.PageName}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.SeoPages.FindAsync(id);
        if (e is null) return NotFound();
        _db.SeoPages.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(SeoPage), id.ToString(), e.PageName);
        Flash($"SEO page “{e.PageName}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.SeoPages.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive; e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    private static void Map(SeoPageFormVm vm, SeoPage e, string route)
    {
        e.PageName = vm.PageName; e.Route = route;
        e.MetaTitle = vm.MetaTitle; e.MetaTitleAr = vm.MetaTitleAr;
        e.MetaDescription = vm.MetaDescription; e.MetaDescriptionAr = vm.MetaDescriptionAr;
        e.Keywords = vm.Keywords; e.CanonicalUrl = vm.CanonicalUrl;
        e.OgTitle = vm.OgTitle; e.OgTitleAr = vm.OgTitleAr;
        e.OgDescription = vm.OgDescription; e.OgDescriptionAr = vm.OgDescriptionAr;
        e.OgImage = vm.OgImage; e.Robots = vm.Robots; e.IsActive = vm.IsActive;
    }

    /// <summary>Normalizes the route and returns it if unique; otherwise null.</summary>
    private async Task<string?> UniqueRoute(string provided, int? excludeId)
    {
        var route = provided.Trim();
        if (!route.StartsWith('/')) route = "/" + route;
        if (route.Length > 1) route = route.TrimEnd('/');
        var clash = await _db.SeoPages.AnyAsync(s => s.Route == route && s.Id != excludeId);
        return clash ? null : route;
    }
}
