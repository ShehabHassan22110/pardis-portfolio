using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Upload form for the media library — accepts one or more files.</summary>
public class MediaUploadVm
{
    public IFormFile[]? Files { get; set; }
}

/// <summary>Edit form for a media asset's metadata (and optional file replacement).</summary>
public class MediaEditVm
{
    public int Id { get; set; }
    public string? FilePath { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? OriginalFileName { get; set; }

    [Display(Name = "Alt text"), StringLength(300)]
    public string? AltText { get; set; }
    [Display(Name = "Alt text (Arabic)"), StringLength(300)]
    public string? AltTextAr { get; set; }
    [Display(Name = "Caption"), StringLength(500)]
    public string? Caption { get; set; }
    [Display(Name = "Caption (Arabic)"), StringLength(500)]
    public string? CaptionAr { get; set; }

    /// <summary>Optional replacement file — swaps the stored file, keeps the record.</summary>
    [Display(Name = "Replace file")]
    public IFormFile? ReplacementFile { get; set; }
}
