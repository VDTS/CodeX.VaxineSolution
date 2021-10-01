using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.MobilizerShell.Views.Home.Status;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Status
{
    public class StatusViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<ChildGroupbyFamilyModel>? familyGroup;
        public ObservableCollection<ChildGroupbyFamilyModel>? FamilyGroup
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

        private ChildModel? selectedChild;
        public ChildModel? SelectedChild
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

        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }


        // ctor
        public StatusViewModel()
        {
            // Property
            FamilyGroup = new ObservableCollection<ChildGroupbyFamilyModel>();

            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public void FirstLoad(object sender, EventArgs e)
        {
            if(FamilyGroup?.Count == 0)
            Get();
        }
        private async void Get()
        {
            var jData = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");

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
                    var data = JsonConvert.DeserializeObject<Dictionary<string, FamilyModel>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, FamilyModel> item in data)
                        {
                            var nestedJData = await DataService.Get($"Child/{item.Value.Id}");

                            if (nestedJData == "ConnectionError")
                            {
                                StandardMessagesDisplay.NoConnectionToast();
                            }
                            else if (nestedJData == "null")
                            {
                                StandardMessagesDisplay.NoDataDisplayMessage();
                            }
                            else if (nestedJData == "Error")
                            {
                                StandardMessagesDisplay.Error();
                            }
                            else if (nestedJData == "ErrorTracked")
                            {
                                StandardMessagesDisplay.ErrorTracked();
                            }
                            else
                            {
                                var nestedData = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(nestedJData);
                                List<ChildModel> lp = new List<ChildModel>();

                                if (nestedData != null)
                                    foreach (KeyValuePair<string, ChildModel> item2 in nestedData)
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
                                FamilyGroup?.Add(new ChildGroupbyFamilyModel(item.Value.HouseNo, lp));
                            }
                        }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
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
            FamilyGroup?.Clear();
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
