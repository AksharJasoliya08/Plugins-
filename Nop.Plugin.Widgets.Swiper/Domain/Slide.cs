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
