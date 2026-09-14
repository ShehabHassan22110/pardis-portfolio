using Microsoft.AspNetCore.Http;

namespace BardeesCms.Web.Services;

/// <summary>
/// Per-request language state for the public site. Language is stored in the
/// <c>bardees-lang</c> cookie ("en" | "ar"); theme in <c>bardees-theme</c>
/// ("dark" | "light"). Server renders in the current language for SEO; the
/// language toggle sets the cookie and reloads, the theme toggle is instant.
/// </summary>
public class SiteContext
{
    public const string LangCookie = "bardees-lang";
    public const string ThemeCookie = "bardees-theme";

    public string Lang { get; }
    public string Theme { get; }

    public bool IsArabic => Lang == "ar";
    public bool IsRtl => IsArabic;
    public string Dir => IsRtl ? "rtl" : "ltr";

    public SiteContext(IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        var lang = ctx?.Request.Cookies[LangCookie];
        Lang = lang == "ar" ? "ar" : "en";
        var theme = ctx?.Request.Cookies[ThemeCookie];
        Theme = theme == "light" ? "light" : "dark";
    }

    /// <summary>Returns the Arabic value when in Arabic and it is present, else the English.</summary>
    public string? Pick(string? en, string? ar) => IsArabic && !string.IsNullOrWhiteSpace(ar) ? ar : en;

    /// <summary>Static-string convenience: choose between two literals by language.</summary>
    public string T(string en, string ar) => IsArabic ? ar : en;
}
