namespace Nop.Plugin.Widgets.Swiper.Domain
{
    /// <summary>
    /// Represents the content type of a slide
    /// </summary>
    public enum SwiperContentType
    {
        /// <summary>
        /// Image slide
        /// </summary>
        Image = 0,

        /// <summary>
        /// Uploaded video slide (MP4/WebM stored locally)
        /// </summary>
        UploadedVideo = 1,

        /// <summary>
        /// External video URL (YouTube, Vimeo, or direct MP4/WebM URL)
        /// </summary>
        ExternalVideo = 2
    }
}
