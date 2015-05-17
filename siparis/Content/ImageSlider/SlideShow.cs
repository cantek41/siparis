using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;

namespace siparis
{
    public class ImageSliderFeaturesDemoOptions
    {
        ImageSliderImageAreaSettings settingsImageArea;
        ImageSliderNavigationBarSettings settingsNavigationBar;
        ImageSliderBehaviorSettings settingsBehavior;

        public ImageSliderFeaturesDemoOptions()
        {
            this.settingsImageArea = new ImageSliderImageAreaSettings(null)
            {
                ImageSizeMode = ImageSizeMode.FillAndCrop,
                AnimationType = AnimationType.Fade,
                NavigationDirection = NavigationDirection.Horizontal,
                ItemTextVisibility = ElementVisibilityMode.OnMouseOver,
                NavigationButtonVisibility = ElementVisibilityMode.OnMouseOver
            };
            this.settingsNavigationBar = new ImageSliderNavigationBarSettings(null)
            {
                Position = NavigationBarPosition.Bottom,
                Mode = NavigationBarMode.Thumbnails,
                ThumbnailsModeNavigationButtonVisibility = ElementVisibilityMode.OnMouseOver
            };
            this.settingsBehavior = new ImageSliderBehaviorSettings(null)
            {
                EnablePagingGestures = true,
                EnablePagingByClick = AutoBoolean.Auto,
                ImageLoadMode = ImageLoadMode.DynamicLoadAndCache,
                ExtremeItemClickMode = ExtremeItemClickMode.SelectAndSlide
            };
            ShowNavigationBar = true;
            PredefinedScenario = "Default";
        }

        public ImageSliderImageAreaSettings SettingsImageArea { get { return settingsImageArea; } }
        public ImageSliderNavigationBarSettings SettingsNavigationBar { get { return settingsNavigationBar; } }
        public ImageSliderBehaviorSettings SettingsBehavior { get { return settingsBehavior; } }
        public bool ShowNavigationBar { get; set; }
        public string PredefinedScenario { get; set; }
    }


    public class ImageSliderSlideShowDemoOptions
    {
        public ImageSliderSlideShowDemoOptions()
        {
            AutoPlay = true;
            Interval = 5000;
            PlayPauseButtonVisibility = ElementVisibilityMode.Faded;
            StopPlayingWhenPaging = true;
            PausePlayingWhenMouseOver = false;
        }

        public bool AutoPlay { get; set; }
        public int Interval { get; set; }
        public ElementVisibilityMode PlayPauseButtonVisibility { get; set; }
        public bool StopPlayingWhenPaging { get; set; }
        public bool PausePlayingWhenMouseOver { get; set; }
    }

    public static class ImageSliderDemoHelper
    {
        public static List<SelectListItem> GetPredefinedScenarios(string selectedScenario)
        {
            List<SelectListItem> predefinedScenarios = new List<SelectListItem>() {
                new SelectListItem() { Text = "Default", Value = "Default" },
                new SelectListItem() { Text = "Fill and Crop, Dots", Value = "FillAndCropDots" },
                new SelectListItem() { Text = "Vertical Scrolling", Value = "VerticalScrolling" }
            };
            if (!string.IsNullOrEmpty(selectedScenario))
                predefinedScenarios.Find(item => item.Value == selectedScenario).Selected = true;
            return predefinedScenarios;
        }
        public static List<SelectListItem> GetSlideShowIntervals()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "1000", Value = "1000" },
                new SelectListItem() { Text = "2000", Value = "2000" },
                new SelectListItem() { Text = "3000", Value = "3000" },
                new SelectListItem() { Text = "5000", Value = "5000", Selected = true }
            };
        }
        public static List<SelectListItem> GetSourceFolders()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "Landscapes", Value = "~/Content/Images/landscapes", Selected = true },
                new SelectListItem() { Text = "People", Value = "~/Content/Images/people" },
                new SelectListItem() { Text = "Photo Gallery", Value = "~/Content/Images/photo_gallery" }
            };
        }
        public static List<SelectListItem> GetCategories()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "All", Value = null, Selected = true },
                new SelectListItem() { Text = "Vegetables", Value = "1" },
                new SelectListItem() { Text = "Fruits", Value = "2" },
                new SelectListItem() { Text = "Mushrooms", Value = "3" },
                new SelectListItem() { Text = "Cereal", Value = "4" }
            };
        }
        public static List<SelectListItem> GetVisibleItemsCount()
        {
            return new List<SelectListItem>() {
                new SelectListItem() { Text = "3", Value = "3" },
                new SelectListItem() { Text = "5", Value = "5", Selected = true }
            };
        }
    }
}