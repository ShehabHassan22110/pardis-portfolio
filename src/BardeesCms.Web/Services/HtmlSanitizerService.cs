using Ganss.Xss;

namespace BardeesCms.Web.Services;

/// <summary>Sanitizes rich-text/HTML from the admin before it is rendered on the public site.</summary>
public interface IHtmlSanitizerService
{
    string Sanitize(string? html);
}

/// <summary>Wraps HtmlSanitizer with an allow-list tuned for editorial rich text.</summary>
public class HtmlSanitizerService : IHtmlSanitizerService
{
    private readonly HtmlSanitizer _sanitizer;

    public HtmlSanitizerService()
    {
        _sanitizer = new HtmlSanitizer();
        _sanitizer.AllowedTags.Clear();
        foreach (var tag in new[] { "p", "br", "strong", "b", "em", "i", "u", "a", "ul", "ol", "li",
                                    "h2", "h3", "h4", "blockquote", "span", "small" })
            _sanitizer.AllowedTags.Add(tag);
        _sanitizer.AllowedAttributes.Clear();
        _sanitizer.AllowedAttributes.Add("href");
        _sanitizer.AllowedAttributes.Add("title");
        _sanitizer.AllowedAttributes.Add("class");
        _sanitizer.AllowedAttributes.Add("style");
        _sanitizer.AllowedCssProperties.Clear();
        _sanitizer.AllowedCssProperties.Add("font-style");
        _sanitizer.AllowedCssProperties.Add("text-align");
        _sanitizer.AllowedSchemes.Clear();
        _sanitizer.AllowedSchemes.Add("https");
        _sanitizer.AllowedSchemes.Add("http");
        _sanitizer.AllowedSchemes.Add("mailto");
        _sanitizer.AllowedSchemes.Add("tel");
    }

    public string Sanitize(string? html) => string.IsNullOrWhiteSpace(html) ? "" : _sanitizer.Sanitize(html);
}
