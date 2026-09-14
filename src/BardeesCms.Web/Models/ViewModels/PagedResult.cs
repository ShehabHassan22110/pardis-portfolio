namespace BardeesCms.Web.Models.ViewModels;

/// <summary>A page of results plus the query state that produced it (for list views).</summary>
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public int TotalItems { get; init; }
    public int TotalPages => PageSize <= 0 ? 1 : (int)Math.Ceiling(TotalItems / (double)PageSize);

    public string? Search { get; set; }
    public string? Sort { get; set; }
    public string? Filter { get; set; }

    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
    public int FirstItem => TotalItems == 0 ? 0 : (Page - 1) * PageSize + 1;
    public int LastItem => Math.Min(Page * PageSize, TotalItems);
}

/// <summary>Common query parameters bound from the list view's toolbar.</summary>
public class ListQuery
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public string? Filter { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public int NormalizedPage => Page < 1 ? 1 : Page;
    public int NormalizedPageSize => PageSize is < 1 or > 100 ? 20 : PageSize;
}
