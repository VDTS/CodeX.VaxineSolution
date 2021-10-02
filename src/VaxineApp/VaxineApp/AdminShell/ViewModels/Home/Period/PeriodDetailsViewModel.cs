using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.AdminShell.Views.Home.Period;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Period
{
    public class PeriodDetailsViewModel : ViewModelBase
    {
        // Property
        public PeriodModel VaccinePeriods { get; }

        // Command
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }

        public PeriodDetailsViewModel(PeriodModel vaccinePeriods)
        {
            VaccinePeriods = vaccinePeriods;

            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
        }
        private async void GoToPutPage()
        {
            if (VaccinePeriods?.Id != Guid.Empty)
            {
                var jsonClinic = JsonConvert.SerializeObject(VaccinePeriods);
                var route = $"{nameof(EditPeriodPage)}?Period={jsonClinic}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private async void Delete(object obj)
        {
            if (VaccinePeriods?.Id != Guid.Empty)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(VaccinePeriods.PeriodName);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"VaccinePeriods/{VaccinePeriods.FId}");
                    if (deleteResponse == "ConnectionError")
                    {
                        StandardMessagesDisplay.NoConnectionToast();
                    }
                    else if (deleteResponse == "Error")
                    {
                        StandardMessagesDisplay.Error();
                    }
                    else if (deleteResponse == "ErrorTracked")
                    {
                        StandardMessagesDisplay.ErrorTracked();
                    }
                    else if (deleteResponse == "null")
                    {
                        StandardMessagesDisplay.ItemDeletedToast();

                        await Shell.Current.GoToAsync("..");
                    }
                }
                else
                {
                    return;
                }

            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }

    }
}
