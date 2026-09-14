using System.Diagnostics;
using BardeesCms.Web.Models;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPublicContentService _content;
    public HomeController(IPublicContentService content) => _content = content;

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = await _content.GetSeoAsync("/");
        return View(await _content.GetHomeAsync());
    }

    /// <summary>Switches the UI language via cookie, then returns to the referring page.</summary>
    [HttpGet("/set-language/{lang}")]
    public IActionResult SetLanguage(string lang, string? returnUrl)
    {
        var value = lang == "ar" ? "ar" : "en";
        Response.Cookies.Append(SiteContext.LangCookie, value, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });
        return LocalRedirect(string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl) ? "/" : returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode)
    {
        Response.StatusCode = statusCode ?? 500;
        ViewData["StatusCode"] = statusCode ?? 500;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
