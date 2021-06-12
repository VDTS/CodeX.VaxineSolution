using System;
using System.Collections.Generic;
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
            WhatsNewContent = "Coming soon";
            Release = "25";
        }

    }
}
