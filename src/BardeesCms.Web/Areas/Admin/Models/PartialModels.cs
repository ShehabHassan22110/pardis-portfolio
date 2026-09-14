namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Model for the reusable admin page header partial.</summary>
public class PageHeaderModel
{
    public string Title { get; set; } = "";
    public string? Subtitle { get; set; }
    public string? ActionText { get; set; }
    public string? ActionUrl { get; set; }
    public string ActionIcon { get; set; } = "bi-plus-lg";
}

/// <summary>Model for the reusable pager partial. RouteValues preserve search/sort/filter.</summary>
public class PagerModel
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int FirstItem { get; set; }
    public int LastItem { get; set; }
    public string Action { get; set; } = "Index";
    public IDictionary<string, string?> RouteValues { get; set; } = new Dictionary<string, string?>();
}
