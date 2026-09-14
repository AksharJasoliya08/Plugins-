using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Swiper.Domain;

namespace Nop.Plugin.Widgets.Swiper.Data.Migrations
{
    /// <summary>
    /// Represents the plugin database migration for Swiper slider
    /// </summary>
    [NopMigration("2024.01.15", "Swiper Slider - Initial Schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoPluginMigration
    {
        #region Methods

        /// <summary>
        /// Upgrade process
        /// </summary>
        public override void Up()
        {
            // Create SwiperSlider table
            if (!TableExists(nameof(SwiperSlider)))
            {
                CreateTable(nameof(SwiperSlider), (EntityTypeBuilder<SwiperSlider> builder) =>
                {
                    builder.HasKey(slider => slider.Id);
                    builder.Property(slider => slider.Id).ValueGeneratedOnAdd();
                    builder.Property(slider => slider.Name).HasMaxLength(200).IsRequired();
                    builder.Property(slider => slider.WidgetZone).HasMaxLength(200);
                    builder.Property(slider => slider.SliderFormat);
                    builder.Property(slider => slider.Enabled);
                    builder.Property(slider => slider.ShowNavigation);
                    builder.Property(slider => slider.ShowPagination);
                    builder.Property(slider => slider.Autoplay);
                    builder.Property(slider => slider.AutoplayDelay);
                    builder.Property(slider => slider.LazyLoading);
                    builder.Property(slider => slider.EnableProductMapping);
                    builder.Property(slider => slider.ShowProductName);
                    builder.Property(slider => slider.ShowProductPrice);
                    builder.Property(slider => slider.ShowShopNowButton);
                    builder.Property(slider => slider.ButtonText).HasMaxLength(100);
                    builder.Property(slider => slider.DisplayOrder);
                    builder.Property(slider => slider.StoreId);
                    builder.Property(slider => slider.CreatedOnUtc);
                    builder.Property(slider => slider.UpdatedOnUtc);
                    builder.Property(slider => slider.TextAlignment).HasMaxLength(20);

                    builder.HasIndex(slider => slider.Enabled);
                    builder.HasIndex(slider => slider.StoreId);
                    builder.HasIndex(slider => slider.DisplayOrder);
                });
            }

            // Create SwiperSlide table
            if (!TableExists(nameof(SwiperSlide)))
            {
                CreateTable(nameof(SwiperSlide), (EntityTypeBuilder<SwiperSlide> builder) =>
                {
                    builder.HasKey(slide => slide.Id);
                    builder.Property(slide => slide.Id).ValueGeneratedOnAdd();
                    builder.Property(slide => slide.SwiperSliderId);
                    builder.Property(slide => slide.DisplayOrder);
                    builder.Property(slide => slide.ContentType);
                    builder.Property(slide => slide.PictureId);
                    builder.Property(slide => slide.TitleText).HasMaxLength(200);
                    builder.Property(slide => slide.LinkUrl).HasMaxLength(500);
                    builder.Property(slide => slide.AltText).HasMaxLength(200);
                    builder.Property(slide => slide.VideoUrl).HasMaxLength(500);
                    builder.Property(slide => slide.ExternalVideoUrl).HasMaxLength(500);
                    builder.Property(slide => slide.PosterPictureId);
                    builder.Property(slide => slide.MobileVideoUrl).HasMaxLength(500);
                    builder.Property(slide => slide.MobilePosterPictureId);
                    builder.Property(slide => slide.DesktopVideoUrl).HasMaxLength(500);
                    builder.Property(slide => slide.DesktopPosterPictureId);
                    builder.Property(slide => slide.ProductId);
                    builder.Property(slide => slide.Heading).HasMaxLength(200);
                    builder.Property(slide => slide.Subtitle).HasMaxLength(200);
                    builder.Property(slide => slide.CtaText).HasMaxLength(100);
                    builder.Property(slide => slide.CtaUrl).HasMaxLength(500);
                    builder.Property(slide => slide.TextAlignment).HasMaxLength(20);
                    builder.Property(slide => slide.VideoAutoplay);
                    builder.Property(slide => slide.VideoMuted);
                    builder.Property(slide => slide.VideoLoop);
                    builder.Property(slide => slide.VideoControls);
                    builder.Property(slide => slide.Description).HasMaxLength(1000);
                    builder.Property(slide => slide.VideoFileName).HasMaxLength(255);
                    builder.Property(slide => slide.MobileVideoFileName).HasMaxLength(255);
                    builder.Property(slide => slide.DesktopVideoFileName).HasMaxLength(255);

                    builder.HasIndex(slide => slide.SwiperSliderId);
                    builder.HasIndex(slide => slide.DisplayOrder);
                    builder.HasIndex(slide => slide.ProductId);
                    builder.HasIndex(slide => slide.ContentType);
                });
            }
        }

        #endregion
    }
}
