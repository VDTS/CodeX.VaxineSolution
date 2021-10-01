using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.MobilizerShell.Views.Home.Status.Anonymous;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Status.Anonymous
{
    public class AnonymousChildViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<AnonymousChildModel>? anonymousChild;
        public ObservableCollection<AnonymousChildModel>? AnonymousChild
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

        private AnonymousChildModel? selectedAnonymousChild;
        public AnonymousChildModel? SelectedAnonymousChild
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

            // Command
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToPutPageCommand = new Command(GoToPutPage);
            DeleteCommand = new Command(Delete);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
        }
        public void FirstLoad(object sender, EventArgs e)
        {
            if(AnonymousChild?.Count == 0)
            Get();
        }
        private async void Get()
        {
            var jData = await DataService.Get($"AnonymousChild/{Preferences.Get("TeamId", "")}");

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
                    var data = JsonConvert.DeserializeObject<Dictionary<string, AnonymousChildModel>>(jData);

                    StaticDataStore.TeamStats.TotalIDPChilds = data.Where(item => item.Value.Type == "IDP").ToList().Count;
                    StaticDataStore.TeamStats.TotalGuestChilds = data.Where(item => item.Value.Type == "Guest").ToList().Count;
                    StaticDataStore.TeamStats.TotalReturnChilds = data.Where(item => item.Value.Type == "Return").ToList().Count;
                    StaticDataStore.TeamStats.TotalRefugeeChilds = data.Where(item => item.Value.Type == "Refugee").ToList().Count;

                    if (data != null)
                        foreach (KeyValuePair<string, AnonymousChildModel> item in data)
                        {
                            AnonymousChild?.Add(
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
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private async void Delete(object obj)
        {
            if (SelectedAnonymousChild?.FId != null)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedAnonymousChild.FullName);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"AnonymousChild/{Preferences.Get("TeamId", "")}/{SelectedAnonymousChild.FId}");

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
                        if (SelectedAnonymousChild.Type == "Refugee")
                        {
                            _ = await DataService.Put((--StaticDataStore.TeamStats.TotalRefugeeChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalRefugeeChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "IDP")
                        {
                            _ = await DataService.Put((--StaticDataStore.TeamStats.TotalIDPChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalIDPChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "Return")
                        {
                            _ = await DataService.Put((--StaticDataStore.TeamStats.TotalReturnChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalReturnChilds");
                        }
                        else if (SelectedAnonymousChild.Type == "Guest")
                        {
                            _ = await DataService.Put((--StaticDataStore.TeamStats.TotalGuestChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalGuestChilds");
                        }
                        else
                        {
                            return;
                        }

                        StandardMessagesDisplay.ItemDeletedToast();

                        AnonymousChild?.Remove(SelectedAnonymousChild);
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
            if (SelectedAnonymousChild?.FId != null)
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
            AnonymousChild?.Clear();
        }
    }
}
