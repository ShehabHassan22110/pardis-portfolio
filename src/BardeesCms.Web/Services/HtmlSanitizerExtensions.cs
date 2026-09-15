using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BardeesCms.Web.Services;

/// <summary>
/// View helper for rendering admin-authored rich text. Use <c>@Html.Sanitized(...)</c> instead of
/// <c>@Html.Raw(...)</c> for any content that originates from the database / admin editors, so it is
/// run through the editorial allow-list (<see cref="IHtmlSanitizerService"/>) before reaching the page.
/// Do NOT use for pre-encoded output (JSON-LD) or for the by-design SuperAdmin CustomCss/CustomJs.
/// </summary>
public static class HtmlSanitizerExtensions
{
    public static IHtmlContent Sanitized(this IHtmlHelper helper, string? html)
    {
        if (string.IsNullOrWhiteSpace(html)) return HtmlString.Empty;
        var sanitizer = helper.ViewContext.HttpContext.RequestServices
            .GetRequiredService<IHtmlSanitizerService>();
        return new HtmlString(sanitizer.Sanitize(html));
    }
}
