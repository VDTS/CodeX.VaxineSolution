using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.PrivacyPolicy
{
    public class PrivacyPolicyViewModel : BaseViewModel
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisedPropertyChanged(nameof(Content));
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
