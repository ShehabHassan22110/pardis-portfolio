using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class ServicesController : Controller
{
    private readonly IPublicContentService _content;
    public ServicesController(IPublicContentService content) => _content = content;

    [HttpGet("/services")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/services");
        return View(await _content.GetServicesAsync());
    }

    [HttpGet("/services/{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var service = await _content.GetServiceAsync(slug);
        if (service is null) return NotFound();
        ViewData["Seo"] = await _content.GetSeoAsync($"/services/{slug}") ?? new SeoPage
        {
            MetaTitle = $"{service.Title} — Services",
            MetaTitleAr = string.IsNullOrEmpty(service.TitleAr) ? null : $"{service.TitleAr} — الخدمات",
            MetaDescription = service.ShortDescription ?? WorkController.StripHtml(service.Description),
            MetaDescriptionAr = service.ShortDescriptionAr ?? WorkController.StripHtml(service.DescriptionAr),
            OgImage = service.Image
        };
        return View(service);
    }
}
