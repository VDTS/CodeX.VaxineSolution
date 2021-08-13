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
            //WebClient client = new WebClient();
            //Stream stream = client.OpenRead("https://raw.githubusercontent.com/VDTS/docs/main/PrivacyPolicy.md");
            //StreamReader reader = new StreamReader(stream);

            //var view = new MarkdownView();
            //view.Markdown = reader.ReadToEnd();
            ////view.Theme = new DarkMarkdownTheme(); // Default is white, you also modify various values
            //Content = view;

            var view = new MarkdownView();
            view.Markdown = $@"# Privacy Policy
version: 1.0 beta
- Tracking users __GPS__ information for detecting fraud behavior in fields  
- __Collecting device information__ of users for feedback; device number, device version and platform for diagnosing which device can cause affliction to the Application.  
- In case of crashes in the app, the complete __diagnostic information__ will be sent to the maintenance team.  
- The app buttons and other components usage __statistics and analytics__ will be shared with product management team for improving user experience based on it.  
- Any __deleted data__ in the app will take 90 days to be completely deleted from the database; this feature is for data integrity.  
- Each data __edit history__ is kept.  
- The App __usage wellbeing__ will be kept for tracking user activity duration.  
- In case of the __Beta version__ of the app, each user is required to update the latest release of the app; as the maintenance team consists of few members and can not support many updates at once.  

### - [TheCodeX Team 2021](https://github.com/VDTS)
";
            Content = view;
        }
    }
}
