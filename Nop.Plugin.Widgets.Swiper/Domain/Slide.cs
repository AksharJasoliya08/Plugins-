namespace Nop.Plugin.Widgets.Swiper.Domain;

/// <summary>
/// Represents a slider item in the settings
/// </summary>
public class Slide
{
    #region Properties

    /// <summary>
    /// Picture identifier
    /// </summary>
    public int PictureId { get; set; }

    /// <summary>
    /// Title attribute for image
    /// </summary>
    public string TitleText { get; set; }

    /// <summary>
    /// Link URL 
    /// </summary>
    public string LinkUrl { get; set; }

    /// <summary>
    /// Image alternate text
    /// </summary>
    public string AltText { get; set; }

    /// <summary>
    /// Content type: 0=Image, 1=Uploaded Video, 2=External Video URL
    /// </summary>
    public int ContentType { get; set; }

    /// <summary>
    /// Uploaded video URL (for ContentType = 1)
    /// </summary>
    public string VideoUrl { get; set; }

    /// <summary>
    /// External video URL (for ContentType = 2)
    /// </summary>
    public string ExternalVideoUrl { get; set; }

    /// <summary>
    /// Video poster image ID
    /// </summary>
    public int PosterPictureId { get; set; }

    /// <summary>
    /// Mobile video URL for responsive video support
    /// </summary>
    public string MobileVideoUrl { get; set; }

    /// <summary>
    /// Mobile poster picture ID
    /// </summary>
    public int MobilePosterPictureId { get; set; }

    /// <summary>
    /// Desktop video URL (alternative to VideoUrl for hero format)
    /// </summary>
    public string DesktopVideoUrl { get; set; }

    /// <summary>
    /// Desktop poster picture ID
    /// </summary>
    public int DesktopPosterPictureId { get; set; }

    /// <summary>
    /// Product ID for product mapping (Product Carousel/Reels)
    /// </summary>
    public int ProductId { get; set; }

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
    /// Text alignment for Hero format (left, center, right)
    /// </summary>
    public string TextAlignment { get; set; }

    /// <summary>
    /// Video autoplay setting
    /// </summary>
    public bool VideoAutoplay { get; set; }

    /// <summary>
    /// Video muted setting
    /// </summary>
    public bool VideoMuted { get; set; }

    /// <summary>
    /// Video loop setting
    /// </summary>
    public bool VideoLoop { get; set; }

    /// <summary>
    /// Show video controls
    /// </summary>
    public bool VideoControls { get; set; }

    /// <summary>
    /// Slide description/overlay text for video slides
    /// </summary>
    public string Description { get; set; }

    #endregion
}
