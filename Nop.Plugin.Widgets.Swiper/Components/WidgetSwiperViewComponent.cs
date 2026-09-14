using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Swiper.Domain;
using Nop.Plugin.Widgets.Swiper.Infrastructure.Cache;
using Nop.Plugin.Widgets.Swiper.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Swiper.Components;

public class WidgetSwiperViewComponent : NopViewComponent
{
    #region Fields

    protected readonly IPictureService _pictureService;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly ISettingService _settingService;
    protected readonly IStoreContext _storeContext;
    protected readonly IWebHelper _webHelper;
    protected readonly IProductService _productService;

    #endregion

    #region Ctor

    public WidgetSwiperViewComponent(IPictureService pictureService,
    IStaticCacheManager staticCacheManager,
    ISettingService settingService,
    IStoreContext storeContext,
    IWebHelper webHelper,
    IProductService productService)
    {
        _pictureService = pictureService;
        _staticCacheManager = staticCacheManager;
        _settingService = settingService;
        _storeContext = storeContext;
        _webHelper = webHelper;
        _productService = productService;
    }

    #endregion

    #region Utilities

    /// <returns>A task that represents the asynchronous operation</returns>
    private async Task<string> GetPictureUrlAsync(int pictureId)
    {
        if (pictureId == 0)
            return string.Empty;

        var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PictureUrlModelKey,
            pictureId, _webHelper.IsCurrentConnectionSecured());

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            //little hack here. nulls aren't cacheable so set it to ""
            var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? "";
            return url;
        });
    }

    #endregion

    #region Methods

    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(store.Id);

        if (string.IsNullOrEmpty(sliderSettings.Slides))
            return Content("");

        var model = new PublicInfoModel
        {
            ShowNavigation = sliderSettings.ShowNavigation,
            ShowPagination = sliderSettings.ShowPagination,
            Autoplay = sliderSettings.Autoplay,
            AutoplayDelay = sliderSettings.AutoplayDelay,
            SliderFormat = sliderSettings.SliderFormat,
            EnableProductMapping = sliderSettings.EnableProductMapping,
            ShowProductName = sliderSettings.ShowProductName,
            ShowProductPrice = sliderSettings.ShowProductPrice,
            ShowShopNowButton = sliderSettings.ShowShopNowButton,
            ButtonText = sliderSettings.ButtonText
        };

        var slides = JsonConvert.DeserializeObject<List<Slide>>(sliderSettings.Slides);
        foreach (var slide in slides)
        {
            var picUrl = await GetPictureUrlAsync(slide.PictureId);
            var posterPicUrl = await GetPictureUrlAsync(slide.PosterPictureId);
            var mobilePosterPicUrl = await GetPictureUrlAsync(slide.MobilePosterPictureId);
            var desktopPosterPicUrl = await GetPictureUrlAsync(slide.DesktopPosterPictureId);

            var publicSlideModel = new PublicSlideModel
            {
                PictureId = slide.PictureId,
                PictureUrl = picUrl,
                TitleText = slide.TitleText,
                LinkUrl = slide.LinkUrl,
                AltText = slide.AltText,
                LazyLoading = sliderSettings.LazyLoading,
                ContentType = slide.ContentType,
                VideoUrl = slide.VideoUrl,
                ExternalVideoUrl = slide.ExternalVideoUrl,
                PosterPictureUrl = posterPicUrl,
                MobileVideoUrl = slide.MobileVideoUrl,
                MobilePosterPictureUrl = mobilePosterPicUrl,
                DesktopVideoUrl = slide.DesktopVideoUrl,
                DesktopPosterPictureUrl = desktopPosterPicUrl,
                ProductId = slide.ProductId,
                Heading = slide.Heading,
                Subtitle = slide.Subtitle,
                CtaText = slide.CtaText,
                CtaUrl = slide.CtaUrl,
                TextAlignment = slide.TextAlignment,
                VideoAutoplay = slide.VideoAutoplay,
                VideoMuted = slide.VideoMuted,
                VideoLoop = slide.VideoLoop,
                VideoControls = slide.VideoControls,
                Description = slide.Description
            };

            // Load product information if product mapping is enabled and productId is set
            if (sliderSettings.EnableProductMapping && slide.ProductId > 0)
            {
                var product = await _productService.GetProductByIdAsync(slide.ProductId);
                if (product != null)
                {
                    publicSlideModel.ProductName = product.Name;
                    publicSlideModel.ProductUrl = await _productService.GetProductUrlAsync(product);
                    
                    // Get product price
                    var priceCalculationResult = await _productService.GetFinalPriceAsync(
                        product, 
                        await _storeContext.GetCurrentCustomerAsync(), 
                        false, 
                        1);
                    publicSlideModel.ProductPrice = priceCalculationResult.FinalPrice.ToString("C");
                    
                    // Get product thumbnail
                    var productPicture = await _productService.GetProductPicturesAsync(product);
                    if (productPicture.Any())
                    {
                        publicSlideModel.ProductThumbnailUrl = await GetPictureUrlAsync(productPicture.First().PictureId);
                    }
                }
            }

            // Pass settings to the slide model for rendering
            publicSlideModel.EnableProductMapping = sliderSettings.EnableProductMapping;
            publicSlideModel.ShowProductName = sliderSettings.ShowProductName;
            publicSlideModel.ShowProductPrice = sliderSettings.ShowProductPrice;
            publicSlideModel.ShowShopNowButton = sliderSettings.ShowShopNowButton;
            publicSlideModel.ButtonText = sliderSettings.ButtonText;
            publicSlideModel.SliderFormat = sliderSettings.SliderFormat;

            model.Slides.Add(publicSlideModel);
        }

        if (!model.Slides.Any())
            return Content("");

        return View("~/Plugins/Widgets.Swiper/Views/PublicInfo.cshtml", model);
    }

    #endregion
}