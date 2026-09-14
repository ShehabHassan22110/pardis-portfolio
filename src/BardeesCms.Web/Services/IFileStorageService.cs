namespace BardeesCms.Web.Services;

/// <summary>Result of storing a file.</summary>
public record StoredFile(string WebPath, string FileName, long Size, string? MimeType, int? Width, int? Height, string? ThumbnailPath = null);

/// <summary>
/// Abstraction over file storage so the backing store (local disk, R2, Azure Blob, S3)
/// can change without touching callers. Paths returned are web-relative (e.g. "/uploads/...").
/// </summary>
public interface IFileStorageService
{
    /// <summary>Validates and stores an uploaded file under the given logical folder.</summary>
    Task<StoredFile> SaveAsync(IFormFile file, string folder, CancellationToken ct = default);

    /// <summary>Deletes a previously stored file by its web-relative path. No-op if missing.</summary>
    Task DeleteAsync(string? webPath, CancellationToken ct = default);

    /// <summary>True if the extension/content type is an allowed image.</summary>
    bool IsAllowedImage(IFormFile file);

    /// <summary>True if the extension/content type is an allowed video.</summary>
    bool IsAllowedVideo(IFormFile file);
}
