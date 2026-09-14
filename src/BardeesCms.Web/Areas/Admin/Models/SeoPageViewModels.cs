using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Create/edit form for a <see cref="BardeesCms.Web.Models.Entities.SeoPage"/>.</summary>
public class SeoPageFormVm
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Page name")]
    public string PageName { get; set; } = "";

    [Required, StringLength(200)]
    [Display(Name = "Route")]
    public string Route { get; set; } = "";

    [Display(Name = "Meta title"), StringLength(200)]
    public string? MetaTitle { get; set; }
    [Display(Name = "Meta title (Arabic)"), StringLength(200)]
    public string? MetaTitleAr { get; set; }

    [Display(Name = "Meta description")]
    public string? MetaDescription { get; set; }
    [Display(Name = "Meta description (Arabic)")]
    public string? MetaDescriptionAr { get; set; }

    [Display(Name = "Keywords")]
    public string? Keywords { get; set; }

    [Display(Name = "Canonical URL"), Url]
    public string? CanonicalUrl { get; set; }

    [Display(Name = "OG title"), StringLength(200)]
    public string? OgTitle { get; set; }
    [Display(Name = "OG title (Arabic)"), StringLength(200)]
    public string? OgTitleAr { get; set; }

    [Display(Name = "OG description")]
    public string? OgDescription { get; set; }
    [Display(Name = "OG description (Arabic)")]
    public string? OgDescriptionAr { get; set; }

    [Display(Name = "OG image (URL)")]
    public string? OgImage { get; set; }

    [Display(Name = "Robots")]
    public string? Robots { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
