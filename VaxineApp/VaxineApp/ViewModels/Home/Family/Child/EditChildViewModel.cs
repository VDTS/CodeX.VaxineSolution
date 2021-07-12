using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class EditChildViewModel : ViewModelBase
    {
        // Validator Class
        ChildValidator ChildValidator { get; set; }
        // Property
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

        Guid FamilyId;
        // Command
        public ICommand PutCommand { private set; get; }

        public EditChildViewModel(ChildModel child, Guid familyId)
        {
            // Objects
            ChildValidator = new ChildValidator();

            // Property
            FamilyId = familyId;
            Child = child;

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put()
        {
            var result = ChildValidator.Validate(Child);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Child);
                var data = await DataService.Put(jsonData, $"Child/{FamilyId}/{Child.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(child.FullName);
                    var route = $"//{nameof(StatusPage)}";
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
