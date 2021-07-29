using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Status.Anonymous;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status.Anonymous
{
    public class AnonymousChildViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<AnonymousChildModel> anonymousChild;
        public ObservableCollection<AnonymousChildModel> AnonymousChild
        {
            get
            {
                return anonymousChild;
            }
            set
            {
                anonymousChild = value;
                OnPropertyChanged();
            }
        }

        private AnonymousChildModel selectedAnonymousChild;
        public AnonymousChildModel SelectedAnonymousChild
        {
            get
            {
                return selectedAnonymousChild;
            }
            set
            {
                selectedAnonymousChild = value;
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
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }


        // ctor
        public AnonymousChildViewModel()
        {
            // Property
            AnonymousChild = new ObservableCollection<AnonymousChildModel>();
            SelectedAnonymousChild = new AnonymousChildModel();

            // Get
            Get();

            // Command
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToPutPageCommand = new Command(GoToPutPage);
            DeleteCommand = new Command(Delete);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
        }

        private async void Get()
        {
            var data = await DataService.Get($"AnonymousChild/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, AnonymousChildModel>>(data);

                StaticDataStore.TeamStats.TotalIDPChilds = clinic.Where(item => item.Value.Type == "IDP").ToList().Count;
                StaticDataStore.TeamStats.TotalGuestChilds = clinic.Where(item => item.Value.Type == "Guest").ToList().Count;
                StaticDataStore.TeamStats.TotalReturnChilds = clinic.Where(item => item.Value.Type == "Return").ToList().Count;
                StaticDataStore.TeamStats.TotalRefugeeChilds = clinic.Where(item => item.Value.Type == "Refugee").ToList().Count;


                foreach (KeyValuePair<string, AnonymousChildModel> item in clinic)
                {
                    AnonymousChild.Add(
                        new AnonymousChildModel
                        {
                            FId = item.Key.ToString(),
                            Id = item.Value.Id,
                            DOB = item.Value.DOB,
                            FullName = item.Value.FullName,
                            Gender = item.Value.Gender,
                            IsVaccined = item.Value.IsVaccined, 
                            RegisteredBy = item.Value.RegisteredBy,
                            Type = item.Value.Type
                        }
                        );
                }

            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private async void Delete(object obj)
        {
            if (SelectedAnonymousChild.FId != null)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedAnonymousChild.FullName);
                if (isDeleteAccepted)
                {
                    var data = await DataService.Delete($"AnonymousChild/{Preferences.Get("TeamId", "")}/{SelectedAnonymousChild.FId}");
                    if (data == "Deleted")
                    {
                        if (SelectedAnonymousChild.Type == "Refugee")
                        {
                            string b = await DataService.Put((--StaticDataStore.TeamStats.TotalRefugeeChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalRefugeeChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "IDP")
                        {
                            string b = await DataService.Put((--StaticDataStore.TeamStats.TotalIDPChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalIDPChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "Return")
                        {
                            string b = await DataService.Put((--StaticDataStore.TeamStats.TotalReturnChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalReturnChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "Guest")
                        {
                            string b = await DataService.Put((--StaticDataStore.TeamStats.TotalGuestChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalGuestChilds");
                        }
                        else
                        {
                            return;
                        }


                        AnonymousChild.Remove(SelectedAnonymousChild);
                    }
                    else
                    {
                        StandardMessagesDisplay.CanceledDisplayMessage();
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private async void GoToPutPage(object obj)
        {
            if (SelectedAnonymousChild.FId != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedAnonymousChild);
                var route = $"{nameof(EditAnonymousChildPage)}?AnonymousChild={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedAnonymousChild = null;
            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }

        private async void GoToPostPage(object obj)
        {
            var route = $"{nameof(AddAnonymousChildPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }
        public void Clear()
        {
            AnonymousChild.Clear();
        }
    }
}
