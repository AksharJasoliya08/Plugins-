using System.Text.Json;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Plugin.Widgets.Swiper.Components;
using Nop.Plugin.Widgets.Swiper.Domain;
using Nop.Plugin.Widgets.Swiper.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.Swiper;

/// <summary>
/// Represents swiper widget
/// </summary>
public class SwiperPlugin : BasePlugin, IWidgetPlugin
{
    #region Fields

    protected readonly ILocalizationService _localizationService;
    protected readonly INopFileProvider _fileProvider;
    protected readonly IPictureService _pictureService;
    protected readonly ISettingService _settingService;
    protected readonly IWebHelper _webHelper;
    protected readonly WidgetSettings _widgetSettings;
    protected readonly IRepository<SwiperSlider> _sliderRepository;
    protected readonly IRepository<SwiperSlide> _slideRepository;
    protected readonly ISwiperSliderService _swiperSliderService;

    #endregion

    #region Ctor

    public SwiperPlugin(
        ILocalizationService localizationService,
        INopFileProvider fileProvider,
        IPictureService pictureService,
        ISettingService settingService,
        IWebHelper webHelper,
        WidgetSettings widgetSettings,
        IRepository<SwiperSlider> sliderRepository,
        IRepository<SwiperSlide> slideRepository,
        ISwiperSliderService swiperSliderService)
    {
        _localizationService = localizationService;
        _fileProvider = fileProvider;
        _pictureService = pictureService;
        _settingService = settingService;
        _webHelper = webHelper;
        _widgetSettings = widgetSettings;
        _sliderRepository = sliderRepository;
        _slideRepository = slideRepository;
        _swiperSliderService = swiperSliderService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets widget zones where this widget should be rendered
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the widget zones
    /// </returns>
    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    /// <summary>
    /// Gets a configuration page URL
    /// </summary>
    public override string GetConfigurationPageUrl()
    {
        return _webHelper.GetStoreLocation() + "Admin/WidgetSwiper/Configure";
    }

    /// <summary>
    /// Gets a name of a view component for displaying widget
    /// </summary>
    /// <param name="widgetZone">Name of the widget zone</param>
    /// <returns>View component name</returns>
    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(WidgetSwiperViewComponent);
    }

