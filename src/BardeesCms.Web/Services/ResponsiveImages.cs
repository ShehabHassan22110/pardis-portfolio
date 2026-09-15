using Microsoft.Extensions.Caching.Memory;

namespace BardeesCms.Web.Services;

/// <summary>
/// Builds a WebP <c>srcset</c> for admin-uploaded images from the fixed-width renditions produced
/// on upload (see <see cref="LocalFileStorageService"/>). Only renditions that actually exist on
/// disk are emitted, so images uploaded before responsive rendering was added fall back gracefully
/// to their single original source. Results are cached (filenames are content-unique GUIDs).
/// </summary>
public class ResponsiveImages
{
    private readonly IWebHostEnvironment _env;
    private readonly IMemoryCache _cache;

    public ResponsiveImages(IWebHostEnvironment env, IMemoryCache cache)
    {
        _env = env;
        _cache = cache;
    }

    /// <summary>e.g. "/uploads/x-480.webp 480w, /uploads/x-960.webp 960w" — empty if none exist.</summary>
    public string WebpSrcSet(string? imageWebPath)
    {
        if (string.IsNullOrWhiteSpace(imageWebPath)
            || !imageWebPath.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
            return "";

        return _cache.GetOrCreate("respimg:" + imageWebPath, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(12);
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var lastDot = imageWebPath.LastIndexOf('.');
            var stem = lastDot > 0 ? imageWebPath[..lastDot] : imageWebPath;

            var candidates = new List<string>();
            foreach (var w in FileStorageOptions.ResponsiveWidths)
            {
                var webPath = $"{stem}-{w}.webp";
                var abs = Path.Combine(webRoot, webPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(abs)) candidates.Add($"{webPath} {w}w");
            }
            return string.Join(", ", candidates);
        }) ?? "";
    }
}
