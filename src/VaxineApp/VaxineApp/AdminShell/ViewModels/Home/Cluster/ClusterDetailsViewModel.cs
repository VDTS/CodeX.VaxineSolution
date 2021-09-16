using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.AdminShell.Views.Home.Cluster;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Cluster
{
    public class ClusterDetailsViewModel : ViewModelBase
    {
        // Property
        public ClusterModel Cluster { get; }

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

        private ObservableCollection<TeamModel> teams;

        public ObservableCollection<TeamModel> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand GoToLocationCommand { private set; get; }
        public ICommand ShowLocationCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }

        // ctor
        public ClusterDetailsViewModel(ClusterModel cluster)
        {
            // Property
            Cluster = cluster;
            Teams = new ObservableCollection<TeamModel>();

            // Command
            GoToLocationCommand = new Command(GoToLocation);
            ShowLocationCommand = new Command(ShowLocation);
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
            PullRefreshCommand = new Command(Refresh);
        }

        private async void GoToPutPage()
        {
            if (Cluster.ClusterName != null)
            {
                var jData = JsonConvert.SerializeObject(Cluster);
                var route = $"{nameof(EditClusterPage)}?Cluster={jData}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private async void Delete(object obj)
        {

            if (!Cluster.AreEmpty())
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(Cluster.ClusterName);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"Cluster/{Cluster.FId}");
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
                        StandardMessagesDisplay.ItemDeletedToast();

                        await Shell.Current.GoToAsync("../");
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public async void GoToLocation()
        {

        }
        private async void ShowLocation(object obj)
        {
            //var location = new Location(Convert.ToDouble(Masjeed.Latitude), Convert.ToDouble(Masjeed.Longitude));
            //await Map.OpenAsync(location);
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            //Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public void Clear()
        {
            Teams.Clear();
        }
    }
}