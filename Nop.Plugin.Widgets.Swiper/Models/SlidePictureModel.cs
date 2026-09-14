using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Swiper.Models;

/// <summary>
/// Represents a slide model
/// </summary>
public record SlidePictureModel : BaseNopModel
{
    #region Properties

    [UIHint("Picture")]
    [NopResourceDisplayName("Plugins.Widgets.Swiper.Picture")]
    public int PictureId { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.TitleText")]
    public string TitleText { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.LinkUrl")]
    public string LinkUrl { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Swiper.AltText")]
    public string AltText { get; set; }

    /// <summary>
    /// Content type: 0=Image, 1=Uploaded Video, 2=External Video URL
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ContentType")]
    public int ContentType { get; set; }

    /// <summary>
    /// Uploaded video URL
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.VideoUrl")]
    public string VideoUrl { get; set; }

    /// <summary>
    /// External video URL
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ExternalVideoUrl")]
    public string ExternalVideoUrl { get; set; }

    /// <summary>
    /// Poster picture ID for video
    /// </summary>
    [UIHint("Picture")]
    [NopResourceDisplayName("Plugins.Widgets.Swiper.PosterPictureId")]
    public int PosterPictureId { get; set; }

    /// <summary>
    /// Mobile video URL for responsive support
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.MobileVideoUrl")]
    public string MobileVideoUrl { get; set; }

    /// <summary>
    /// Mobile poster picture ID
    /// </summary>
    [UIHint("Picture")]
    [NopResourceDisplayName("Plugins.Widgets.Swiper.MobilePosterPictureId")]
    public int MobilePosterPictureId { get; set; }

    /// <summary>
    /// Desktop video URL for hero format
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.DesktopVideoUrl")]
    public string DesktopVideoUrl { get; set; }

    /// <summary>
    /// Desktop poster picture ID
    /// </summary>
    [UIHint("Picture")]
    [NopResourceDisplayName("Plugins.Widgets.Swiper.DesktopPosterPictureId")]
    public int DesktopPosterPictureId { get; set; }

    /// <summary>
    /// Product ID for product mapping
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.ProductId")]
    public int ProductId { get; set; }

    /// <summary>
    /// Heading text for Hero format
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.Heading")]
    public string Heading { get; set; }

    /// <summary>
    /// Subtitle text for Hero format
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.Subtitle")]
    public string Subtitle { get; set; }

    /// <summary>
    /// CTA button text for Hero format
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.CtaText")]
    public string CtaText { get; set; }

    /// <summary>
    /// CTA button URL for Hero format
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.CtaUrl")]
    public string CtaUrl { get; set; }

    /// <summary>
    /// Text alignment for Hero format (left, center, right)
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.TextAlignment")]
    public string TextAlignment { get; set; }

    /// <summary>
    /// Video autoplay
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.VideoAutoplay")]
    public bool VideoAutoplay { get; set; }

    /// <summary>
    /// Video muted
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.VideoMuted")]
    public bool VideoMuted { get; set; }

    /// <summary>
    /// Video loop
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.VideoLoop")]
    public bool VideoLoop { get; set; }

    /// <summary>
    /// Show video controls
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.VideoControls")]
    public bool VideoControls { get; set; }

    /// <summary>
    /// Description/overlay text
    /// </summary>
    [NopResourceDisplayName("Plugins.Widgets.Swiper.Description")]
    public string Description { get; set; }

    #endregion
}
