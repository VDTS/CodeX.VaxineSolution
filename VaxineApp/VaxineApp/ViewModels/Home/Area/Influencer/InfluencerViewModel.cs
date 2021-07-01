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
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
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
                var isDeleteAccepted =  await StandardMessagesDisplay.DeleteDisplayMessage(SelectedInfluencer.Name);
                if (isDeleteAccepted)
                {
                    var data = await DataService.Delete($"Clinic/{Preferences.Get("TeamId", "")}/{SelectedInfluencer.FId}");
                    if (data == "Deleted")
                    {
                        influencers.Remove(SelectedInfluencer);
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
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
        }

        private async void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var data = await DataService.Get($"Influencer/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, InfluencerModel>>(data);
                foreach (KeyValuePair<string, InfluencerModel> item in clinic)
                {
                    influencers.Add(
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
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        async void GoToPostPage()
        {
            var route = $"{nameof(AddInfluencerPage)}";
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

        void Clear()
        {
            influencers.Clear();
        }
    }
}
