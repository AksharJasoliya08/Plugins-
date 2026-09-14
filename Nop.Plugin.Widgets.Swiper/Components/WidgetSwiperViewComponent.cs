using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Swiper.Domain;
using Nop.Plugin.Widgets.Swiper.Infrastructure.Cache;
using Nop.Plugin.Widgets.Swiper.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Seo;
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
    protected readonly IPriceCalculationService _priceCalculationService;
    protected readonly IWorkContext _workContext;
    protected readonly IUrlRecordService _urlRecordService;

    #endregion

    #region Ctor

    public WidgetSwiperViewComponent(
        IPictureService pictureService,
        IStaticCacheManager staticCacheManager,
        ISettingService settingService,
        IStoreContext storeContext,
        IWebHelper webHelper,
        IProductService productService,
        IPriceCalculationService priceCalculationService,
        IWorkContext workContext,
        IUrlRecordService urlRecordService)
    {
        _pictureService = pictureService;
        _staticCacheManager = staticCacheManager;
        _settingService = settingService;
        _storeContext = storeContext;
        _webHelper = webHelper;
        _productService = productService;
        _priceCalculationService = priceCalculationService;
        _workContext = workContext;
        _urlRecordService = urlRecordService;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Gets a picture URL by picture identifier.
    /// </summary>
    /// <param name="pictureId">Picture identifier.</param>
    /// <returns>Picture URL.</returns>
    private async Task<string> GetPictureUrlAsync(int pictureId)
    {
        if (pictureId <= 0)
            return string.Empty;

        var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(
            ModelCacheEventConsumer.PictureUrlModelKey,
            pictureId,
            _webHelper.IsCurrentConnectionSecured());

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            // Null values are not cacheable, so use an empty string.
            var url = await _pictureService.GetPictureUrlAsync(
                pictureId,
                showDefaultPicture: false);

            return url ?? string.Empty;
        });
    }

    /// <summary>
    /// Gets the public URL of a product.
    /// </summary>
    /// <param name="product">Product.</param>
    /// <returns>Product URL.</returns>
    private async Task<string> GetProductUrlAsync(Nop.Core.Domain.Catalog.Product product)
    {
        var seName = await _urlRecordService.GetSeNameAsync(product);

        if (string.IsNullOrWhiteSpace(seName))
            return string.Empty;

        var storeLocation = _webHelper.GetStoreLocation();

        return $"{storeLocation.TrimEnd('/')}/product/{seName}";
    }

    #endregion

    #region Methods

    /// <summary>
    /// Invokes the Swiper widget.
    /// </summary>
    /// <param name="widgetZone">Widget zone.</param>
    /// <param name="additionalData">Additional data.</param>
    /// <returns>View component result.</returns>
    public async Task<IViewComponentResult> InvokeAsync(
        string widgetZone,
        object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();

        var sliderSettings =
            await _settingService.LoadSettingAsync<SwiperSettings>(store.Id);

        if (sliderSettings == null || string.IsNullOrWhiteSpace(sliderSettings.Slides))
            return Content(string.Empty);

        var dbModel = new PublicInfoModel
        {
            ShowNavigation = slider.ShowNavigation,
            ShowPagination = slider.ShowPagination,
            Autoplay = slider.Autoplay,
            AutoplayDelay = slider.AutoplayDelay,
            SliderFormat = slider.SliderFormat,
            EnableProductMapping = slider.EnableProductMapping,
            ShowProductName = slider.ShowProductName,
            ShowProductPrice = slider.ShowProductPrice,
            ShowShopNowButton = slider.ShowShopNowButton,
            ButtonText = slider.ButtonText ?? "Shop Now"
        };

        var slides = JsonConvert.DeserializeObject<List<Slide>>(
            sliderSettings.Slides) ?? new List<Slide>();

        foreach (var slide in slides)
        {
            if (slide == null)
                continue;

            var pictureUrl = await GetPictureUrlAsync(slide.PictureId);
            var posterPictureUrl = await GetPictureUrlAsync(slide.PosterPictureId);
            var mobilePosterPictureUrl =
                await GetPictureUrlAsync(slide.MobilePosterPictureId);
            var desktopPosterPictureUrl =
                await GetPictureUrlAsync(slide.DesktopPosterPictureId);

            var publicSlideModel = new PublicSlideModel
            {
                Id = slide.Id,
                PictureId = slide.PictureId,
                PictureUrl = pictureUrl,
                TitleText = slide.TitleText,
                LinkUrl = slide.LinkUrl,
                AltText = slide.AltText,
                LazyLoading = sliderSettings.LazyLoading,

                ContentType = slide.ContentType,
                VideoUrl = slide.VideoUrl,
                ExternalVideoUrl = slide.ExternalVideoUrl,

                PosterPictureUrl = posterPictureUrl,

                MobileVideoUrl = slide.MobileVideoUrl,
                MobilePosterPictureUrl = mobilePosterPictureUrl,

                DesktopVideoUrl = slide.DesktopVideoUrl,
                DesktopPosterPictureUrl = desktopPosterPictureUrl,

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

                Description = slide.Description,

                EnableProductMapping = sliderSettings.EnableProductMapping,
                ShowProductName = sliderSettings.ShowProductName,
                ShowProductPrice = sliderSettings.ShowProductPrice,
                ShowShopNowButton = sliderSettings.ShowShopNowButton,
                ButtonText = sliderSettings.ButtonText,
                SliderFormat = sliderSettings.SliderFormat
            };

            /*
             * Product mapping
             */
            if (sliderSettings.EnableProductMapping && slide.ProductId > 0)
            {
                var product = await _productService.GetProductByIdAsync(
                    slide.ProductId);

                if (product != null && !product.Deleted)
                {
                    publicSlideModel.ProductName = product.Name;

                    // Product URL
                    publicSlideModel.ProductUrl =
                        await GetProductUrlAsync(product);

                    // Product price
                    var customer =
                        await _workContext.GetCurrentCustomerAsync();

                    var priceResult =
                        await _priceCalculationService.GetFinalPriceAsync(
                            product,
                            customer,
                            store,
                            additionalCharge: 0,
                            includeDiscounts: true,
                            quantity: 1);

                    publicSlideModel.ProductPrice =
                        priceResult.finalPrice.ToString(
                            "C",
                            CultureInfo.CurrentCulture);

                    // Product thumbnail
                    var productPictures =
                        await _productService.GetProductPicturesByProductIdAsync(
                            product.Id);

                    var firstProductPicture = productPictures?
                        .OrderBy(p => p.DisplayOrder)
                        .ThenBy(p => p.Id)
                        .FirstOrDefault();

                    if (firstProductPicture != null)
                    {
                        publicSlideModel.ProductThumbnailUrl =
                            await GetPictureUrlAsync(
                                firstProductPicture.PictureId);
                    }
                }
            }

            model.Slides.Add(publicSlideModel);
        }

        if (!model.Slides.Any())
            return Content(string.Empty);

        return View(
            "~/Plugins/Widgets.Swiper/Views/PublicInfo.cshtml",
            model);
    }

    #endregion
}