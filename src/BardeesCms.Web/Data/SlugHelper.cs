using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BardeesCms.Web.Data;

/// <summary>Generates URL-friendly slugs from titles (Latin transliteration-light).</summary>
public static partial class SlugHelper
{
    [GeneratedRegex(@"[^a-z0-9\s-]")] private static partial Regex NonSlug();
    [GeneratedRegex(@"\s+")] private static partial Regex Whitespace();
    [GeneratedRegex(@"-+")] private static partial Regex Dashes();

    public static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "item";
        var normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        var slug = sb.ToString().ToLowerInvariant();
        slug = NonSlug().Replace(slug, "");
        slug = Whitespace().Replace(slug, "-");
        slug = Dashes().Replace(slug, "-").Trim('-');
        return string.IsNullOrEmpty(slug) ? "item" : slug;
    }

    /// <summary>Returns a slug unique within <paramref name="taken"/>, adding a numeric suffix if needed.</summary>
    public static string Unique(string input, ISet<string> taken)
    {
        var baseSlug = Slugify(input);
        var slug = baseSlug;
        var i = 2;
        while (!taken.Add(slug))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }
}
