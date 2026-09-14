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
    public interface ISwiperSliderService
    {
        #region Slider Methods

        Task<SwiperSlider> GetSliderByIdAsync(int id);
        Task<IList<SwiperSlider>> GetAllSlidersAsync(string widgetZone = null, int storeId = 0, bool onlyEnabled = true);
        Task InsertSliderAsync(SwiperSlider slider);
        Task UpdateSliderAsync(SwiperSlider slider);
        Task DeleteSliderAsync(SwiperSlider slider);

        #endregion

        #region Slide Methods

        Task<SwiperSlide> GetSlideByIdAsync(int slideId);
        Task<IList<SwiperSlide>> GetSlidesBySliderIdAsync(int sliderId, int storeId = 0);
        Task InsertSlideAsync(SwiperSlide slide);
        Task UpdateSlideAsync(SwiperSlide slide);
        Task DeleteSlideAsync(SwiperSlide slide);
        Task<int> GetSlideCountAsync(int sliderId);
        Task DeleteSlidesBySliderIdAsync(int sliderId);

        #endregion
    }
}
