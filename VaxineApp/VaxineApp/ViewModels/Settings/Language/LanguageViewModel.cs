﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using VaxineApp.Resx;

namespace VaxineApp.ViewModels.Settings.Language
{
    public class LanguageViewModel : ViewModelBase
    {
        List<(Func<string> name, string value)> languageMapping { get; } = new()
        {
            (() => AppResources.System, null),
            (() => AppResources.English, "en"),
            (() => AppResources.Pashto, "ps"),
        };

        public LocalizedString CurrentLanguage { get; }

        //public LocalizedString Version { get; } = new(() => string.Format(AppResources.Version, AppInfo.VersionString));

        public ICommand ChangeLanguageCommand { get; }

        public LanguageViewModel()
        {
            CurrentLanguage = new(GetCurrentLanguageName);
            //CurrentLanguage = new(() => LocalizationResourceManager.Current.CurrentCulture.DisplayName);

            ChangeLanguageCommand = new AsyncCommand(ChangeLanguage);
        }

        private string GetCurrentLanguageName()
        {
            var (knownName, _) = languageMapping.SingleOrDefault(m => m.value == LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            return knownName != null ? knownName() : LocalizationResourceManager.Current.CurrentCulture.DisplayName;
        }

        async Task ChangeLanguage()
        {
            string selectedName = await Application.Current.MainPage.DisplayActionSheet(
                AppResources.ChangeLanguage,
                null, null,
                languageMapping.Select(m => m.name()).ToArray());
            if (selectedName == null)
            {
                return;
            }

            string selectedValue = languageMapping.Single(m => m.name() == selectedName).value;
            LocalizationResourceManager.Current.CurrentCulture = selectedValue == null ? CultureInfo.CurrentCulture : new CultureInfo(selectedValue);
        }
    }
}
