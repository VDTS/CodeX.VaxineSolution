using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Home.Status.Vaccine;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status
{
    public class ChildVaccineViewModel : ViewModelBase, IDataCrud, IVMUtils
    {

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

        private ObservableCollection<VaccineModel> vaccineList;
        public ObservableCollection<VaccineModel> VaccineList
        {
            get
            {
                return vaccineList;
            }
            set
            {
                vaccineList = value;
                OnPropertyChanged();
            }
        }

        private VaccineModel currentVaccine;
        public VaccineModel CurrentVaccine
        {
            get
            {
                return currentVaccine;
            }
            set
            {
                currentVaccine = value;
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



        // Command
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToSubPostPageCommand { private set; get; }
        public ICommand GoToSubPutPageCommand { private set; get; }
        public ICommand SubDeleteCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }

        // ctor
        public ChildVaccineViewModel(ChildModel child)
        {
            // Property
            Child = child;
            VaccineList = new ObservableCollection<VaccineModel>();
            CurrentVaccine = new VaccineModel();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToSubPutPageCommand = new Command(GoToPutPage);
            SubDeleteCommand = new Command(Delete);
            GoToSubPostPageCommand = new Command(GoToPostPage);
            PullRefreshCommand = new Command(Refresh);
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            VaccineList.Clear();
        }

        public async void Delete()
        {
            if (CurrentVaccine.FId != null)
            {
                var isDeleteAccepted = await App.Current.MainPage.DisplayAlert("", $"Do you want to delete vaccine: {CurrentVaccine.VaccineStatus}?", "Yes", "No");
                if (isDeleteAccepted)
                {
                    var data = await DataService.Delete($"Vaccine/{Child.Id}/{CurrentVaccine.FId}");
                    if (data == "Deleted")
                    {
                        VaccineList.Remove(CurrentVaccine);
                        CurrentVaccine = VaccineList.OrderBy(x => x.VaccinePeriod).LastOrDefault();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Not Deleted", "Try again", "OK");
                    }
                }
                else
                {
                    return;
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No vaccine available", "Vaccine list is empty, can't delete anything", "OK");
            }
        }

        public async void Get()
        {
            var data = await DataService.Get($"Vaccine/{Child.Id}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, VaccineModel>>(data);
                foreach (KeyValuePair<string, VaccineModel> item in clinic)
                {
                    VaccineList.Add(
                        new VaccineModel
                        {
                            Id = item.Value.Id,
                            RegisteredBy = item.Value.RegisteredBy,
                            VaccinePeriod = item.Value.VaccinePeriod,
                            VaccineStatus = item.Value.VaccineStatus,
                            FId = item.Key
                        }
                        );
                }
                CurrentVaccine = VaccineList.OrderBy(x => x.VaccinePeriod).LastOrDefault();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToPostPage()
        {
            var jsonChild = JsonConvert.SerializeObject(Child);
            var route = $"{nameof(AddVaccinePage)}?Child={jsonChild}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GoToPutPage()
        {
            if (CurrentVaccine.FId != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(CurrentVaccine);
                var route = $"{nameof(EditVaccinePage)}?Vaccine={jsonClinic}&ChildId={Child.Id}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No vaccine", "Add vaccine first", "OK");
            }
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public void SaveAsPDF()
        {
            throw new NotImplementedException();
        }

    }
}
