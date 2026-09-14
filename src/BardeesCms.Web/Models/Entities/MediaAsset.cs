using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>A file in the centralized media library.</summary>
public class MediaAsset
{
    public int Id { get; set; }
    /// <summary>Sanitized, stored file name (never the client-supplied name).</summary>
    public string FileName { get; set; } = "";
    public string? OriginalFileName { get; set; }
    /// <summary>Web path relative to wwwroot, e.g. "/uploads/images/site/foo.webp".</summary>
    public string FilePath { get; set; } = "";
    public string? ThumbnailPath { get; set; }
    public MediaType MediaType { get; set; } = MediaType.Image;
    public string? MimeType { get; set; }
    public long FileSize { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? AltText { get; set; }
    public string? AltTextAr { get; set; }
    public string? Caption { get; set; }
    public string? CaptionAr { get; set; }
    /// <summary>Logical folder/bucket, e.g. "projects", "brands", "site".</summary>
    public string? Folder { get; set; }
    /// <summary>Content hash to help detect duplicate uploads.</summary>
    public string? ContentHash { get; set; }
    public DateTime CreatedAt { get; set; }
}
