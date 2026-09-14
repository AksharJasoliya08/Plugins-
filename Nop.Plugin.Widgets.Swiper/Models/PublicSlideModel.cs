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

    #endregion
}
