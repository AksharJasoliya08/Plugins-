using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Swiper.Models;

/// <summary>
/// Represents a configuration model
/// </summary>
public record ConfigurationModel : BaseNopModel
{
    #region Properties

    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.ShowNavigation")]
    public bool ShowNavigation { get; set; }
    public bool ShowNavigation_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.ShowPagination")]
    public bool ShowPagination { get; set; }
    public bool ShowPagination_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.Autoplay")]
    public bool Autoplay { get; set; }
    public bool Autoplay_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.AutoplayDelay")]
    public int AutoplayDelay { get; set; }
    public bool AutoplayDelay_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.LazyLoading")]
    public bool LazyLoading { get; set; }
    public bool LazyLoading_OverrideForStore { get; set; }

    /// <summary>
    /// Slider format: 0=Standard, 1=Hero Video, 2=Product Carousel, 3=Reels
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.SliderFormat")]
    public int SliderFormat { get; set; }
    public bool SliderFormat_OverrideForStore { get; set; }

    /// <summary>
    /// Enable product mapping for Product Carousel and Reels formats
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.EnableProductMapping")]
    public bool EnableProductMapping { get; set; }
    public bool EnableProductMapping_OverrideForStore { get; set; }

    /// <summary>
    /// Show product name in Product Carousel/Reels
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ShowProductName")]
    public bool ShowProductName { get; set; }
    public bool ShowProductName_OverrideForStore { get; set; }

    /// <summary>
    /// Show product price in Product Carousel/Reels
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ShowProductPrice")]
    public bool ShowProductPrice { get; set; }
    public bool ShowProductPrice_OverrideForStore { get; set; }

    /// <summary>
    /// Show Shop Now button in Product Carousel/Reels
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ShowShopNowButton")]
    public bool ShowShopNowButton { get; set; }
    public bool ShowShopNowButton_OverrideForStore { get; set; }

    /// <summary>
    /// Custom button text for Shop Now button
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ButtonText")]
    public string ButtonText { get; set; }
    public bool ButtonText_OverrideForStore { get; set; }

    public SlidesSearchModel SlidesSearchModel { get; set; } = new();
    public SlidePictureModel AddSlideModel { get; set; } = new();

    #endregion
}