﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.ViewModels.Home.Area.Area;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : ViewModelBase, IDataCrud, IVMUtils
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

        private bool isToolbarIconsVisible;
        public bool IsToolbarIconsVisible
        {
            get
            {
                return isToolbarIconsVisible;
            }
            set
            {
                isToolbarIconsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isSearchVisible;
        public bool IsSearchVisible
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
            IsSearchVisible = true;
            IsToolbarIconsVisible = false;

            // Get
            Get();

            // Commands
            PullRefreshCommand = new Command(Refresh);
            DeleteCommand = new Command(Delete);
            GoToPostPageCommand = new Command(GoToPostPage);
            CancelSelectionCommand = new Command(CancelSelection);
            GoToPutPageCommand = new Command(GoToPutPage);
        }

        public void Clear()
        {
            Clinics.Clear();
        }

        public async void Delete()
        {
            if (SelectedClinic.Id != null)
            {
                var isDeleteAccepted = await App.Current.MainPage.DisplayAlert("", $"Do you want to delete {SelectedClinic.ClinicName}?", "Yes", "No");
                if (isDeleteAccepted)
                {
                    var data = await DataService.Delete($"Clinic/{Preferences.Get("TeamId", "")}/{SelectedClinic.FId}");
                    if (data == "Deleted")
                    {
                        Clinics.Remove(SelectedClinic);
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
                await App.Current.MainPage.DisplayAlert("No item selected", "Select an item to delete", "OK");
            }
        }

        public async void Get()
        {
            var data = await DataService.Get($"Clinic/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(data);
                foreach (KeyValuePair<string, ClinicModel> item in clinic)
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
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToMapPage()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        public async void GoToPostPage()
        {
            SelectedClinic = null;
            var route = $"{nameof(AddClinicPage)}";
            await Shell.Current.GoToAsync(route);
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
                await App.Current.MainPage.DisplayAlert("No Clinic", "Select a clinic", "OK");
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

        public async void SaveAsPDF()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
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
