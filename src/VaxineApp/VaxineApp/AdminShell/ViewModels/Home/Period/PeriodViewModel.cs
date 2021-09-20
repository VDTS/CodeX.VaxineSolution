using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.AdminShell.Views.Home.Period;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Period
{
    public class PeriodViewModel : ViewModelBase
    {
        private VaccinePeriods? selectedPeriod;
        public VaccinePeriods? SelectedPeriod
        {
            get
            {
                return selectedPeriod;
            }
            set
            {
                selectedPeriod = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VaccinePeriods>? periods;
        public ObservableCollection<VaccinePeriods>? Periods
        {
            get
            {
                return periods;
            }
            set
            {
                periods = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

        // ctor
        public PeriodViewModel()
        {
            // Property
            Periods = new ObservableCollection<VaccinePeriods>();
            SelectedPeriod = new VaccinePeriods();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public async void GoToDetailsPage()
        {
            if (SelectedPeriod.AreEmpty())
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedPeriod);
                var route = $"{nameof(PeriodDetailsViewModel)}?Period={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedPeriod = null;
            }
        }


        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get("VaccinePeriods");

            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, VaccinePeriods>>(jData);
                    
                    if(data != null)
                    foreach (KeyValuePair<string, VaccinePeriods> item in data)
                    {
                        Periods?.Add(
                            new VaccinePeriods
                            {
                                Id = item.Value.Id,
                                StartDate = item.Value.StartDate,
                                EndDate = item.Value.EndDate,
                                PeriodName = item.Value.PeriodName
                            }
                            );
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        async void GoToPostPage()
        {
            var route = $"{nameof(AddPeriodPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Periods?.Clear();
        }
    }
}