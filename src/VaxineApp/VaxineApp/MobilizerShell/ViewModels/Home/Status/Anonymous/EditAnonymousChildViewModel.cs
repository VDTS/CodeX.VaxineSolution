using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Status.Anonymous
{
    public class EditAnonymousChildViewModel : ViewModelBase
    {
        // Validator Class
        AnonymousChildValidator AnonymousChildValidator { get; set; }
        // Property
        private AnonymousChildModel anonymousChild;
        public AnonymousChildModel AnonymousChild
        {
            get
            {
                return anonymousChild;
            }
            set
            {
                anonymousChild = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public EditAnonymousChildViewModel(AnonymousChildModel anonymousChild)
        {
            // Validator
            AnonymousChildValidator = new AnonymousChildValidator();

            // Property
            AnonymousChild = anonymousChild;

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put(object obj)
        {
            var result = AnonymousChildValidator.Validate(AnonymousChild);

            if (result.IsValid)
            {
                var time = DateTime.Now;
                DateTime dateTime = new DateTime(AnonymousChild.DOB.Year, AnonymousChild.DOB.Month, AnonymousChild.DOB.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

                AnonymousChild.DOB = dateTime;

                var jsonData = JsonConvert.SerializeObject(AnonymousChild);
                var data = await DataService.Put(jsonData, $"AnonymousChild/{Preferences.Get("TeamId", "")}/{AnonymousChild.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage($"{AnonymousChild.FullName}");
                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }

}
