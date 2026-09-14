using BardeesCms.Web.Models.Enums;

namespace BardeesCms.Web.Models.Entities;

/// <summary>A showreel / campaign film. Can be a YouTube/Vimeo URL, an uploaded MP4, or external.</summary>
public class Video : AuditableEntity, IOrderable
{
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    /// <summary>Client / subtitle shown under the title.</summary>
    public string? Client { get; set; }
    public string? ClientAr { get; set; }

    public VideoProvider Provider { get; set; } = VideoProvider.YouTube;
    /// <summary>External URL (YouTube/Vimeo/other) when Provider is not Mp4.</summary>
    public string? VideoUrl { get; set; }
    /// <summary>YouTube/Vimeo watch id, parsed for embedding + poster.</summary>
    public string? ProviderVideoId { get; set; }
    /// <summary>Path to an uploaded MP4 when Provider is Mp4.</summary>
    public string? VideoFile { get; set; }
    public string? Thumbnail { get; set; }
    public string? Duration { get; set; }
    public bool IsPortrait { get; set; }

    public int? VideoCategoryId { get; set; }
    public VideoCategory? VideoCategory { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; } = true;
}

/// <summary>Filter category for the videos page (e.g. "Marketing", "Commercial").</summary>
public class VideoCategory : IOrderable, IActivatable
{
    public int Id { get; set; }
    /// <summary>Stable key used in filter tabs, e.g. "marketing".</summary>
    public string Key { get; set; } = "";
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Video> Videos { get; set; } = new List<Video>();
}
