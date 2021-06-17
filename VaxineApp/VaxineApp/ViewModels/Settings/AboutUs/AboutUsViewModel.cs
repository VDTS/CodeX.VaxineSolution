using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.AboutUs
{
    public class AboutUsViewModel : BaseViewModel
    {
        public ICommand GoToLinkedInCommand { private set; get; }
        public ICommand GoToTwitterCommand { private set; get; }

        private List<AboutUsModel> _persons;
        public List<AboutUsModel> Persons
        {
            get { return _persons; }
            set
            {
                _persons = value;
                RaisedPropertyChanged(nameof(Persons));
            }
        }
        public AboutUsViewModel()
        {
            GoToLinkedInCommand = new Command<string>(GoToLinkedIn);
            GoToTwitterCommand = new Command<string>(GoToTwitter);
            Persons = new List<AboutUsModel>();
            AddBio();
        }

        private async void GoToTwitter(string url)
        {
            Uri uri = new Uri(url);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private async void GoToLinkedIn(string url)
        {
            Uri uri = new Uri(url);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        void AddBio()
        {
            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Naveed Ahmad Hematmal",
                    Role = "Project Manager & Full Stack Developer",
                    PhotoURL = "NaveedAhmed.jpg",
                    LinkedInURL = "https://linkedin.com/in/naveedahmadhematmal",
                    TwitterURL = "https://twitter.com/NaveedHematmal"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Saeeda Rasuly",
                    Role = "Program manager and UX designer",
                    PhotoURL = "SaeedaRasuly.jpg",
                    LinkedInURL = "https://linkedin.com/in/saeeda-rasuly-377327169",
                    TwitterURL = "https://twitter.com/RasulySaeeda"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Mohammed Yaseen Zaheen",
                    Role = "Contents Developer and UX Designer",
                    PhotoURL = "MohammadYaseen.jpg",
                    LinkedInURL = "https://linkedin.com/in/mohammad-yasin-zahin-95753517b",
                    TwitterURL = "https://twitter.com/YasinZahin4"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Abdul Basir Zafar",
                    Role = "Developer and UI Designer",
                    PhotoURL = "AbdulBasir.jpg",
                    LinkedInURL = "https://linkedin.com/in/abdul-basir-zafar-271097193",
                    TwitterURL = "https://twitter.com/abBasirZafar"
                }
                );
        }

    }
    public class AboutUsModel
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string PhotoURL { get; set; }
        public string LinkedInURL { get; set; }
        public string TwitterURL { get; set; }
    }
}
