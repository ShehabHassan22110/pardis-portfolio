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

[Authorize(Policy = Policies.ManageMessages)]
public class ContactMessagesController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IActivityLogger _log;
    public ContactMessagesController(ApplicationDbContext db, IActivityLogger log) { _db = db; _log = log; }

    public async Task<IActionResult> Index(ListQuery q)
    {
        var query = _db.ContactMessages.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim();
            query = query.Where(x => x.Name.Contains(s) || x.Email.Contains(s) || (x.Subject != null && x.Subject.Contains(s)));
        }
        if (Enum.TryParse<MessageStatus>(q.Filter, true, out var status)) query = query.Where(x => x.Status == status);

        query = query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id);

        var total = await query.CountAsync();
        var items = await query.Skip((q.NormalizedPage - 1) * q.NormalizedPageSize).Take(q.NormalizedPageSize).ToListAsync();
        ViewBag.Result = new PagedResult<ContactMessage>
        {
            Items = items, Page = q.NormalizedPage, PageSize = q.NormalizedPageSize,
            TotalItems = total, Search = q.Search, Sort = q.Sort, Filter = q.Filter
        };
        ViewBag.UnreadCount = await _db.ContactMessages.CountAsync(x => x.Status == MessageStatus.Unread);
        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var e = await _db.ContactMessages.FindAsync(id);
        if (e is null) return NotFound();
        if (e.Status == MessageStatus.Unread)
        {
            e.IsRead = true;
            e.Status = MessageStatus.Read;
            await _db.SaveChangesAsync();
        }
        return View(e);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetStatus(int id, MessageStatus status)
    {
        var e = await _db.ContactMessages.FindAsync(id);
        if (e is null) return NotFound();
        e.Status = status;
        if (status != MessageStatus.Unread) e.IsRead = true;
        await _db.SaveChangesAsync();
        await _log.LogAsync("Updated", nameof(ContactMessage), id.ToString(), $"Status → {status} ({e.Name})");
        Flash($"Message from “{e.Name}” marked {status}.");
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.ContactMessages.FindAsync(id);
        if (e is null) return NotFound();
        _db.ContactMessages.Remove(e);
        await _db.SaveChangesAsync();
        await _log.LogAsync("Deleted", nameof(ContactMessage), id.ToString(), $"{e.Name} <{e.Email}>");
        Flash($"Message from “{e.Name}” deleted.");
        return RedirectToAction(nameof(Index));
    }
}
