namespace Nop.Plugin.Widgets.Swiper.Domain
{
    /// <summary>
    /// Represents the slider display format
    /// </summary>
    public enum SliderFormat
    {
        /// <summary>
        /// Standard mixed image and video swiper
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Full-width hero video slider with overlay text and CTA
        /// </summary>
        HeroVideo = 1,

        /// <summary>
        /// Product video carousel with product information cards
        /// </summary>
        ProductCarousel = 2,

        /// <summary>
        /// Reels video slider (9:16 vertical cards) with popup viewer
        /// </summary>
        Reels = 3
    }
}
