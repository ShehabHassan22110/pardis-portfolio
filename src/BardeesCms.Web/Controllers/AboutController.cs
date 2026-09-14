using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class AboutController : Controller
{
    private readonly IPublicContentService _content;
    public AboutController(IPublicContentService content) => _content = content;

    [HttpGet("/about")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/about");
        // Reuse the home aggregate — the About page renders the about block, facts,
        // clients, market positioning, disciplines and presence from the same data.
        return View(await _content.GetHomeAsync());
    }
}
