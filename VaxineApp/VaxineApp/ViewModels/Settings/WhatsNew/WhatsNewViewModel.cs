using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.Settings.WhatsNew
{
    public class WhatsNewViewModel : BaseViewModel
    {
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
        public WhatsNewViewModel()
        {
            Version = "1.0 - alpha";
            BuildNo = "34";
            Release = "25";

            DownloadFile();
        }
        public void DownloadFile()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://raw.githubusercontent.com/VDTS/docs/main/ReleaseNotes/25.txt");
            StreamReader reader = new StreamReader(stream);
            WhatsNewContent = reader.ReadToEnd();
        }
    }
}
