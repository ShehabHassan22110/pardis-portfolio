using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class WorkController : Controller
{
    private readonly IPublicContentService _content;
    public WorkController(IPublicContentService content) => _content = content;

    [HttpGet("/work")]
    public async Task<IActionResult> Index(string? discipline)
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/work");
        return View(await _content.GetWorkAsync(discipline));
    }

    [HttpGet("/work/{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var vm = await _content.GetProjectAsync(slug);
        if (vm is null) return NotFound();
        var p = vm.Project;
        // Admin SeoPage override for this exact route wins; otherwise derive from content.
        ViewData["Seo"] = await _content.GetSeoAsync($"/work/{slug}") ?? new SeoPage
        {
            MetaTitle = string.IsNullOrEmpty(p.ClientName) ? p.Title : $"{p.Title} — {p.ClientName}",
            MetaTitleAr = string.IsNullOrEmpty(p.ClientNameAr) ? p.TitleAr : $"{p.TitleAr} — {p.ClientNameAr}",
            MetaDescription = p.ShortDescription ?? StripHtml(p.Description),
            MetaDescriptionAr = p.ShortDescriptionAr ?? StripHtml(p.DescriptionAr),
            OgImage = string.IsNullOrEmpty(p.CoverImage) ? p.ThumbnailImage : p.CoverImage
        };
        return View(vm);
    }

    internal static string? StripHtml(string? html) =>
        string.IsNullOrEmpty(html) ? html : System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", "").Trim();
}
