using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VaxineApp.MVVMHelper;
using Xam.Forms.Markdown;

namespace VaxineApp.ViewModels.Settings.PrivacyPolicy
{
    public class PrivacyPolicyViewModel : ViewModelBase
    {
        // Property
        private MarkdownView content;
        public MarkdownView Content
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

            var view = new MarkdownView();
            view.Markdown = reader.ReadToEnd();
            //view.Theme = new DarkMarkdownTheme(); // Default is white, you also modify various values
            Content = view;
        }
    }
}
