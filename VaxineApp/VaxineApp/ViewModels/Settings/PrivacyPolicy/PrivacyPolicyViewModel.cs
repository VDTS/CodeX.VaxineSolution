using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.Settings.PrivacyPolicy
{
    public class PrivacyPolicyViewModel : ViewModelBase
    {
        // Property
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public PrivacyPolicyViewModel()
        {
            DownloadFile();
        }
        public void DownloadFile()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://raw.githubusercontent.com/VDTS/docs/main/PrivacyPolicy.md");
            StreamReader reader = new StreamReader(stream);
            Content = reader.ReadToEnd();
        }
    }
}
