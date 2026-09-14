using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Plugin.Widgets.Swiper.Domain;
using Nop.Plugin.Widgets.Swiper.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.Swiper.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetSwiperController : BasePluginController
{
    #region Fields

    protected readonly ILocalizationService _localizationService;
    protected readonly INotificationService _notificationService;
    protected readonly IPictureService _pictureService;
    protected readonly ISettingService _settingService;
    protected readonly IStoreContext _storeContext;
    protected readonly IWebHostEnvironment _webHostEnvironment;

    #endregion

    #region Ctor

    public WidgetSwiperController(ILocalizationService localizationService,
        INotificationService notificationService,
        IPictureService pictureService,
        ISettingService settingService,
        IStoreContext storeContext,
        IWebHostEnvironment webHostEnvironment)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _pictureService = pictureService;
        _settingService = settingService;
        _storeContext = storeContext;
        _webHostEnvironment = webHostEnvironment;
    }

    #endregion

    #region Utilities

    protected virtual async Task<List<Slide>> GetSlidesForStoreAsync(int storeId)
    {
        var key = $"{nameof(SwiperSettings)}.{nameof(SwiperSettings.Slides)}";

        //load settings for a chosen store scope
        var slidesSetting = await _settingService.GetSettingByKeyAsync(key, string.Empty, storeId: storeId, loadSharedValueIfNotFound: false);

        if (string.IsNullOrEmpty(slidesSetting))
            return new List<Slide>();

        return JsonConvert.DeserializeObject<List<Slide>>(slidesSetting);
    }

    #endregion

    #region Methods

    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public async Task<IActionResult> Configure()
    {
        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ShowNavigation = sliderSettings.ShowNavigation,
            ShowPagination = sliderSettings.ShowPagination,
            Autoplay = sliderSettings.Autoplay,
            AutoplayDelay = sliderSettings.AutoplayDelay,
            LazyLoading = sliderSettings.LazyLoading,
            SliderFormat = sliderSettings.SliderFormat,
            EnableProductMapping = sliderSettings.EnableProductMapping,
            ShowProductName = sliderSettings.ShowProductName,
            ShowProductPrice = sliderSettings.ShowProductPrice,
            ShowShopNowButton = sliderSettings.ShowShopNowButton,
            ButtonText = sliderSettings.ButtonText,
            ActiveStoreScopeConfiguration = storeScope
        };

        if (storeScope > 0)
        {
            model.ShowNavigation_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.ShowNavigation, storeScope);
            model.Autoplay_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.Autoplay, storeScope);
            model.AutoplayDelay_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.AutoplayDelay, storeScope);
            model.LazyLoading_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.LazyLoading, storeScope);
            model.SliderFormat_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.SliderFormat, storeScope);
            model.EnableProductMapping_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.EnableProductMapping, storeScope);
            model.ShowProductName_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.ShowProductName, storeScope);
            model.ShowProductPrice_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.ShowProductPrice, storeScope);
            model.ShowShopNowButton_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.ShowShopNowButton, storeScope);
            model.ButtonText_OverrideForStore = await _settingService.SettingExistsAsync(sliderSettings, x => x.ButtonText, storeScope);
        }

        return View("~/Plugins/Widgets.Swiper/Views/Configure.cshtml", model);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(storeScope);

        sliderSettings.ShowNavigation = model.ShowNavigation;
        sliderSettings.ShowPagination = model.ShowPagination;
        sliderSettings.Autoplay = model.Autoplay;
        sliderSettings.AutoplayDelay = model.AutoplayDelay;
        sliderSettings.LazyLoading = model.LazyLoading;
        sliderSettings.SliderFormat = model.SliderFormat;
        sliderSettings.EnableProductMapping = model.EnableProductMapping;
        sliderSettings.ShowProductName = model.ShowProductName;
        sliderSettings.ShowProductPrice = model.ShowProductPrice;
        sliderSettings.ShowShopNowButton = model.ShowShopNowButton;
        sliderSettings.ButtonText = model.ButtonText;

        /* We do not clear cache after each setting update.
         * This behavior can increase performance because cached settings will not be cleared 
         * and loaded from database after each update */
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ShowNavigation, model.ShowNavigation_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ShowPagination, model.ShowPagination_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.Autoplay, model.Autoplay_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.AutoplayDelay, model.AutoplayDelay_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.LazyLoading, model.LazyLoading_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.SliderFormat, model.SliderFormat_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.EnableProductMapping, model.EnableProductMapping_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ShowProductName, model.ShowProductName_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ShowProductPrice, model.ShowProductPrice_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ShowShopNowButton, model.ShowShopNowButton_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.ButtonText, model.ButtonText_OverrideForStore, storeScope, false);

        //now clear settings cache
        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }

    [IgnoreAntiforgeryToken]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    [HttpPost, ActionName("Configure")]
    [FormValueRequired("add-slide")]
    public virtual async Task<IActionResult> SlideAdd(SlidePictureModel model)
    {
        if (!ModelState.IsValid)
            return await Configure();

        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(storeScope);

        var slides = await GetSlidesForStoreAsync(storeScope);

        slides.Add(new()
        {
            PictureId = model.PictureId,
            AltText = model.AltText,
            TitleText = model.TitleText,
            LinkUrl = model.LinkUrl,
            ContentType = model.ContentType,
            VideoUrl = model.VideoUrl,
            ExternalVideoUrl = model.ExternalVideoUrl,
            PosterPictureId = model.PosterPictureId,
            MobileVideoUrl = model.MobileVideoUrl,
            MobilePosterPictureId = model.MobilePosterPictureId,
            DesktopVideoUrl = model.DesktopVideoUrl,
            DesktopPosterPictureId = model.DesktopPosterPictureId,
            ProductId = model.ProductId,
            Heading = model.Heading,
            Subtitle = model.Subtitle,
            CtaText = model.CtaText,
            CtaUrl = model.CtaUrl,
            TextAlignment = model.TextAlignment,
            VideoAutoplay = model.VideoAutoplay,
            VideoMuted = model.VideoMuted,
            VideoLoop = model.VideoLoop,
            VideoControls = model.VideoControls,
            Description = model.Description
        });

        sliderSettings.Slides = JsonConvert.SerializeObject(slides);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.Slides, true, storeScope);

        return RedirectToAction(nameof(Configure));
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public async Task<IActionResult> SlideList(SlidesSearchModel slidesSearchModel)
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var slides = await GetSlidesForStoreAsync(storeScope);

        if (slides is null)
            return Json(new SlideListModel());

        var model = await new SlideListModel().PrepareToGridAsync(slidesSearchModel, slides.ToPagedList(slidesSearchModel), () =>
        {
            return slides
                .Where(s => s.PictureId != 0 || !string.IsNullOrEmpty(s.VideoUrl) || !string.IsNullOrEmpty(s.ExternalVideoUrl))
                .SelectAwait(async item =>
                {
                    var picture = item.PictureId > 0 ? await _pictureService.GetPictureByIdAsync(item.PictureId) : null;
                    var posterPicture = item.PosterPictureId > 0 ? await _pictureService.GetPictureByIdAsync(item.PosterPictureId) : null;
                    var mobilePosterPicture = item.MobilePosterPictureId > 0 ? await _pictureService.GetPictureByIdAsync(item.MobilePosterPictureId) : null;
                    var desktopPosterPicture = item.DesktopPosterPictureId > 0 ? await _pictureService.GetPictureByIdAsync(item.DesktopPosterPictureId) : null;

                    return new PublicSlideModel
                    {
                        PictureId = item.PictureId,
                        PictureUrl = picture != null ? (await _pictureService.GetPictureUrlAsync(picture, 200)).Url : "",
                        TitleText = item.TitleText,
                        AltText = item.AltText,
                        LinkUrl = item.LinkUrl,
                        ContentType = item.ContentType,
                        VideoUrl = item.VideoUrl,
                        ExternalVideoUrl = item.ExternalVideoUrl,
                        PosterPictureUrl = posterPicture != null ? (await _pictureService.GetPictureUrlAsync(posterPicture, 200)).Url : "",
                        MobileVideoUrl = item.MobileVideoUrl,
                        MobilePosterPictureUrl = mobilePosterPicture != null ? (await _pictureService.GetPictureUrlAsync(mobilePosterPicture, 200)).Url : "",
                        DesktopVideoUrl = item.DesktopVideoUrl,
                        DesktopPosterPictureUrl = desktopPosterPicture != null ? (await _pictureService.GetPictureUrlAsync(desktopPosterPicture, 200)).Url : "",
                        ProductId = item.ProductId,
                        Heading = item.Heading,
                        Subtitle = item.Subtitle,
                        CtaText = item.CtaText,
                        CtaUrl = item.CtaUrl,
                        TextAlignment = item.TextAlignment,
                        VideoAutoplay = item.VideoAutoplay,
                        VideoMuted = item.VideoMuted,
                        VideoLoop = item.VideoLoop,
                        VideoControls = item.VideoControls,
                        Description = item.Description
                    };
                });
        });

        return Json(model);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public virtual async Task<IActionResult> SlideDelete(int pictureId)
    {
        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();

        var slides = await GetSlidesForStoreAsync(storeScope);

        if (!slides?.Any() == true)
            return Json(slides);

        if (slides.RemoveAll(s => s.PictureId == pictureId) == 0)
            return new NullJsonResult();

        var pic = await _pictureService.GetPictureByIdAsync(pictureId);
        if (pic is null)
            return new NullJsonResult();

        await _pictureService.DeletePictureAsync(pic);

        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(storeScope);

        if (!slides?.Any() == true)
        {
            await _settingService.DeleteSettingAsync(sliderSettings, setting => setting.Slides, storeScope);
        }
        else
        {
            sliderSettings.Slides = JsonConvert.SerializeObject(slides);
            await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.Slides, true, storeScope);
        }

        return new NullJsonResult();
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public virtual async Task<IActionResult> SlideEdit(SlidePictureModel model)
    {
        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();

        var slides = await GetSlidesForStoreAsync(storeScope);
        if (!slides?.Any() == true)
            return Content("No slides");

        //try to get a picture with the specified id
        var slide = slides.FirstOrDefault(s => s.PictureId == model.PictureId)
            ?? throw new ArgumentException("No slides found with the specified picture id");

        slide.TitleText = model.TitleText;
        slide.AltText = model.AltText;
        slide.LinkUrl = model.LinkUrl;
        slide.ContentType = model.ContentType;
        slide.VideoUrl = model.VideoUrl;
        slide.ExternalVideoUrl = model.ExternalVideoUrl;
        slide.PosterPictureId = model.PosterPictureId;
        slide.MobileVideoUrl = model.MobileVideoUrl;
        slide.MobilePosterPictureId = model.MobilePosterPictureId;
        slide.DesktopVideoUrl = model.DesktopVideoUrl;
        slide.DesktopPosterPictureId = model.DesktopPosterPictureId;
        slide.ProductId = model.ProductId;
        slide.Heading = model.Heading;
        slide.Subtitle = model.Subtitle;
        slide.CtaText = model.CtaText;
        slide.CtaUrl = model.CtaUrl;
        slide.TextAlignment = model.TextAlignment;
        slide.VideoAutoplay = model.VideoAutoplay;
        slide.VideoMuted = model.VideoMuted;
        slide.VideoLoop = model.VideoLoop;
        slide.VideoControls = model.VideoControls;
        slide.Description = model.Description;

        var sliderSettings = await _settingService.LoadSettingAsync<SwiperSettings>(storeScope);
        sliderSettings.Slides = JsonConvert.SerializeObject(slides);
        await _settingService.SaveSettingOverridablePerStoreAsync(sliderSettings, x => x.Slides, true, storeScope);

        return new NullJsonResult();
    }

    /// <summary>
    /// Upload video file (MegaSlider-style upload)
    /// </summary>
    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
    public virtual async Task<IActionResult> UploadVideo(IFormFile videoFile)
    {
        // 1. Null check
        if (videoFile == null || videoFile.Length == 0)
            return Json(new { success = false, message = "No file uploaded." });

        // 2. MIME type server-side validation
        var allowedMimeTypes = new[] { "video/mp4", "video/webm" };
        if (!allowedMimeTypes.Contains(videoFile.ContentType?.ToLower()))
            return Json(new { success = false, message = "Only MP4 and WebM formats are supported." });

        // 3. Extension validation
        var extension = Path.GetExtension(videoFile.FileName)?.ToLower();
        if (extension != ".mp4" && extension != ".webm")
            return Json(new { success = false, message = "Only MP4 and WebM formats are supported." });

        // 4. Filename sanitization
        var originalName = Path.GetFileNameWithoutExtension(videoFile.FileName ?? "video");

        // Remove unsafe characters — replace with underscore
        var sanitized = System.Text.RegularExpressions.Regex
            .Replace(originalName, @"[<>\/\\:*?|""'\s\.]+", "_");

        // Remove path traversal sequences
        sanitized = sanitized.Replace("..", "_");

        // Max 100 chars total (filename + extension)
        var maxNameLength = 100 - extension.Length;
        if (sanitized.Length > maxNameLength)
            sanitized = sanitized.Substring(0, maxNameLength);

        // Use sanitized name + GUID suffix to avoid conflicts
        var safeFileName = sanitized + "_" + Guid.NewGuid().ToString("N").Substring(0, 8) + extension;

        // 5. Save file to wwwroot/videos folder
        var videosFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "videos");

        if (!Directory.Exists(videosFolderPath))
            Directory.CreateDirectory(videosFolderPath);

        var filePath = Path.Combine(videosFolderPath, safeFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await videoFile.CopyToAsync(stream);
        }

        var url = "/videos/" + safeFileName;

        return Json(new { success = true, url = url });
    }

    #endregion
}