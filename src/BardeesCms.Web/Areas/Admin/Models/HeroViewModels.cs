using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Editor for the homepage hero singleton plus its rotating slides.</summary>
public class HeroFormVm
{
    public int Id { get; set; }

    [Display(Name = "Eyebrow")]
    public string? Eyebrow { get; set; }
    [Display(Name = "Eyebrow (Arabic)")]
    public string? EyebrowAr { get; set; }

    [Required, StringLength(200), Display(Name = "Title")]
    public string Title { get; set; } = "";
    [Display(Name = "Title (Arabic)"), StringLength(200)]
    public string? TitleAr { get; set; }

    [Display(Name = "Subtitle")]
    public string? Subtitle { get; set; }
    [Display(Name = "Subtitle (Arabic)")]
    public string? SubtitleAr { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Sectors")]
    public string? Sectors { get; set; }
    [Display(Name = "Sectors (Arabic)")]
    public string? SectorsAr { get; set; }

    [Display(Name = "Primary button text")]
    public string? PrimaryButtonText { get; set; }
    [Display(Name = "Primary button text (Arabic)")]
    public string? PrimaryButtonTextAr { get; set; }
    [Display(Name = "Primary button URL")]
    public string? PrimaryButtonUrl { get; set; }

    [Display(Name = "Secondary button text")]
    public string? SecondaryButtonText { get; set; }
    [Display(Name = "Secondary button text (Arabic)")]
    public string? SecondaryButtonTextAr { get; set; }
    [Display(Name = "Secondary button URL")]
    public string? SecondaryButtonUrl { get; set; }

    /// <summary>Existing stored background image path.</summary>
    public string? BackgroundImage { get; set; }
    [Display(Name = "Background image")]
    public IFormFile? BackgroundImageFile { get; set; }

    [Display(Name = "Background video")]
    public string? BackgroundVideo { get; set; }

    [Display(Name = "Featured label")]
    public string? FeaturedLabel { get; set; }
    [Display(Name = "Featured label (Arabic)")]
    public string? FeaturedLabelAr { get; set; }
    [Display(Name = "Issue number")]
    public string? IssueNumber { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    public List<HeroSlideVm> Slides { get; set; } = new();
}

/// <summary>A single rotating look in the hero background.</summary>
public class HeroSlideVm
{
    public int Id { get; set; }
    [Display(Name = "Image")]
    public string? Image { get; set; }
    /// <summary>Optional uploaded image for this slide; replaces <see cref="Image"/> when present.</summary>
    [Display(Name = "Upload")]
    public IFormFile? ImageFile { get; set; }
    [Display(Name = "Label")]
    public string? Label { get; set; }
    [Display(Name = "Label (Arabic)")]
    public string? LabelAr { get; set; }
    [Display(Name = "Order")]
    public int DisplayOrder { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
