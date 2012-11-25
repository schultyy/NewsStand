using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Caliburn.Micro;
using NewsStand.Configuration;

namespace NewsStand.UI.Configuration.ViewModels
{
    public sealed class ConfigurationViewModel : Screen
    {
        private ObservableCollection<FontFamily> installedFonts;

        public ObservableCollection<FontFamily> InstalledFonts
        {
            get { return installedFonts; }
            set
            {
                if (installedFonts == value)
                    return;
                installedFonts = value;
                NotifyOfPropertyChange(() => InstalledFonts);
            }
        }

        private FontFamily selectedFont;

        public FontFamily SelectedFont
        {
            get { return selectedFont; }
            set
            {
                if (Equals(selectedFont, value))
                    return;
                selectedFont = value;
                NotifyOfPropertyChange(() => SelectedFont);
            }
        }

        public ConfigurationViewModel()
        {
            DisplayName = "CFG";
            InstalledFonts = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies.OrderBy(c => c.Source));
            var settings = ConfigurationSerializer.Load();
            SelectedFont = string.IsNullOrEmpty(settings.Font) ? InstalledFonts.First() : InstalledFonts.Single(c => c.Source == settings.Font);
        }

        public void SaveSettings()
        {
            var settings = ConfigurationSerializer.Load();
            settings.Font = SelectedFont.Source;
            ConfigurationSerializer.Save(settings);
        }
    }
}
