using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="BardeesCms.Web.Models.Entities.Video"/>, with MP4 + thumbnail upload.</summary>
public class VideoFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";
    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Client")]
    public string? Client { get; set; }
    [Display(Name = "Client (Arabic)")]
    public string? ClientAr { get; set; }

    public VideoProvider Provider { get; set; } = VideoProvider.YouTube;

    [Display(Name = "Video URL"), Url]
    public string? VideoUrl { get; set; }
    [Display(Name = "Provider video id")]
    public string? ProviderVideoId { get; set; }

    /// <summary>Existing stored MP4 path.</summary>
    public string? VideoFile { get; set; }
    /// <summary>Newly uploaded MP4, if any.</summary>
    [Display(Name = "Video file (MP4)")]
    public IFormFile? VideoFileUpload { get; set; }

    /// <summary>Existing stored thumbnail path.</summary>
    public string? Thumbnail { get; set; }
    /// <summary>Newly uploaded thumbnail image, if any.</summary>
    [Display(Name = "Thumbnail")]
    public IFormFile? ThumbnailFile { get; set; }

    [StringLength(20)]
    public string? Duration { get; set; }
    [Display(Name = "Portrait (vertical)")]
    public bool IsPortrait { get; set; }

    [Display(Name = "Category")]
    public int? VideoCategoryId { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Featured")]
    public bool IsFeatured { get; set; }
    [Display(Name = "Published")]
    public bool IsPublished { get; set; } = true;
}
