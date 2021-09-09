﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Routes;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<ClinicModel> clinics;
        public ObservableCollection<ClinicModel> Clinics
        {
            get
            {
                return clinics;
            }
            set
            {
                clinics = value;
                OnPropertyChanged();
            }
        }

        private ClinicModel selectedClinic;
        public ClinicModel SelectedClinic
        {
            get
            {
                return selectedClinic;
            }
            set
            {
                selectedClinic = value;
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
        // Crud Commands
        public ICommand PutCommand { private set; get; }
        public ICommand PostCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        // Crud Commands fo child item

        public ICommand SubPutCommand { private set; get; }
        public ICommand SubPostCommand { private set; get; }
        public ICommand SubDeleteCommand { private set; get; }

        // Other Commands
        public ICommand SelectionCommand { private set; get; }
        public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }


        // Goto Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }
        public ICommand GoToMapPageCommand { private set; get; }

        // Goto Commands fo child item
        public ICommand GoToSubPostPageCommand { private set; get; }
        public ICommand GoToSubPutPageCommand { private set; get; }
        public ICommand GoToSubDetailsPageCommand { private set; get; }

        // Functional Commands
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DialerCommand { private set; get; }

        // ctor
        public ClinicViewModel()
        {
            // Property
            Clinics = new ObservableCollection<ClinicModel>();
            SelectedClinic = new ClinicModel();

            // Get
            Get();

            // Commands
            PullRefreshCommand = new Command(Refresh);
            DeleteCommand = new Command(CanExecuteDelete);
            GoToPostPageCommand = new Command(GoToPostPage);
            CancelSelectionCommand = new Command(CancelSelection);
            GoToPutPageCommand = new Command(GoToPutPage);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToMapPageCommand = new Command(GoToMapPage);
        }

        public void Clear()
        {
            Clinics.Clear();
        }

        public async void CanExecuteDelete()
        {
            if (SelectedClinic.ClinicName != null)
            {

                var acceptAction = new SnackBarActionOptions()
                {
                    Action = () => ExecuteDelete(),
                    ForegroundColor = Color.FromHex("#0990bf"),
                    BackgroundColor = Color.FromHex("#e2e2e2"),
                    Text = "OK",
                    Padding = 12
                };

                var options = new SnackBarOptions()
                {
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.FromHex("#505050"),
                        Padding = 12,
                        Message = "Do you want to move this to recycle bin?"
                    },

                    BackgroundColor = Color.FromHex("#e2e2e2"),
                    Duration = TimeSpan.FromSeconds(5),
                    CornerRadius = 12,
                    Actions = new[] { acceptAction }

                };
                await App.Current.MainPage.DisplaySnackBarAsync(options);

            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }
        public async Task ExecuteDelete()
        {
            var deleteResponse = await DataService.Delete($"Clinic/{Preferences.Get("TeamId", "")}/{SelectedClinic.FId}");

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
                _ = await DataService.Put((--StaticDataStore.TeamStats.TotalClinics).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalClinics");

                StandardMessagesDisplay.ItemDeletedToast();
                Clinics.Remove(SelectedClinic);
            }
        }
        public async void Get()
        {
            var jData = await DataService.Get($"Clinic/{Preferences.Get("TeamId", "")}");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(jData);
                foreach (KeyValuePair<string, ClinicModel> item in data)
                {
                    Clinics.Add(
                            new ClinicModel
                            {
                                FId = item.Key,
                                ClinicName = item.Value.ClinicName,
                                Fixed = item.Value.Fixed,
                                Outreach = item.Value.Outreach
                            }
                        );
                }
            }
        }

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public void GoToMapPage()
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public void GoToPostPage()
        {
            SelectedClinic = null;
            StandardRoutes.GoToAddPage("AddClinicPage");
        }

        public async void GoToPutPage()
        {
            if (SelectedClinic.ClinicName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedClinic);
                var route = $"{nameof(EditClinicPage)}?Clinic={jsonClinic}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
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
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public void CancelSelection()
        {
            if (SelectedClinic.Id != null)
            {
                SelectedClinic = null;
            }
            else
            {
                return;
            }
        }
    }
}
