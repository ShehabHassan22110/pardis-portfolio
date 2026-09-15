namespace BardeesCms.Web.Models.ViewModels;

/// <summary>Model for the reusable &lt;picture&gt; partial (webp + jpg fallback, or direct path).</summary>
public class PictureVm
{
    public string? Image { get; set; }
    public string Rendition { get; set; } = "portrait";
    public string Alt { get; set; } = "";
    public string CssClass { get; set; } = "";
    public string ImgClass { get; set; } = "";
    public bool Eager { get; set; }
    /// <summary>Responsive "sizes" hint (how wide the image renders across breakpoints). Defaults to 100vw.</summary>
    public string Sizes { get; set; } = "100vw";
}
