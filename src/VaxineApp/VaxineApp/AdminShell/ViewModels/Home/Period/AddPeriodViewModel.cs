using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Period
{
    public class AddPeriodViewModel : ViewModelBase
    {
        PeriodValidator? ValidationRules { get; set; }
        // Property
        private PeriodModel? vaccinePeriod;
        public PeriodModel? VaccinePeriod
        {
            get
            {
                return vaccinePeriod;
            }
            set
            {
                vaccinePeriod = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PostCommand { private set; get; }

        public AddPeriodViewModel()
        {
            VaccinePeriod = new PeriodModel();
            ValidationRules = new PeriodValidator();

            // Command
            PostCommand = new Command(Post);
        }
        private async void Post()
        {
            if (VaccinePeriod != null)
            {
                VaccinePeriod.Id = Guid.NewGuid();
                VaccinePeriod.StartDate = VaccinePeriod.StartDate.ToUniversalTime();
                VaccinePeriod.EndDate = VaccinePeriod.EndDate.ToUniversalTime();

                var result = ValidationRules?.Validate(VaccinePeriod);
                if (result != null && result.IsValid)
                {
                    var jData = JsonConvert.SerializeObject(VaccinePeriod);

                    string postResponse = await DataService.Post(jData, $"VaccinePeriods");
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
                        StandardMessagesDisplay.AddDisplayMessage(VaccinePeriod.PeriodName);

                        var route = "..";
                        await Shell.Current.GoToAsync(route);
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
