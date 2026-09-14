using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Data for the admin dashboard home.</summary>
public class DashboardViewModel
{
    public int TotalProjects { get; set; }
    public int PublishedProjects { get; set; }
    public int DraftProjects { get; set; }
    public int TotalBrands { get; set; }
    public int TotalServices { get; set; }
    public int TotalVideos { get; set; }
    public int TotalMessages { get; set; }
    public int UnreadMessages { get; set; }
    public int TotalDisciplines { get; set; }
    public int TotalFaqs { get; set; }

    public List<PortfolioProject> RecentProjects { get; set; } = new();
    public List<ContactMessage> RecentMessages { get; set; } = new();
    public List<ActivityLog> RecentActivity { get; set; } = new();
}
