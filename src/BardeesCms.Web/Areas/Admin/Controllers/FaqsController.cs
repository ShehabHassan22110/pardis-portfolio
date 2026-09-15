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
public class FaqsController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public FaqsController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.Faqs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Question.Contains(s) || (x.QuestionAr != null && x.QuestionAr.Contains(s)));
        }
        if (q.Filter == "active") query = query.Where(x => x.IsActive);
        else if (q.Filter == "inactive") query = query.Where(x => !x.IsActive);

        query = q.Sort switch
        {
            "title" => query.OrderBy(x => x.Question),
            "recent" => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize)
                               .Take(q.NormalizedPageSize).ToListAsync();

        ViewBag.Result = new PagedResult<Faq>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        return View(items);
    }

    public IActionResult Create() => View("Edit", new FaqFormVm
    {
        DisplayOrder = (_db.Faqs.Max(f => (int?)f.DisplayOrder) ?? -1) + 1,
        IsActive = true
    });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FaqFormVm vm)
    {
        if (!ModelState.IsValid) return View("Edit", vm);
        var entity = new Faq { CreatedAt = DateTime.UtcNow };
        Map(vm, entity);
        _db.Faqs.Add(entity);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Created", nameof(Faq), entity.Id.ToString(), entity.Question);
        Flash($"FAQ “{entity.Question}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var e = await _db.Faqs.FindAsync(id);
        if (e is null) return NotFound();
        return View(new FaqFormVm
        {
            Id = e.Id, Question = e.Question, QuestionAr = e.QuestionAr,
            Answer = e.Answer, AnswerAr = e.AnswerAr,
            DisplayOrder = e.DisplayOrder, IsActive = e.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(FaqFormVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var e = await _db.Faqs.FindAsync(vm.Id);
        if (e is null) return NotFound();
        Map(vm, e);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(Faq), e.Id.ToString(), e.Question);
        Flash($"FAQ “{e.Question}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Faqs.FindAsync(id);
        if (e is null) return NotFound();
        _db.Faqs.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(Faq), id.ToString(), e.Question);
        Flash($"FAQ “{e.Question}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var e = await _db.Faqs.FindAsync(id);
        if (e is null) return NotFound();
        e.IsActive = !e.IsActive;
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(new { e.IsActive });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder([FromBody] int[] ids)
    {
        var items = await _db.Faqs.Where(f => ids.Contains(f.Id)).ToListAsync();
        for (var i = 0; i < ids.Length; i++)
            if (items.FirstOrDefault(x => x.Id == ids[i]) is { } hit) hit.DisplayOrder = i;
        await _db.SaveChangesAsync();
        return Ok();
    }

    private static void Map(FaqFormVm vm, Faq e)
    {
        e.Question = vm.Question; e.QuestionAr = vm.QuestionAr;
        e.Answer = vm.Answer; e.AnswerAr = vm.AnswerAr;
        e.DisplayOrder = vm.DisplayOrder; e.IsActive = vm.IsActive;
    }
}
