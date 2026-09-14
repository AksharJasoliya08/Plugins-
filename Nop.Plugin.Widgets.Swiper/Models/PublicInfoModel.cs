using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Swiper.Models;

/// <summary>
/// Represents a slider model on the site
/// </summary>
public record PublicInfoModel : BaseNopModel
{
    #region Properties

    public bool ShowNavigation { get; set; }
    public bool ShowPagination { get; set; }
    public bool Autoplay { get; set; }
    public int AutoplayDelay { get; set; }
    public List<PublicSlideModel> Slides { get; set; } = new();

    /// <summary>
    /// Slider format: 0=Standard, 1=Hero Video, 2=Product Carousel, 3=Reels
    /// </summary>
    public int SliderFormat { get; set; }

    /// <summary>
    /// Enable product mapping for Product Carousel and Reels formats
    /// </summary>
    public bool EnableProductMapping { get; set; }

    /// <summary>
    /// Show product name in Product Carousel/Reels
    /// </summary>
    public bool ShowProductName { get; set; }

    /// <summary>
    /// Show product price in Product Carousel/Reels
    /// </summary>
    public bool ShowProductPrice { get; set; }

    /// <summary>
    /// Show Shop Now button in Product Carousel/Reels
    /// </summary>
    public bool ShowShopNowButton { get; set; }

    /// <summary>
    /// Custom button text for Shop Now button
    /// </summary>
    public string ButtonText { get; set; }

    #endregion
}