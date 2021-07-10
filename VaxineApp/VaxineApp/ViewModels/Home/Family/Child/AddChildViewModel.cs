using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Validations;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class AddChildViewModel : ViewModelBase
    {
        // Validator Class
        ChildValidator ChildValidator { get; set; }

        // Property
        public GetFamilyModel Family { get; set; }

        private ChildModel child;
        public ChildModel Child
        {
            get
            {
                return child;
            }
            set
            {
                child = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }

        // ctor
        public AddChildViewModel(GetFamilyModel family)
        {
            // Objects
            ChildValidator = new ChildValidator();
            Family = family;

            // Property
            Child = new ChildModel();

            // Command
            PutCommand = new Command(Post);
        }

        private async void Post()
        {
            Child.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));
            Child.Id = Guid.NewGuid();
            Child.DOB = Child.DOB.ToUniversalTime();
            var result = ChildValidator.Validate(Child);
            if (result.IsValid)
            {
                var data = JsonConvert.SerializeObject(Child);

                string a = await DataService.Post(data, $"Child/{Family.Id}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Child.FullName);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(FamilyListPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not valid", result.Errors[0].ErrorMessage, "OK");
            }
        }
    }
}
