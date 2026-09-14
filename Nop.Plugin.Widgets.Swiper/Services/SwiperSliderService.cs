using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nop.Data;
using Nop.Plugin.Widgets.Swiper.Domain;

namespace Nop.Plugin.Widgets.Swiper.Services
{
    /// <summary>
    /// Represents the Swiper slider service
    /// </summary>
    public class SwiperSliderService : ISwiperSliderService
    {
        #region Fields

        private readonly IRepository<SwiperSlider> _sliderRepository;
        private readonly IRepository<SwiperSlide> _slideRepository;

        #endregion

        #region Ctor

        public SwiperSliderService(
            IRepository<SwiperSlider> sliderRepository,
            IRepository<SwiperSlide> slideRepository)
        {
            _sliderRepository = sliderRepository;
            _slideRepository = slideRepository;
        }

        #endregion

        #region Slider Methods

        /// <summary>
        /// Gets a slider by identifier
        /// </summary>
        public virtual async Task<SwiperSlider> GetSliderByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _sliderRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all sliders
        /// </summary>
        public virtual async Task<IList<SwiperSlider>> GetAllSlidersAsync(string widgetZone = null, int storeId = 0, bool onlyEnabled = true)
        {
            var query = _sliderRepository.Table;

            if (!string.IsNullOrEmpty(widgetZone))
                query = query.Where(slider => slider.WidgetZone == widgetZone);

            if (storeId > 0)
                query = query.Where(slider => slider.StoreId == 0 || slider.StoreId == storeId);

            if (onlyEnabled)
                query = query.Where(slider => slider.Enabled);

            query = query.OrderBy(slider => slider.DisplayOrder).ThenBy(slider => slider.Id);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Inserts a slider
        /// </summary>
        public virtual async Task InsertSliderAsync(SwiperSlider slider)
        {
            if (slider == null)
                throw new ArgumentNullException(nameof(slider));

            slider.CreatedOnUtc = DateTime.UtcNow;
            slider.UpdatedOnUtc = DateTime.UtcNow;

            await _sliderRepository.InsertAsync(slider);
        }

        /// <summary>
        /// Updates a slider
        /// </summary>
        public virtual async Task UpdateSliderAsync(SwiperSlider slider)
        {
            if (slider == null)
                throw new ArgumentNullException(nameof(slider));

            slider.UpdatedOnUtc = DateTime.UtcNow;

            await _sliderRepository.UpdateAsync(slider);
        }

        /// <summary>
        /// Deletes a slider
        /// </summary>
        public virtual async Task DeleteSliderAsync(SwiperSlider slider)
        {
            if (slider == null)
                throw new ArgumentNullException(nameof(slider));

            // Delete associated slides first
            await DeleteSlidesBySliderIdAsync(slider.Id);

            await _sliderRepository.DeleteAsync(slider);
        }

        #endregion

        #region Slide Methods

        /// <summary>
        /// Gets a slide by identifier
        /// </summary>
        public virtual async Task<SwiperSlide> GetSlideByIdAsync(int slideId)
        {
            if (slideId <= 0)
                return null;

            return await _slideRepository.GetByIdAsync(slideId);
        }

        /// <summary>
        /// Gets slides by slider identifier
        /// </summary>
        public virtual async Task<IList<SwiperSlide>> GetSlidesBySliderIdAsync(int sliderId, int storeId = 0)
        {
            if (sliderId <= 0)
                return new List<SwiperSlide>();

            var query = _slideRepository.Table
                .Where(slide => slide.SwiperSliderId == sliderId)
                .OrderBy(slide => slide.DisplayOrder)
                .ThenBy(slide => slide.Id);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Inserts a slide
        /// </summary>
        public virtual async Task InsertSlideAsync(SwiperSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            await _slideRepository.InsertAsync(slide);
        }

        /// <summary>
        /// Updates a slide
        /// </summary>
        public virtual async Task UpdateSlideAsync(SwiperSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            await _slideRepository.UpdateAsync(slide);
        }

        /// <summary>
        /// Deletes a slide
        /// </summary>
        public virtual async Task DeleteSlideAsync(SwiperSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            await _slideRepository.DeleteAsync(slide);
        }

        /// <summary>
        /// Gets slide count by slider identifier
        /// </summary>
        public virtual async Task<int> GetSlideCountAsync(int sliderId)
        {
            if (sliderId <= 0)
                return 0;

            return await _slideRepository.Table.CountAsync(slide => slide.SwiperSliderId == sliderId);
        }

        /// <summary>
        /// Deletes slides by slider identifier
        /// </summary>
        public virtual async Task DeleteSlidesBySliderIdAsync(int sliderId)
        {
            if (sliderId <= 0)
                return;

            var slides = await GetSlidesBySliderIdAsync(sliderId);
            foreach (var slide in slides)
            {
                await _slideRepository.DeleteAsync(slide);
            }
        }

        #endregion
    }
}
