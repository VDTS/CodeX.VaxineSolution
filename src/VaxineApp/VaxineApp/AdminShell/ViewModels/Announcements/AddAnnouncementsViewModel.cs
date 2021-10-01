using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.Core.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Announcements
{
    public class AddAnnouncementsViewModel : ViewModelBase
    {
        AnnouncementsValidator ValidationRules { get; set; }
        // Property
        private AnnouncementsModel? announcement;
        public AnnouncementsModel? Announcement
        {
            get
            {
                return announcement;
            }
            set
            {
                announcement = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddAnnouncementsViewModel()
        {
            // Property
            Announcement = new AnnouncementsModel();
            ValidationRules = new AnnouncementsValidator();

            // Command
            PostCommand = new Command(Post);
        }

        public async void Post()
        {
            if (Announcement != null)
            {
                var result = ValidationRules.Validate(Announcement);
                Announcement.Id = Guid.NewGuid();
                Announcement.IsActive = IsActive.Active;
                Announcement.MessageDateTime = DateTime.UtcNow;

                if (result.IsValid)
                {
                    var jData = JsonConvert.SerializeObject(Announcement);

                    string postResponse = await DataService.Post(jData, "Announcements");

                    if (postResponse == "ConnectionError")
                    {
                        StandardMessagesDisplay.NoConnectionToast();
                    }
                    else if (postResponse == "Error")
                    {
                        StandardMessagesDisplay.Error();
                    }
                    else if (postResponse == "ErrorTracked")
                    {
                        StandardMessagesDisplay.ErrorTracked();
                    }
                    else
                    {
                        var route = "..";
                        await Shell.Current.GoToAsync(route);
                    }
                }
                else
                {
                    StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
                }
            }
        }
    }
}
