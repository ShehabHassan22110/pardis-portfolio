namespace BardeesCms.Web.Services;

/// <summary>
/// Resolves image references that may be either a legacy "base name" (e.g. "couture-01",
/// which has responsive renditions under /assets/img/&lt;base&gt;-&lt;rend&gt;.{webp,jpg}) or an
/// admin-uploaded web path (e.g. "/uploads/brands/x.png") or an absolute URL.
/// </summary>
public static class ImageResolver
{
    /// <summary>True when the value is a full path/URL rather than a rendition base name.</summary>
    public static bool IsDirectPath(string? value) =>
        !string.IsNullOrWhiteSpace(value) &&
        (value.StartsWith('/') || value.StartsWith("http", StringComparison.OrdinalIgnoreCase)
         || value.Contains('/') || value.Contains('.'));

    /// <summary>Best jpg/direct src for an image reference at the given rendition.</summary>
    public static string Src(string? value, string rendition = "portrait")
    {
        if (string.IsNullOrWhiteSpace(value)) return "";
        if (IsDirectPath(value)) return Normalize(value);
        return $"/assets/img/{value}-{rendition}.jpg";
    }

    /// <summary>WebP source for a base-name image; empty for direct paths (no renditions).</summary>
    public static string WebpSrc(string? value, string rendition = "portrait")
    {
        if (string.IsNullOrWhiteSpace(value) || IsDirectPath(value)) return "";
        return $"/assets/img/{value}-{rendition}.webp";
    }

    private static string Normalize(string value)
    {
        if (value.StartsWith("http", StringComparison.OrdinalIgnoreCase) || value.StartsWith('/')) return value;
        return "/" + value.TrimStart('/');
    }
}
