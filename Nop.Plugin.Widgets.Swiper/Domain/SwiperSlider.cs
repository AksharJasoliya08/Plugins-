using Nop.Core;

namespace Nop.Plugin.Widgets.Swiper.Domain
{
    /// <summary>
    /// Represents a Swiper Slider configuration
    /// </summary>
    public class SwiperSlider : BaseEntity
    {
        /// <summary>
        /// Gets or sets the slider name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the widget zone where this slider will be displayed
        /// </summary>
        public string WidgetZone { get; set; }

        /// <summary>
        /// Gets or sets the slider format (Standard, Hero, Carousel, Reels)
        /// </summary>
        public int SliderFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the slider is published
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show navigation arrows
        /// </summary>
        public bool ShowNavigation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show pagination
        /// </summary>
        public bool ShowPagination { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable autoplay
        /// </summary>
        public bool Autoplay { get; set; }

        /// <summary>
        /// Gets or sets the autoplay delay in milliseconds
        /// </summary>
        public int AutoplayDelay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable lazy loading
        /// </summary>
        public bool LazyLoading { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether product mapping is enabled
        /// </summary>
        public bool EnableProductMapping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show product name
        /// </summary>
        public bool ShowProductName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show product price
        /// </summary>
        public bool ShowProductPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show "Shop Now" button
        /// </summary>
        public bool ShowShopNowButton { get; set; }

        /// <summary>
        /// Gets or sets the custom button text
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the store ID (0 for all stores)
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public System.DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity update
        /// </summary>
        public System.DateTime UpdatedOnUtc { get; set; }
    }
}
