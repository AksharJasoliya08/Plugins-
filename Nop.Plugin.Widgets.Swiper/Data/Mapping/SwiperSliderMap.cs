using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.Swiper.Domain;

namespace Nop.Plugin.Widgets.Swiper.Data.Mapping
{
    /// <summary>
    /// Represents the SwiperSlider entity mapping configuration
    /// </summary>
    public class SwiperSliderMap : NopEntityTypeConfiguration<SwiperSlider>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SwiperSlider> builder)
        {
            builder.ToTable(nameof(SwiperSlider));

            builder.HasKey(slider => slider.Id);

            builder.Property(slider => slider.Name).HasMaxLength(200).IsRequired();
            builder.Property(slider => slider.WidgetZone).HasMaxLength(200);
            builder.Property(slider => slider.ButtonText).HasMaxLength(100);
            builder.Property(slider => slider.TextAlignment).HasMaxLength(20);

            builder.HasIndex(slider => slider.Enabled);
            builder.HasIndex(slider => slider.StoreId);
            builder.HasIndex(slider => slider.DisplayOrder);

            base.Configure(builder);
        }

        #endregion
    }
}
