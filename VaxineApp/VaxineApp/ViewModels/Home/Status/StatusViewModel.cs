using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;

namespace VaxineApp.ViewModels.Home.Status
{
    public class StatusViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<ChildGroupbyFamilyModel> familyGroup;
        public ObservableCollection<ChildGroupbyFamilyModel> FamilyGroup
        {
            get
            {
                return familyGroup;
            }
            set
            {
                familyGroup = value;
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


        // Command

        public ICommand SelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }


        // ctor
        public StatusViewModel()
        {
            // Property
            FamilyGroup = new ObservableCollection<ChildGroupbyFamilyModel>();


            // Get
            Get();

            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        private async void Get()
        {
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (data == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (data == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (data == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (data == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
                foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
                {
                    var data2 = await DataService.Get($"Child/{item.Value.Id}");
                    if (data2 != "null" && data2 != "Error" && data2 != "ErrorTracked" && data2 != "ConnectionError")
                    {
                        var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                        List<ChildModel> lp = new List<ChildModel>();
                        foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                        {
                            lp.Add(
                            new ChildModel
                            {
                                Id = item2.Value.Id,
                                FullName = item2.Value.FullName,
                                Gender = item2.Value.Gender,
                                DOB = item2.Value.DOB,
                                OPV0 = item2.Value.OPV0,
                                RINo = item2.Value.RINo
                            });
                        }
                        FamilyGroup.Add(new ChildGroupbyFamilyModel(item.Value.HouseNo, lp));
                    }
                }
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
            FamilyGroup.Clear();
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void SaveAsPDF()
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public void GoToPostPage()
        {
            throw new NotImplementedException();
        }

        public void GoToPutPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToDetailsPage()
        {
            if (SelectedChild == null)
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedChild);
                var route = $"{nameof(ChildVaccinePage)}?Child={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedChild = null;
            }
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }
    }
}
