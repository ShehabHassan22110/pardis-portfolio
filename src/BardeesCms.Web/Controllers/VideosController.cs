using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class VideosController : Controller
{
    private readonly IPublicContentService _content;
    public VideosController(IPublicContentService content) => _content = content;

    [HttpGet("/videos")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/videos");
        return View(await _content.GetVideosAsync());
    }
}
