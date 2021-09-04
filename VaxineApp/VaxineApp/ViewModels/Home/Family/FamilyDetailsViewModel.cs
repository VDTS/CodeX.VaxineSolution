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
using VaxineApp.Views.Home.Family;
using VaxineApp.Views.Home.Family.Child;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyDetailsViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Property
        private SearchBoxVisibility isSearchVisible;
        public SearchBoxVisibility IsSearchVisible
        {
            get
            {
                return isSearchVisible;
            }
            set
            {
                isSearchVisible = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ChildModel> childs;
        public ObservableCollection<ChildModel> Childs
        {
            get
            {
                return childs;
            }
            set
            {
                childs = value;
                OnPropertyChanged();
            }
        }

        private ChildModel selectedChild;
        public ChildModel SelectedChild
        {
            get
            {
                return selectedChild;
            }
            set
            {
                selectedChild = value;
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

        public FamilyModel Family { get; set; }


        // Command
        public ICommand GoToSubPutPageCommand { private set; get; }
        public ICommand PostCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand DialerCommand { private set; get; }

        public ICommand SubPutCommand { private set; get; }
        public ICommand GoToSubPostPageCommand { private set; get; }
        public ICommand SubDeleteCommand { private set; get; }
        public ICommand SelectionCommand { private set; get; }
        public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand ShareOnAppsCommand { private set; get; }

        // ctor
        public FamilyDetailsViewModel(FamilyModel family)
        {
            // Property
            Family = family;
            SelectedChild = new ChildModel();
            Childs = new ObservableCollection<ChildModel>();
            IsSearchVisible = SearchBoxVisibility.Expanded;

            // Get
            Get();

            // Command
            GoToSubPostPageCommand = new Command(GoToSubPostPage);
            GoToSubPutPageCommand = new Command(GoToSubPutPage);
            GoToPutPageCommand = new Command(GoToPutPage);
            DialerCommand = new Command(Dialer);
            PullRefreshCommand = new Command(Refresh);
            SubDeleteCommand = new Command(SubDelete);
            DeleteCommand = new Command(Delete);
            ShareOnAppsCommand = new Command(ShareOnApps);
        }

        private async void ShareOnApps()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = $"Share {Family.ParentName}'s Family",
                Text = ShareFamilyText()
            });

        }

        private string ShareFamilyText()
        {
            string shareText = $"{Family.ParentName}'s Family: {Environment.NewLine}";
            foreach (var item in Childs)
            {
                shareText += $"{item.FullName} {Environment.NewLine}";
            }

            return shareText;
        }
        private async void SubDelete(object obj)
        {
            if (SelectedChild.FId != null)
            {
                var VaccineData = await DataService.Get($"Vaccine/{SelectedChild.Id}");
                if(VaccineData == "ConnectionError")
                {
                    StandardMessagesDisplay.NoConnectionToast();
                }
                else if (VaccineData == "null")
                {
                    var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedChild.FullName);
                    if (isDeleteAccepted)
                    {
                        var deleteResponse = await DataService.Delete($"Child/{Family.Id}/{SelectedChild.FId}");
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
                            _ = await DataService.Put((--StaticDataStore.TeamStats.TotalChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamId", "")}/TotalChilds");

                            StandardMessagesDisplay.ItemDeletedToast();

                            Childs.Remove(SelectedChild);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var num = JsonConvert.DeserializeObject<Dictionary<string, VaccineModel>>(VaccineData);
                    StandardMessagesDisplay.ChildRecursiveDeletionNotAllowed(SelectedChild.FullName, num.Values.Count);
                }
            }
        }

        private async void Dialer()
        {
            string number = Family.PhoneNumber;
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException)
            {
                await App.Current.MainPage.DisplayAlert("Error!", "Null number", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Not supported on this device", "OK");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't dail the number", "OK");
            }
        }

        private async void GoToSubPutPage()
        {
            if (SelectedChild.FullName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedChild);
                var route = $"{nameof(EditChildPage)}?Child={jsonClinic}&FamilyId={Family.Id}";
                await Shell.Current.GoToAsync(route);
                SelectedChild = null;
            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }

        private async void GoToSubPostPage()
        {
            var JsonFamily = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(AddChildPage)}?Family={JsonFamily}";
            await Shell.Current.GoToAsync(route);
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Childs.Clear();
        }
        public async void Delete()
        {
            if (Childs.Count == 0)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage($"{Family.ParentName}'s Family ");
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"Family/{Preferences.Get("TeamId", "")}/{Family.FId}");
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
                        _ = await DataService.Put((--StaticDataStore.TeamStats.TotalHouseholds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalHouseholds");

                        StandardMessagesDisplay.ItemDeletedToast();

                        var route = "..";
                        await Shell.Current.GoToAsync(route);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                StandardMessagesDisplay.FamilyRecursiveDeletionNotAllowed(Family.ParentName, Childs.Count);
            }
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Child/{Family.Id}");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(jData);
                foreach (KeyValuePair<string, ChildModel> item in data)
                {
                    Childs.Add(
                         new ChildModel
                         {
                             FId = item.Key.ToString(),
                             Id = item.Value.Id,
                             FullName = item.Value.FullName,
                             DOB = item.Value.DOB,
                             Gender = item.Value.Gender,
                             OPV0 = item.Value.OPV0,
                             RINo = item.Value.RINo,
                             RegisteredBy = item.Value.RegisteredBy
                         });
                }
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

        public void GoToPostPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToPutPage()
        {
            var jsonClinic = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(EditFamilyPage)}?Family={jsonClinic}";
            await Shell.Current.GoToAsync(route);
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
