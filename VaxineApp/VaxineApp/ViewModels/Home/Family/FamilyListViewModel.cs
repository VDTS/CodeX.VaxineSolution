using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyListViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        private ObservableCollection<GetFamilyModel> families;
        public ObservableCollection<GetFamilyModel> Families
        {
            get
            {
                return families;
            }
            set
            {
                families = value;
                OnPropertyChanged();
            }
        }

        private GetFamilyModel selectedFamily;
        public GetFamilyModel SelectedFamily
        {
            get
            {
                return selectedFamily;
            }
            set
            {
                selectedFamily = value;
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

        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SelectionCommand { private set; get; }
        public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }

        public FamilyListViewModel()
        {
            // Property
            Families = new ObservableCollection<GetFamilyModel>();

            // Get
            Get();

            // Commands
            SaveAsPDFCommand = new Command(SaveAsPDF);
            SelectionCommand = new Command(GoToDetailsPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
        }

        public async void Get()
        {
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
                foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
                {
                    StaticDataStore.FamilyNumbers.Add(item.Value.HouseNo);
                    Families.Add(
                        new GetFamilyModel
                        {
                            FId = item.Key.ToString(),
                            Id = item.Value.Id,
                            ParentName = item.Value.ParentName,
                            PhoneNumber = item.Value.PhoneNumber,
                            HouseNo = item.Value.HouseNo
                        }
                        );
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Families.Clear();
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public async void SaveAsPDF()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddFamilyPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public void GoToPutPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToDetailsPage()
        {
            if (SelectedFamily == null)
            {
                return;
            }
            else
            {
                var JsonSelectedFamily = JsonConvert.SerializeObject(SelectedFamily);
                var route = $"{nameof(FamilyDetailsPage)}?Family={JsonSelectedFamily}";
                await Shell.Current.GoToAsync(route);

                SelectedFamily = null;
            }
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }
    }
}
