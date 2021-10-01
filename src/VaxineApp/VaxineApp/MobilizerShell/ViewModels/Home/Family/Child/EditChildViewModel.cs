using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Family.Child
{
    public class EditChildViewModel : ViewModelBase
    {
        // Validator Class
        ChildValidator? ChildValidator { get; set; }
        // Property
        private ChildModel? child;
        public ChildModel? Child
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
            if (Child != null)
            {
                var result = ChildValidator?.Validate(Child);
                if (result != null && result.IsValid)
                {
                    var time = DateTime.Now;
                    DateTime dateTime = new DateTime(Child.DOB.Year, Child.DOB.Month, Child.DOB.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

                    Child.DOB = dateTime;

                    var jsonData = JsonConvert.SerializeObject(Child);
                    var data = await DataService.Put(jsonData, $"Child/{FamilyId}/{Child.FId}");
                    if (data == "Submit")
                    {
                        StandardMessagesDisplay.EditDisplaymessage(Child.FullName);
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
                    StandardMessagesDisplay.ValidationRulesViolation(result?.Errors[0].PropertyName, result?.Errors[0].ErrorMessage);
                }
            }
        }
    }
}
