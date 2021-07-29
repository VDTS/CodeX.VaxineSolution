using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Validations;
using VaxineApp.Views.Home.Status.Anonymous;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status.Anonymous
{
    public class AddAnonymousChildViewModel : ViewModelBase
    {
        // Validator Class
        AnonymousChildValidator AnonymousChildValidator { get; set; }
        // Property
        private AnonymousChildModel anonymousChildModel;
        public AnonymousChildModel AnonymousChildModel
        {
            get
            {
                return anonymousChildModel;
            }
            set
            {
                anonymousChildModel = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand PostCommand { private set; get; }
        // ctor
        public AddAnonymousChildViewModel()
        {
            // Validator
            AnonymousChildValidator = new AnonymousChildValidator();

            // Property
            AnonymousChildModel = new AnonymousChildModel();

            // Command
            PostCommand = new Command(Post);
        }

        private async void Post(object obj)
        {

            AnonymousChildModel.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));
            AnonymousChildModel.Id = Guid.NewGuid();

            var time = DateTime.Now;
            DateTime dateTime = new DateTime(AnonymousChildModel.DOB.Year, AnonymousChildModel.DOB.Month, AnonymousChildModel.DOB.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

            AnonymousChildModel.DOB = dateTime;

            var result = AnonymousChildValidator.Validate(AnonymousChildModel);

            if (result.IsValid)
            {
                var data = JsonConvert.SerializeObject(AnonymousChildModel);
                string a = await DataService.Post(data, $"AnonymousChild/{Preferences.Get("TeamId", "")}");
                if (a == "OK")
                {

                    StandardMessagesDisplay.AddDisplayMessage(AnonymousChildModel.FullName);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(AnonymousChildPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
