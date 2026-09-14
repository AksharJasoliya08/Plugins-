using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Swiper.Models;

/// <summary>
/// Represents a slide model on the site
/// </summary>
public record PublicSlideModel : BaseNopModel
{
    #region Properties

    public int PictureId { get; set; }
    public string PictureUrl { get; set; }
    public string TitleText { get; set; }
    public string LinkUrl { get; set; }
    public string AltText { get; set; }
    public bool LazyLoading { get; set; }

    /// <summary>
    /// Content type: 0=Image, 1=Uploaded Video, 2=External Video URL
    /// </summary>
    public int ContentType { get; set; }

    /// <summary>
    /// Uploaded video URL
    /// </summary>
    public string VideoUrl { get; set; }

    /// <summary>
    /// External video URL
    /// </summary>
    public string ExternalVideoUrl { get; set; }

    /// <summary>
    /// Poster picture URL for video
    /// </summary>
    public string PosterPictureUrl { get; set; }

    /// <summary>
    /// Mobile video URL for responsive support
    /// </summary>
    public string MobileVideoUrl { get; set; }

    /// <summary>
    /// Mobile poster picture URL
    /// </summary>
    public string MobilePosterPictureUrl { get; set; }

    /// <summary>
    /// Desktop video URL
    /// </summary>
    public string DesktopVideoUrl { get; set; }

    /// <summary>
    /// Desktop poster picture URL
    /// </summary>
    public string DesktopPosterPictureUrl { get; set; }

    /// <summary>
    /// Product ID for product mapping
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Product name (loaded from product)
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Product price (loaded from product)
    /// </summary>
    public string ProductPrice { get; set; }

    /// <summary>
    /// Product thumbnail URL
    /// </summary>
    public string ProductThumbnailUrl { get; set; }

    /// <summary>
    /// Product detail URL
    /// </summary>
    public string ProductUrl { get; set; }

    /// <summary>
    /// Heading text for Hero format
    /// </summary>
    public string Heading { get; set; }

    /// <summary>
    /// Subtitle text for Hero format
    /// </summary>
    public string Subtitle { get; set; }

    /// <summary>
    /// CTA button text for Hero format
    /// </summary>
    public string CtaText { get; set; }

    /// <summary>
    /// CTA button URL for Hero format
    /// </summary>
    public string CtaUrl { get; set; }

    /// <summary>
    /// Text alignment for Hero format
    /// </summary>
    public string TextAlignment { get; set; }

    /// <summary>
    /// Video autoplay
    /// </summary>
    public bool VideoAutoplay { get; set; }

    /// <summary>
    /// Video muted
    /// </summary>
    public bool VideoMuted { get; set; }

    /// <summary>
    /// Video loop
    /// </summary>
    public bool VideoLoop { get; set; }

    /// <summary>
    /// Show video controls
    /// </summary>
    public bool VideoControls { get; set; }

    /// <summary>
    /// Description/overlay text
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Enable product mapping flag (passed from settings)
    /// </summary>
    public bool EnableProductMapping { get; set; }

    /// <summary>
    /// Show product name flag (passed from settings)
    /// </summary>
    public bool ShowProductName { get; set; }

    /// <summary>
    /// Show product price flag (passed from settings)
    /// </summary>
    public bool ShowProductPrice { get; set; }

    /// <summary>
    /// Show Shop Now button flag (passed from settings)
    /// </summary>
    public bool ShowShopNowButton { get; set; }

    /// <summary>
    /// Custom button text (passed from settings)
    /// </summary>
    public string ButtonText { get; set; }

    /// <summary>
    /// Slider format (passed from settings)
    /// </summary>
    public int SliderFormat { get; set; }

    #endregion
}
