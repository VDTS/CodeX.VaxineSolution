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
using VaxineApp.Views.Home.Area.School;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class SchoolViewModel : ViewModelBase
    {
        // Property
        private SchoolModel selectedSchool;
        public SchoolModel SelectedSchool
        {
            get
            {
                return selectedSchool;
            }
            set
            {
                selectedSchool = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SchoolModel> schools;
        public ObservableCollection<SchoolModel> Schools
        {
            get
            {
                return schools;
            }
            set
            {
                schools = value;
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

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToMapCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }

        // ctor
        public SchoolViewModel()
        {
            // Property
            Schools = new ObservableCollection<SchoolModel>();
            SelectedSchool = new SchoolModel();

            // Get
            Get();

            //Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToMapCommand = new Command(GoToMap);
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
        }

        private async void GoToPutPage()
        {
            if (SelectedSchool.SchoolName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedSchool);
                var route = $"{nameof(EditSchoolPage)}?School={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedSchool = null;
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }
        private async void Delete(object obj)
        {
            if (SelectedSchool.FId != null)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedSchool.SchoolName);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"School/{Preferences.Get("TeamId", "")}/{SelectedSchool.FId}");
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
                        string b = await DataService.Put((--StaticDataStore.TeamStats.TotalSchools).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalSchools");

                        StandardMessagesDisplay.ItemDeletedToast();

                        Schools.Remove(SelectedSchool);
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

        private void GoToMap(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }


        public async void Get()
        {
            var jData = await DataService.Get($"School/{Preferences.Get("TeamId", "")}");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, SchoolModel>>(jData);
                foreach (KeyValuePair<string, SchoolModel> item in data)
                {
                    Schools.Add(
                        new SchoolModel
                        {
                            FId = item.Key.ToString(),
                            Id = item.Value.Id,
                            KeyInfluencer = item.Value.KeyInfluencer,
                            SchoolName = item.Value.SchoolName,
                            Longitude = item.Value.Longitude,
                            Latitude = item.Value.Latitude
                        }
                        );
                }
            }
        }


        async void GoToPostPage()
        {
            var route = $"{nameof(AddSchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async void Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            Get();

            IsBusy = false;
        }

        void Clear()
        {
            Schools.Clear();
        }
    }
}