    /// <summary>
    /// Install plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task InstallAsync()
    {
        // Create default slider
        var slider = new SwiperSlider
        {
            Name = "Homepage Swiper Slider",
            WidgetZone = PublicWidgetZones.HomepageTop,
            SliderFormat = (int)SliderFormat.Standard,
            Enabled = true,
            ShowNavigation = false,
            ShowPagination = true,
            Autoplay = true,
            AutoplayDelay = 3000,
            LazyLoading = true,
            EnableProductMapping = false,
            ShowProductName = true,
            ShowProductPrice = true,
            ShowShopNowButton = true,
            ButtonText = "Shop Now",
            DisplayOrder = 1,
            StoreId = 0,
            CreatedOnUtc = DateTime.UtcNow,
            UpdatedOnUtc = DateTime.UtcNow
        };

        await _swiperSliderService.InsertSliderAsync(slider);

        //pictures
        var sampleImagesPath = _fileProvider.MapPath("~/Plugins/Widgets.Swiper/Content/sample-images/");

        // Create sample slides for the slider
        var picture1 = await _pictureService.InsertPictureAsync(
            await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner_01.webp")), 
            MimeTypes.ImageWebp, "banner_1");
        
        var picture2 = await _pictureService.InsertPictureAsync(
            await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner_02.webp")), 
            MimeTypes.ImageWebp, "banner_2");

        var slide1 = new SwiperSlide
        {
            SwiperSliderId = slider.Id,
            ContentType = (int)SwiperContentType.Image,
            PictureId = picture1.Id,
            TitleText = string.Empty,
            AltText = "Sample slide 1",
            LinkUrl = _webHelper.GetStoreLocation(),
            DisplayOrder = 1,
            VideoAutoplay = false,
            VideoMuted = false,
            VideoLoop = false,
            VideoControls = false
        };

        var slide2 = new SwiperSlide
        {
            SwiperSliderId = slider.Id,
            ContentType = (int)SwiperContentType.Image,
            PictureId = picture2.Id,
            TitleText = string.Empty,
            AltText = "Sample slide 2",
            LinkUrl = _webHelper.GetStoreLocation(),
            DisplayOrder = 2,
            VideoAutoplay = false,
            VideoMuted = false,
            VideoLoop = false,
            VideoControls = false
        };

        await _swiperSliderService.InsertSlideAsync(slide1);
        await _swiperSliderService.InsertSlideAsync(slide2);

        // Migrate old JSON settings if they exist
        var oldSettings = await _settingService.LoadSettingAsync<SwiperSettings>();
        if (!string.IsNullOrEmpty(oldSettings?.Slides))
        {
            try
            {
                var oldSlides = JsonSerializer.Deserialize<List<Slide>>(oldSettings.Slides);
                if (oldSlides != null && oldSlides.Any())
                {
                    // Convert old JSON slides to database entities
                    foreach (var oldSlide in oldSlides)
                    {
                        var newSlide = new SwiperSlide
                        {
                            SwiperSliderId = slider.Id,
                            ContentType = (int)SwiperContentType.Image,
                            PictureId = oldSlide.PictureId,
                            TitleText = oldSlide.TitleText ?? string.Empty,
                            AltText = oldSlide.AltText ?? string.Empty,
                            LinkUrl = oldSlide.LinkUrl ?? string.Empty,
                            DisplayOrder = oldSlide.DisplayOrder,
                            VideoAutoplay = false,
                            VideoMuted = false,
                            VideoLoop = false,
                            VideoControls = false
                        };
                        await _swiperSliderService.InsertSlideAsync(newSlide);
                    }

                    // Clear old JSON settings
                    oldSettings.Slides = string.Empty;
                    await _settingService.SaveSettingAsync(oldSettings);
                }
            }
            catch
            {
                // Ignore migration errors - old data may be corrupted
            }
        }

        if (!_widgetSettings.ActiveWidgetSystemNames.Contains(PluginDescriptor.SystemName))
        {
            _widgetSettings.ActiveWidgetSystemNames.Add(PluginDescriptor.SystemName);
            await _settingService.SaveSettingAsync(_widgetSettings);
        }

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.Swiper.Slide"] = "Slide",
            ["Plugins.Widgets.Swiper.SlideList"] = "Add new slide",
            ["Plugins.Widgets.Swiper.Slide.Add"] = "Add",
            ["Plugins.Widgets.Swiper.Settings"] = "Settings",
            ["Plugins.Widgets.Swiper.Picture"] = "Picture",
            ["Plugins.Widgets.Swiper.Picture.Hint"] = "Upload picture.",
            ["Plugins.Widgets.Swiper.Picture.Required"] = "Picture is required",
            ["Plugins.Widgets.Swiper.TitleText"] = "Title",
            ["Plugins.Widgets.Swiper.TitleText.Hint"] = "Enter title for picture. Leave empty if you don't want to display any text.",
            ["Plugins.Widgets.Swiper.LinkUrl"] = "URL",
            ["Plugins.Widgets.Swiper.LinkUrl.Hint"] = "Enter URL. Leave empty if you don't want this picture to be clickable.",
            ["Plugins.Widgets.Swiper.AltText"] = "Image alternate text",
            ["Plugins.Widgets.Swiper.AltText.Hint"] = "Enter alternate text that will be added to image.",
            ["Plugins.Widgets.Swiper.Autoplay"] = "Autoplay",
            ["Plugins.Widgets.Swiper.Autoplay.Hint"] = "Check to enable autoplay.",
            ["Plugins.Widgets.Swiper.LazyLoading"] = "Lazy loading",
            ["Plugins.Widgets.Swiper.LazyLoading.Hint"] = "Check to enable lazy loading of pictures.",
            ["Plugins.Widgets.Swiper.AutoplayDelay"] = "Delay",
            ["Plugins.Widgets.Swiper.AutoplayDelay.Hint"] = "Delay between transitions (in ms). If this parameter is not specified, auto play will be disabled.",
            ["Plugins.Widgets.Swiper.ShowNavigation"] = "Show navigation arrows",
            ["Plugins.Widgets.Swiper.ShowNavigation.Hint"] = "Check to display navigation arrows for the slider.",
            ["Plugins.Widgets.Swiper.ShowPagination"] = "Show pagination",
            ["Plugins.Widgets.Swiper.ShowPagination.Hint"] = "Check to display pagination for the slider.",
            ["Plugins.Widgets.Swiper.SliderFormat"] = "Slider Format",
            ["Plugins.Widgets.Swiper.SliderFormat.Hint"] = "Select the slider display format.",
            ["Plugins.Widgets.Swiper.ContentType"] = "Content Type",
            ["Plugins.Widgets.Swiper.ContentType.Hint"] = "Select content type: Image, Uploaded Video, or External Video.",
            ["Plugins.Widgets.Swiper.VideoUrl"] = "Video URL",
            ["Plugins.Widgets.Swiper.VideoUrl.Hint"] = "Uploaded video URL.",
            ["Plugins.Widgets.Swiper.ExternalVideoUrl"] = "External Video URL",
            ["Plugins.Widgets.Swiper.ExternalVideoUrl.Hint"] = "External video URL (YouTube, Vimeo, or direct MP4/WebM).",
            ["Plugins.Widgets.Swiper.UploadVideo"] = "Upload Video",
            ["Plugins.Widgets.Swiper.ReplaceVideo"] = "Replace Video",
            ["Plugins.Widgets.Swiper.RemoveVideo"] = "Remove Video",
            ["Plugins.Widgets.Swiper.VideoPreview"] = "Video Preview",
            ["Plugins.Widgets.Swiper.ProductMapping"] = "Product Mapping",
            ["Plugins.Widgets.Swiper.Heading"] = "Heading",
            ["Plugins.Widgets.Swiper.Subtitle"] = "Subtitle",
            ["Plugins.Widgets.Swiper.Description"] = "Description",
            ["Plugins.Widgets.Swiper.CtaText"] = "CTA Button Text",
            ["Plugins.Widgets.Swiper.CtaUrl"] = "CTA Button URL",
            ["Plugins.Widgets.Swiper.TextAlignment"] = "Text Alignment"
        });

        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstall plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task UninstallAsync()
    {
        // Delete all sliders and slides from database
        var sliders = await _swiperSliderService.GetAllSlidersAsync(onlyEnabled: false);
        foreach (var slider in sliders)
        {
            await _swiperSliderService.DeleteSliderAsync(slider);
        }

        // Clean up uploaded video files
        var videosPath = _fileProvider.MapPath("~/Plugins/Widgets.Swiper/Content/videos/");
        if (_fileProvider.DirectoryExists(videosPath))
        {
            try
            {
                var videoFiles = _fileProvider.GetFiles(videosPath, "*.mp4", false);
                foreach (var file in videoFiles)
                {
                    try
                    {
                        _fileProvider.DeleteFile(file);
                    }
                    catch
                    {
                        // Ignore deletion errors
                    }
                }

                videoFiles = _fileProvider.GetFiles(videosPath, "*.webm", false);
                foreach (var file in videoFiles)
                {
                    try
                    {
                        _fileProvider.DeleteFile(file);
                    }
                    catch
                    {
                        // Ignore deletion errors
                    }
                }
            }
            catch
            {
                // Ignore directory errors
            }
        }

        //settings
        await _settingService.DeleteSettingAsync<SwiperSettings>();
        if (_widgetSettings.ActiveWidgetSystemNames.Contains(PluginDescriptor.SystemName))
        {
            _widgetSettings.ActiveWidgetSystemNames.Remove(PluginDescriptor.SystemName);
            await _settingService.SaveSettingAsync(_widgetSettings);
        }

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.Swiper");

        await base.UninstallAsync();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
    /// </summary>
    public bool HideInWidgetList => false;

    #endregion
}