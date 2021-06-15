using Microsoft.AppCenter.Distribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.AppUpdates
{
    public class AppUpdatesViewModel : BaseViewModel
    {
        private bool _isUpdatesAvailable;

        public bool IsUpdatesAvailable
        {
            get { return _isUpdatesAvailable; }
            set
            {
                _isUpdatesAvailable = value;
                RaisedPropertyChanged(nameof(IsUpdatesAvailable));
            }
        }

        private Root _appNewUpdates;
        public Root AppNewUpdates
        {
            get { return _appNewUpdates; }
            set
            {
                _appNewUpdates = value;
                RaisedPropertyChanged(nameof(AppNewUpdates));
            }
        }

        public ICommand CheckForUpdateCommand { private set; get; }
        public ICommand TapToGoToPageCommand { private set; get; }
        public AppUpdatesViewModel()
        {
            CheckForUpdateCommand = new Command(CheckForUpdate);
            TapToGoToPageCommand = new Command<string>(TapToGoToPage);
            DownloadFile();
        }

        private async void TapToGoToPage(string url)
        {
            await App.Current.MainPage.DisplayAlert($"{url}", "Hyperlink to the page is under development", "OK");
        }

        private void CheckForUpdate(object obj)
        {
            Distribute.CheckForUpdate();
        }

        public void DownloadFile()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://raw.githubusercontent.com/VDTS/docs/main/AndroidReleaseNotes/37.json");
                StreamReader reader = new StreamReader(stream);
                string data = reader.ReadToEnd();
                AppNewUpdates = JsonConvert.DeserializeObject<Root>(data);
            }
            catch (Exception)
            {
                IsUpdatesAvailable = true;
            }
        }
    }
    public class Content
    {
        public string Location { get; set; }
        public string Updates { get; set; }
    }

    public class Root
    {
        public string Version { get; set; }
        public string Build { get; set; }
        public string ReleaseNo { get; set; }
        public string ReleaseBranch { get; set; }
        public List<Content> Content { get; set; }
    }
}

