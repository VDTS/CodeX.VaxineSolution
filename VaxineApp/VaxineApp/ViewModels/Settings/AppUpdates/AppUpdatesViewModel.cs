﻿using Microsoft.AppCenter.Distribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.MVVMHelper;
using Xam.Forms.Markdown;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.AppUpdates
{
    public class AppUpdatesViewModel : ViewModelBase
    {
        // Property
        private string appPackageName;
        public string AppPackageName
        {
            get
            {
                return appPackageName;
            }
            set
            {
                appPackageName = value;
                OnPropertyChanged();
            }
        }

        private bool isUpdatesAvailable;
        public bool IsUpdatesAvailable
        {
            get
            {
                return isUpdatesAvailable;
            }
            set
            {
                isUpdatesAvailable = value;
                OnPropertyChanged();
            }
        }

        private MarkdownView appNewUpdates;
        public MarkdownView AppNewUpdates
        {
            get
            {
                return appNewUpdates;
            }
            set
            {
                appNewUpdates = value;
                OnPropertyChanged();
            }
        }

        private string appVersion;
        public string AppVersion
        {
            get
            {
                return appVersion;
            }
            set
            {
                appVersion = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand CheckForUpdateCommand { private set; get; }

        // ctor
        public AppUpdatesViewModel()
        {
            // Property
            AppVersion = DependencyService.Get<IAppVersion>().GetVersion();
            AppPackageName = DependencyService.Get<IPackageName>().PackageName == "com.codex.vaxineappbeta" ? "Beta" : "Production";

            // Get
            DownloadFile();

            // Command
            CheckForUpdateCommand = new Command(CheckForUpdate);
        }

        private void CheckForUpdate(object obj)
        {
            Distribute.CheckForUpdate();
        }

        public void DownloadFile()
        {
            int b = DependencyService.Get<IAppVersion>().GetBuild();
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(string.Concat("https://raw.githubusercontent.com/VDTS/docs/main/AndroidReleaseNotes/",$"{AppPackageName}/{AppVersion}.md"));
                StreamReader reader = new StreamReader(stream);


                var view = new MarkdownView();
                view.Markdown = reader.ReadToEnd();
                //view.Theme = new DarkMarkdownTheme(); // Default is white, you also modify various values
                AppNewUpdates = view;
            }
            catch (Exception)
            {
                IsUpdatesAvailable = true;
            }
        }
    }
}

