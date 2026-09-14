using System.ComponentModel.DataAnnotations;
using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a PortfolioProject, with cover/thumbnail uploads and gallery.</summary>
public class PortfolioProjectFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";
    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    public string? Slug { get; set; }

    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }
    [Display(Name = "Short description (Arabic)")]
    public string? ShortDescriptionAr { get; set; }

    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Client")]
    public string? ClientName { get; set; }
    [Display(Name = "Client (Arabic)")]
    public string? ClientNameAr { get; set; }

    [Display(Name = "Tag")]
    public string? Tag { get; set; }
    [Display(Name = "Tag (Arabic)")]
    public string? TagAr { get; set; }

    [Display(Name = "Discipline")]
    public int? DisciplineId { get; set; }

    /// <summary>Existing stored cover path.</summary>
    public string? CoverImage { get; set; }
    /// <summary>Newly uploaded cover, if any.</summary>
    [Display(Name = "Cover image")]
    public IFormFile? CoverImageFile { get; set; }

    /// <summary>Existing stored thumbnail path.</summary>
    public string? ThumbnailImage { get; set; }
    /// <summary>Newly uploaded thumbnail, if any.</summary>
    [Display(Name = "Thumbnail")]
    public IFormFile? ThumbnailImageFile { get; set; }

    public string? Year { get; set; }
    [Display(Name = "Location")]
    public string? Location { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationAr { get; set; }

    [Display(Name = "Project URL"), Url]
    public string? ProjectUrl { get; set; }
    [Display(Name = "Instagram URL"), Url]
    public string? InstagramUrl { get; set; }

    [Display(Name = "Display order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Featured")]
    public bool IsFeatured { get; set; }
    [Display(Name = "Published")]
    public bool IsPublished { get; set; } = true;

    /// <summary>New gallery images to append on save.</summary>
    [Display(Name = "Gallery images")]
    public IFormFile[]? GalleryFiles { get; set; }

    /// <summary>Existing gallery media (edit only).</summary>
    public List<PortfolioMedia> Media { get; set; } = new();
}
