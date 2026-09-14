using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Editor for the About feature block singleton plus its facts.</summary>
public class AboutFormVm
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

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    [Display(Name = "Long description")]
    public string? LongDescription { get; set; }
    [Display(Name = "Long description (Arabic)")]
    public string? LongDescriptionAr { get; set; }

    /// <summary>Existing stored image path.</summary>
    public string? Image { get; set; }
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Location")]
    public string? Location { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationAr { get; set; }

    [Display(Name = "Quote")]
    public string? Quote { get; set; }
    [Display(Name = "Quote (Arabic)")]
    public string? QuoteAr { get; set; }

    [Display(Name = "Button text")]
    public string? ButtonText { get; set; }
    [Display(Name = "Button text (Arabic)")]
    public string? ButtonTextAr { get; set; }
    [Display(Name = "Button URL")]
    public string? ButtonUrl { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    public List<AboutFactVm> Facts { get; set; } = new();
}

/// <summary>A key/value fact shown in the About block.</summary>
public class AboutFactVm
{
    public int Id { get; set; }
    [Display(Name = "Label")]
    public string? Label { get; set; }
    [Display(Name = "Label (Arabic)")]
    public string? LabelAr { get; set; }
    [Display(Name = "Value")]
    public string? Value { get; set; }
    [Display(Name = "Value (Arabic)")]
    public string? ValueAr { get; set; }
    [Display(Name = "Order")]
    public int DisplayOrder { get; set; }
}
