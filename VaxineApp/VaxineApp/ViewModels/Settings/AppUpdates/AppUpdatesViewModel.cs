using Microsoft.AppCenter.Distribute;
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
        public ICommand CheckForUpdateCommand { private set; get; }
        private string _version;
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                RaisedPropertyChanged(nameof(Version));
            }
        }
        private string _release;
        public string Release
        {
            get { return _release; }
            set
            {
                _release = value;
                RaisedPropertyChanged(nameof(Release));
            }
        }

        private string _buildNo;
        public string BuildNo
        {
            get { return _buildNo; }
            set
            {
                _buildNo = value;
                RaisedPropertyChanged(nameof(BuildNo));
            }
        }
        private string _whatsNewContent;
        public string WhatsNewContent
        {
            get { return _whatsNewContent; }
            set
            {
                _whatsNewContent = value;
                RaisedPropertyChanged(nameof(WhatsNewContent));
            }
        }
        public AppUpdatesViewModel()
        {
            CheckForUpdateCommand = new Command(CheckForUpdate);
            Version = "1.0 - alpha";
            BuildNo = "42";
            Release = "33";

            DownloadFile();
        }

        private void CheckForUpdate(object obj)
        {
            Distribute.CheckForUpdate();
        }

        public void DownloadFile()
        {
            try{
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead("https://raw.githubusercontent.com/VDTS/docs/main/ReleaseNotes/33.txt");
                    StreamReader reader = new StreamReader(stream);
                    WhatsNewContent = reader.ReadToEnd();
                 }
                catch (Exception)
                {
                    WhatsNewContent = $"No release note available for this release, for earlier versions release note, see https://github.com/VDTS/docs/tree/main/ReleaseNotes";
                }
        }
    }
}

