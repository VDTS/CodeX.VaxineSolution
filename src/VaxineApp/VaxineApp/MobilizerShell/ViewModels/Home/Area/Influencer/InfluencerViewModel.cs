﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.MobilizerShell.Views.Home.Area.Influencer;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Influencer
{
    public class InfluencerViewModel : ViewModelBase
    {
        // Property

        private InfluencerModel selectedInfluencer;
        public InfluencerModel SelectedInfluencer
        {
            get
            {
                return selectedInfluencer;
            }
            set
            {
                selectedInfluencer = value;
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

        private ObservableCollection<InfluencerModel> influencers;
        public ObservableCollection<InfluencerModel> Influencers
        {
            get
            {
                return influencers;
            }
            set
            {
                influencers = value;
                OnPropertyChanged();
            }
        }

        // Commands

        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }

        // Constructor
        public InfluencerViewModel()
        {
            // Property
            SelectedInfluencer = new InfluencerModel();
            influencers = new ObservableCollection<InfluencerModel>();

            // Get
            Get();

            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
        }

        private async void GoToPutPage()
        {
            if (SelectedInfluencer.Name != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedInfluencer);
                var route = $"{nameof(EditInfluencerPage)}?Influencer={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedInfluencer = null;
            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }

        public async void Delete()
        {
            if (SelectedInfluencer.Name != null)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedInfluencer.Name);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"Clinic/{Preferences.Get("TeamId", "")}/{SelectedInfluencer.FId}");
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
                        _ = await DataService.Put((--StaticDataStore.TeamStats.TotalInfluencers).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalInfluencers");
                        StandardMessagesDisplay.ItemDeletedToast();

                        Influencers.Remove(SelectedInfluencer);

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

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Influencer/{Preferences.Get("TeamId", "")}");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, InfluencerModel>>(jData);
                foreach (KeyValuePair<string, InfluencerModel> item in data)
                {
                    Influencers.Add(
                        new InfluencerModel
                        {
                            Id = item.Value.Id,
                            FId = item.Key.ToString(),
                            Name = item.Value.Name,
                            Contact = item.Value.Contact,
                            Position = item.Value.Position,
                            DoesHeProvidingSupport = item.Value.DoesHeProvidingSupport
                        }
                        );
                }
            }
        }

        void GoToPostPage()
        {
            Shell.Current.ShowPopup(new AddInfluencerPage());
        }
        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Influencers.Clear();
        }
    }
}