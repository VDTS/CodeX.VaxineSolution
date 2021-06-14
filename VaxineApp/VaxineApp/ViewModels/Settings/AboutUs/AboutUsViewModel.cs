using System;
using System.Collections.Generic;
using System.Text;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.Settings.AboutUs
{
    public class AboutUsViewModel : BaseViewModel
    {
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
            Persons = new List<AboutUsModel>();
            AddBio();
        }
        void AddBio()
        {
            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Naveed Ahmad Hematmal",
                    Role = "Project Manager & Full Stack Developer",
                    PhotoURL = "NaveedAhmed.jpg",
                    LinkedInURL = "http://linkedin.com/in/naveedahmadhematmal",
                    TwitterURL = "http://twitter.com/NaveedHematmal",
                    Bio = "text here"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Saeeda Rasuly",
                    Role = "Program manager and UX designer",
                    PhotoURL = "SaeedaRasuly.jpg",
                    LinkedInURL = "http://linkedin.com/in/saeeda-rasuly-377327169",
                    TwitterURL = "",
                    Bio = "text here"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Mohammed Yaseen Zaheen",
                    Role = "Contents Developer and UX Designer",
                    PhotoURL = "MohammadYaseen.jpg",
                    LinkedInURL = "http://linkedin.com/in/mohammad-yasin-zahin-95753517b",
                    TwitterURL = "",
                    Bio = "text here"
                }
                );

            Persons.Add(
                new AboutUsModel
                {
                    FullName = "Abdul Basir Zafar",
                    Role = "Developer and UI Designer",
                    PhotoURL = "AbdulBasir.jpg",
                    LinkedInURL = "http://linkedin.com/in/abdul-basir-zafar-271097193",
                    TwitterURL = "",
                    Bio = "text here"
                }
                );
        }

    }



    public class AboutUsModel
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }
        public string PhotoURL { get; set; }
        public string LinkedInURL { get; set; }
        public string TwitterURL { get; set; }
    }
}
