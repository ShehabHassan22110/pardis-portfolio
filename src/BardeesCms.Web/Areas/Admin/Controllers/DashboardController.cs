using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Data;
using BardeesCms.Web.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

public class DashboardController : AdminControllerBase
{
    private readonly ApplicationDbContext _db;
    public DashboardController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var vm = new DashboardViewModel
        {
            TotalProjects = await _db.PortfolioProjects.CountAsync(),
            PublishedProjects = await _db.PortfolioProjects.CountAsync(p => p.IsPublished),
            DraftProjects = await _db.PortfolioProjects.CountAsync(p => !p.IsPublished),
            TotalBrands = await _db.Brands.CountAsync(),
            TotalServices = await _db.Services.CountAsync(),
            TotalVideos = await _db.Videos.CountAsync(),
            TotalMessages = await _db.ContactMessages.CountAsync(),
            UnreadMessages = await _db.ContactMessages.CountAsync(m => m.Status == MessageStatus.Unread),
            TotalDisciplines = await _db.Disciplines.CountAsync(),
            TotalFaqs = await _db.Faqs.CountAsync(),
            RecentProjects = await _db.PortfolioProjects.AsNoTracking()
                .Include(p => p.Discipline)
                .OrderByDescending(p => p.CreatedAt).Take(5).ToListAsync(),
            RecentMessages = await _db.ContactMessages.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt).Take(5).ToListAsync(),
            RecentActivity = await _db.ActivityLogs.AsNoTracking()
                .OrderByDescending(a => a.CreatedAt).Take(8).ToListAsync(),
        };
        return View(vm);
    }
}
