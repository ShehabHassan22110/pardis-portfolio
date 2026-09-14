using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace BardeesCms.Web.Services;

/// <summary>Options controlling upload validation + storage location.</summary>
public class FileStorageOptions
{
    /// <summary>Root web path under wwwroot where uploads live.</summary>
    public string UploadsRoot { get; set; } = "/uploads";
    public long MaxImageBytes { get; set; } = 8 * 1024 * 1024;    // 8 MB
    public long MaxVideoBytes { get; set; } = 200 * 1024 * 1024;  // 200 MB
    /// <summary>Originals wider than this are downscaled on upload.</summary>
    public int MaxImageWidth { get; set; } = 2000;
    /// <summary>Longest edge of generated WebP thumbnails.</summary>
    public int ThumbnailWidth { get; set; } = 480;
    public string[] ImageExtensions { get; set; } = { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".svg", ".avif" };
    public string[] VideoExtensions { get; set; } = { ".mp4", ".webm", ".mov" };
    public string[] ImageContentTypes { get; set; } = { "image/jpeg", "image/png", "image/webp", "image/gif", "image/svg+xml", "image/avif" };
    public string[] VideoContentTypes { get; set; } = { "video/mp4", "video/webm", "video/quicktime" };
}

/// <summary>Stores uploaded files on the local disk under wwwroot. Never trusts client filenames.</summary>
public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly FileStorageOptions _options;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IWebHostEnvironment env, IOptions<FileStorageOptions> options, ILogger<LocalFileStorageService> logger)
    {
        _env = env;
        _options = options.Value;
        _logger = logger;
    }

    public bool IsAllowedImage(IFormFile file) => IsAllowed(file, _options.ImageExtensions, _options.ImageContentTypes);
    public bool IsAllowedVideo(IFormFile file) => IsAllowed(file, _options.VideoExtensions, _options.VideoContentTypes);

    private static bool IsAllowed(IFormFile file, string[] exts, string[] types)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        return exts.Contains(ext) && types.Contains(file.ContentType.ToLowerInvariant());
    }

    public async Task<StoredFile> SaveAsync(IFormFile file, string folder, CancellationToken ct = default)
    {
        if (file is null || file.Length == 0)
            throw new InvalidOperationException("Empty file.");

        var isImage = IsAllowedImage(file);
        var isVideo = IsAllowedVideo(file);
        if (!isImage && !isVideo)
            throw new InvalidOperationException($"File type not allowed: {file.FileName} ({file.ContentType}).");

        var max = isVideo ? _options.MaxVideoBytes : _options.MaxImageBytes;
        if (file.Length > max)
            throw new InvalidOperationException($"File too large. Max {max / (1024 * 1024)} MB.");

        // Sanitize the logical folder to prevent path traversal.
        var safeFolder = SanitizeFolder(folder);
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        // Never reuse the client filename: generate a random, safe name.
        var fileName = $"{Guid.NewGuid():N}{ext}";

        var relativeDir = Path.Combine(_options.UploadsRoot.TrimStart('/').Replace('/', Path.DirectorySeparatorChar), safeFolder);
        var absoluteDir = Path.Combine(WebRoot, relativeDir);
        Directory.CreateDirectory(absoluteDir);

        var absolutePath = Path.Combine(absoluteDir, fileName);
        // Guard: resolved path must stay under the uploads root.
        var uploadsAbsRoot = Path.GetFullPath(Path.Combine(WebRoot, _options.UploadsRoot.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));
        if (!Path.GetFullPath(absolutePath).StartsWith(uploadsAbsRoot, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Invalid upload path.");

        await using (var stream = new FileStream(absolutePath, FileMode.Create))
            await file.CopyToAsync(stream, ct);

        int? width = null, height = null;
        string? thumbWebPath = null;
        long finalSize = file.Length;

        // Process raster images: downscale oversized originals + generate a WebP thumbnail.
        // SVGs are left untouched (vector; nothing to resize).
        if (isImage && ext != ".svg")
        {
            try
            {
                using var img = await SixLabors.ImageSharp.Image.LoadAsync(absolutePath, ct);

                // Downscale very large originals in place (keeps files web-sized).
                if (img.Width > _options.MaxImageWidth)
                {
                    var ratio = _options.MaxImageWidth / (double)img.Width;
                    img.Mutate(x => x.Resize(_options.MaxImageWidth, (int)Math.Round(img.Height * ratio)));
                    await img.SaveAsync(absolutePath, ct);
                    finalSize = new FileInfo(absolutePath).Length;
                }
                width = img.Width;
                height = img.Height;

                // Thumbnail as WebP for grids/cards.
                var thumbName = $"{Path.GetFileNameWithoutExtension(fileName)}-thumb.webp";
                var thumbAbs = Path.Combine(absoluteDir, thumbName);
                using (var thumb = img.Clone(x => x.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions
                {
                    Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max,
                    Size = new SixLabors.ImageSharp.Size(_options.ThumbnailWidth, _options.ThumbnailWidth)
                })))
                {
                    await thumb.SaveAsync(thumbAbs, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder { Quality = 80 }, ct);
                }
                thumbWebPath = $"{_options.UploadsRoot}/{safeFolder}/{thumbName}".Replace("\\", "/");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Image processing failed for {File}; stored original only.", fileName);
            }
        }

        var webPath = $"{_options.UploadsRoot}/{safeFolder}/{fileName}".Replace("\\", "/");
        return new StoredFile(webPath, fileName, finalSize, file.ContentType, width, height, thumbWebPath);
    }

    public Task DeleteAsync(string? webPath, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(webPath)) return Task.CompletedTask;
        // Only delete inside the uploads root.
        var uploadsWebRoot = _options.UploadsRoot.TrimEnd('/');
        if (!webPath.StartsWith(uploadsWebRoot, StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

        var relative = webPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var absolute = Path.GetFullPath(Path.Combine(WebRoot, relative));
        var uploadsAbsRoot = Path.GetFullPath(Path.Combine(WebRoot, uploadsWebRoot.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));
        if (absolute.StartsWith(uploadsAbsRoot, StringComparison.OrdinalIgnoreCase) && File.Exists(absolute))
        {
            try { File.Delete(absolute); }
            catch (Exception ex) { _logger.LogWarning(ex, "Failed deleting {Path}.", absolute); }

            // Remove the generated thumbnail sibling, if any.
            var thumb = Path.Combine(Path.GetDirectoryName(absolute)!, $"{Path.GetFileNameWithoutExtension(absolute)}-thumb.webp");
            if (File.Exists(thumb))
            {
                try { File.Delete(thumb); }
                catch (Exception ex) { _logger.LogWarning(ex, "Failed deleting thumbnail {Path}.", thumb); }
            }
        }
        return Task.CompletedTask;
    }

    private string WebRoot => _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");

    private static string SanitizeFolder(string folder)
    {
        if (string.IsNullOrWhiteSpace(folder)) return "misc";
        var parts = folder.Replace('\\', '/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        var clean = parts
            .Select(p => new string(p.Where(c => char.IsLetterOrDigit(c) || c is '-' or '_').ToArray()))
            .Where(p => p.Length > 0 && p != "." && p != "..");
        var result = string.Join('/', clean);
        return string.IsNullOrEmpty(result) ? "misc" : result;
    }
}
