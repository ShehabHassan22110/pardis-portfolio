using BardeesCms.Web.Authorization;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageUsers)]
public class ActivityLogController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    public ActivityLogController(ApplicationDbContext db) { _db = db; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.ActivityLogs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x =>
                x.Action.Contains(s) ||
                (x.EntityName != null && x.EntityName.Contains(s)) ||
                (x.UserName != null && x.UserName.Contains(s)) ||
                (x.Description != null && x.Description.Contains(s)));
        }

        query = query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id);

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<ActivityLog>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = Roles.SuperAdmin)]
    public async Task<IActionResult> Clear()
    {
        await _db.ActivityLogs.ExecuteDeleteAsync();
        Flash("Activity log cleared.");
        return RedirectToAction(nameof(Index));
    }
}
