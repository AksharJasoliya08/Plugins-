using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.Swiper.Domain;

namespace Nop.Plugin.Widgets.Swiper.Data.Mapping
{
    /// <summary>
    /// Represents the SwiperSlide entity mapping configuration
    /// </summary>
    public class SwiperSlideMap : NopEntityTypeConfiguration<SwiperSlide>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SwiperSlide> builder)
        {
            builder.ToTable(nameof(SwiperSlide));

            builder.HasKey(slide => slide.Id);

            builder.Property(slide => slide.TitleText).HasMaxLength(200);
            builder.Property(slide => slide.LinkUrl).HasMaxLength(500);
            builder.Property(slide => slide.AltText).HasMaxLength(200);
            builder.Property(slide => slide.VideoUrl).HasMaxLength(500);
            builder.Property(slide => slide.ExternalVideoUrl).HasMaxLength(500);
            builder.Property(slide => slide.MobileVideoUrl).HasMaxLength(500);
            builder.Property(slide => slide.DesktopVideoUrl).HasMaxLength(500);
            builder.Property(slide => slide.Heading).HasMaxLength(200);
            builder.Property(slide => slide.Subtitle).HasMaxLength(200);
            builder.Property(slide => slide.CtaText).HasMaxLength(100);
            builder.Property(slide => slide.CtaUrl).HasMaxLength(500);
            builder.Property(slide => slide.TextAlignment).HasMaxLength(20);
            builder.Property(slide => slide.Description).HasMaxLength(1000);
            builder.Property(slide => slide.VideoFileName).HasMaxLength(255);
            builder.Property(slide => slide.MobileVideoFileName).HasMaxLength(255);
            builder.Property(slide => slide.DesktopVideoFileName).HasMaxLength(255);

            builder.HasIndex(slide => slide.SwiperSliderId);
            builder.HasIndex(slide => slide.DisplayOrder);
            builder.HasIndex(slide => slide.ProductId);
            builder.HasIndex(slide => slide.ContentType);

            base.Configure(builder);
        }

        #endregion
    }
}
