using Nop.Core;

namespace Nop.Plugin.Widgets.Swiper.Domain
{
    /// <summary>
    /// Represents a Swiper Slide
    /// </summary>
    public class SwiperSlide : BaseEntity
    {
        /// <summary>
        /// Gets or sets the slider identifier
        /// </summary>
        public int SwiperSliderId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the content type (Image, UploadedVideo, ExternalVideo)
        /// </summary>
        public int ContentType { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier (for image slides)
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the title text
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// Gets or sets the link URL
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the alt text for images
        /// </summary>
        public string AltText { get; set; }

        /// <summary>
        /// Gets or sets the uploaded video URL
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the external video URL (YouTube, Vimeo, direct MP4/WebM)
        /// </summary>
        public string ExternalVideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the poster picture identifier
        /// </summary>
        public int PosterPictureId { get; set; }

        /// <summary>
        /// Gets or sets the mobile video URL
        /// </summary>
        public string MobileVideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the mobile poster picture identifier
        /// </summary>
        public int MobilePosterPictureId { get; set; }

        /// <summary>
        /// Gets or sets the desktop video URL
        /// </summary>
        public string DesktopVideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the desktop poster picture identifier
        /// </summary>
        public int DesktopPosterPictureId { get; set; }

        /// <summary>
        /// Gets or sets the associated product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the heading text (for Hero format)
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets the subtitle text (for Hero format)
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the CTA button text (for Hero format)
        /// </summary>
        public string CtaText { get; set; }

        /// <summary>
        /// Gets or sets the CTA button URL (for Hero format)
        /// </summary>
        public string CtaUrl { get; set; }

        /// <summary>
        /// Gets or sets the text alignment (Left, Center, Right)
        /// </summary>
        public string TextAlignment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video autoplay is enabled
        /// </summary>
        public bool VideoAutoplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video is muted
        /// </summary>
        public bool VideoMuted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video loops
        /// </summary>
        public bool VideoLoop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video controls are shown
        /// </summary>
        public bool VideoControls { get; set; }

        /// <summary>
        /// Gets or sets the description/overlay text
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the stored video file name (for cleanup)
        /// </summary>
        public string VideoFileName { get; set; }

        /// <summary>
        /// Gets or sets the stored mobile video file name (for cleanup)
        /// </summary>
        public string MobileVideoFileName { get; set; }

        /// <summary>
        /// Gets or sets the stored desktop video file name (for cleanup)
        /// </summary>
        public string DesktopVideoFileName { get; set; }
    }
}
