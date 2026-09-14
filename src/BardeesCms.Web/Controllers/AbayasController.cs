using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class AbayasController : Controller
{
    private readonly IPublicContentService _content;
    public AbayasController(IPublicContentService content) => _content = content;

    [HttpGet("/abayas")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/abayas");
        return View(await _content.GetAbayasAsync());
    }
}
