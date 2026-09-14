using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Models.ViewModels;

/// <summary>A showreel bento item (one large + stacked side items).</summary>
public class ShowreelItemVm
{
    public Video Video { get; set; } = new();
    public bool Main { get; set; }
}
