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
            //int b = DependencyService.Get<IAppVersion>().GetBuild();
            //try
            //{
            //    WebClient client = new WebClient();
            //    Stream stream = client.OpenRead(string.Concat("https://raw.githubusercontent.com/VDTS/docs/main/AndroidReleaseNotes/",$"{AppPackageName}/{AppVersion}.md"));
            //    StreamReader reader = new StreamReader(stream);


            //    var view = new MarkdownView();
            //    view.Markdown = reader.ReadToEnd();
            //    //view.Theme = new DarkMarkdownTheme(); // Default is white, you also modify various values
            //    AppNewUpdates = view;
            //}
            //catch (Exception)
            //{
            //    IsUpdatesAvailable = true;
            //}

            var view = new MarkdownView();
            view.Markdown = $@"## VaxineApp
Version: 1.5.1 beta  
for Android

### Whats new?
- Recycle Bin only in UI
- Different Toast Messages added to reduce exceptions and give more information to users about what happens
- Feedback page is ready for cache support, and will be added in next releases
- All messages changed to Toast messages to reduce complexity in app.
- Routes runs independently from Add and Edit methods. If you add or edit something, you will be on the page until you decide to go out.
- SignIn Command added, it turns off sign in button unitl you add proper email and password
- Adds Expander view to Edit Profile Page
- Toast messages text refactored.
- AppUpdates and Privacy Policy cached locally


### Bug fixes and exceptions handled
- Fixed issues in routes, now you can get out from pages easily without any problem.
- SearchBox crashed fixed
- AppUpdates and Privacy Policy Pages slow execution fixed.

### Known issues
- Can't Edit Location in MasjeedEdit Page
- Show Masjeed Page when you open Area Page

### Important Links
To know whats next, see our plans.  
- [Android App Plan](https://github.com/VDTS/CodeX.VaxineSolution/projects/1)  
- [Android App UI Plan](https://github.com/VDTS/CodeX.VaxineSolution/projects/2)  


### Note
> Fill a feedback if you have issue or any suggestion.  
> Don't submit app crash report in feedback, because they are logged automatically using Micrsoft Visual Studio App Center Crashes Analytics
";
            AppNewUpdates = view;
        }
    }
}

