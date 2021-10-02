using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Period
{
    public class EditPeriodViewModel : ViewModelBase
    {
        // Property
        public PeriodModel VaccinePeriods { get; }
        PeriodValidator? ValidationRules { get; set; }

        // Command
        public ICommand PutCommand { private set; get; }
        public EditPeriodViewModel(PeriodModel vaccinePeriods)
        {
            VaccinePeriods = vaccinePeriods;
            ValidationRules = new PeriodValidator();

            PutCommand = new Command(Put);
        }
        public async void Put()
        {
            if (VaccinePeriods != null)
            {
                var result = ValidationRules?.Validate(VaccinePeriods);
                if (result != null && result.IsValid)
                {
                    var jsonData = JsonConvert.SerializeObject(VaccinePeriods);
                    var data = await DataService.Put(jsonData, $"VaccinePeriods/{VaccinePeriods.FId}");
                    if (data == "Submit")
                    {
                        StandardMessagesDisplay.EditDisplaymessage(VaccinePeriods.PeriodName);
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
