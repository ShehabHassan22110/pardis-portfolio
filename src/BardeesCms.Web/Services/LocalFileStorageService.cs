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
    /// <summary>Widths (px) of the responsive WebP renditions generated for &lt;picture&gt; srcset.</summary>
    public static readonly int[] ResponsiveWidths = { 480, 960, 1440 };
    // SVG is intentionally NOT accepted for upload: it can carry inline scripts and would be
    // served from our own origin (stored XSS). The static brand favicon.svg under /assets is unaffected.
    public string[] ImageExtensions { get; set; } = { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".avif" };
    public string[] VideoExtensions { get; set; } = { ".mp4", ".webm", ".mov" };
    public string[] ImageContentTypes { get; set; } = { "image/jpeg", "image/png", "image/webp", "image/gif", "image/avif" };
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

    public bool IsAllowedImage(IFormFile file) => IsAllowed(file, _options.ImageExtensions, _options.ImageContentTypes, video: false);
    public bool IsAllowedVideo(IFormFile file) => IsAllowed(file, _options.VideoExtensions, _options.VideoContentTypes, video: true);

    // Extension and Content-Type are both client-controlled and trivially spoofable, so we also
    // verify the file's real leading bytes (magic number) match the claimed kind.
    private static bool IsAllowed(IFormFile file, string[] exts, string[] types, bool video)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!exts.Contains(ext) || !types.Contains(file.ContentType.ToLowerInvariant())) return false;
        return video ? HasVideoSignature(file) : HasImageSignature(file);
    }

    private static byte[] ReadHeader(IFormFile file, int count)
    {
        using var s = file.OpenReadStream();
        var buf = new byte[count];
        var read = 0;
        int n;
        while (read < count && (n = s.Read(buf, read, count - read)) > 0) read += n;
        return read < count ? buf[..read] : buf;
    }

    private static bool HasImageSignature(IFormFile file)
    {
        var h = ReadHeader(file, 12);
        if (h.Length < 12) return false;
        // JPEG
        if (h[0] == 0xFF && h[1] == 0xD8 && h[2] == 0xFF) return true;
        // PNG
        if (h[0] == 0x89 && h[1] == 0x50 && h[2] == 0x4E && h[3] == 0x47) return true;
        // GIF ("GIF8")
        if (h[0] == (byte)'G' && h[1] == (byte)'I' && h[2] == (byte)'F' && h[3] == (byte)'8') return true;
        // WEBP ("RIFF"...."WEBP")
        if (h[0] == (byte)'R' && h[1] == (byte)'I' && h[2] == (byte)'F' && h[3] == (byte)'F'
            && h[8] == (byte)'W' && h[9] == (byte)'E' && h[10] == (byte)'B' && h[11] == (byte)'P') return true;
        // AVIF/HEIF (ISO-BMFF: "....ftyp")
        if (h[4] == (byte)'f' && h[5] == (byte)'t' && h[6] == (byte)'y' && h[7] == (byte)'p') return true;
        return false;
    }

    private static bool HasVideoSignature(IFormFile file)
    {
        var h = ReadHeader(file, 12);
        if (h.Length < 12) return false;
        // MP4 / MOV (ISO-BMFF: "....ftyp")
        if (h[4] == (byte)'f' && h[5] == (byte)'t' && h[6] == (byte)'y' && h[7] == (byte)'p') return true;
        // WEBM / Matroska (EBML header)
        if (h[0] == 0x1A && h[1] == 0x45 && h[2] == 0xDF && h[3] == 0xA3) return true;
        return false;
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

                // Responsive WebP renditions (fixed width buckets) for <picture> srcset.
                // Only generate buckets narrower than the source — never upscale.
                var baseName = Path.GetFileNameWithoutExtension(fileName);
                foreach (var w in FileStorageOptions.ResponsiveWidths)
                {
                    if (img.Width < w) continue;
                    var h = (int)Math.Round(img.Height * (w / (double)img.Width));
                    using var rimg = img.Clone(x => x.Resize(w, h));
                    await rimg.SaveAsync(
                        Path.Combine(absoluteDir, $"{baseName}-{w}.webp"),
                        new SixLabors.ImageSharp.Formats.Webp.WebpEncoder { Quality = 80 }, ct);
                }
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

            // Remove the generated WebP siblings (thumbnail + responsive renditions), if any.
            var dir = Path.GetDirectoryName(absolute)!;
            var stem = Path.GetFileNameWithoutExtension(absolute);
            var siblings = new List<string> { Path.Combine(dir, $"{stem}-thumb.webp") };
            foreach (var w in FileStorageOptions.ResponsiveWidths)
                siblings.Add(Path.Combine(dir, $"{stem}-{w}.webp"));
            foreach (var sibling in siblings.Where(File.Exists))
            {
                try { File.Delete(sibling); }
                catch (Exception ex) { _logger.LogWarning(ex, "Failed deleting derived image {Path}.", sibling); }
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
