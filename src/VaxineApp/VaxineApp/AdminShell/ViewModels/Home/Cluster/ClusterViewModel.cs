using Newtonsoft.Json;
using System.Collections.Generic;
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
    public class ClusterViewModel : ViewModelBase
    {
        // Property
        private ClusterModel selectedCluster;
        public ClusterModel SelectedCluster
        {
            get
            {
                return selectedCluster;
            }
            set
            {
                selectedCluster = value;
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

        private ObservableCollection<ClusterModel> clusters;
        public ObservableCollection<ClusterModel> Clusters
        {
            get
            {
                return clusters;
            }
            set
            {
                clusters = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

        // ctor
        public ClusterViewModel()
        {
            // Property
            Clusters = new ObservableCollection<ClusterModel>();
            SelectedCluster = new ClusterModel();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public async void GoToDetailsPage()
        {
            if (SelectedCluster.AreEmpty())
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedCluster);
                var route = $"{nameof(ClusterDetailsPage)}?Cluster={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedCluster = null;
            }
        }


        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get("Cluster");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(jData);
                foreach (KeyValuePair<string, ClusterModel> item in data)
                {
                    Clusters.Add(
                        new ClusterModel
                        {
                            ClusterName = item.Value.ClusterName,
                            CurrentVaccinePeriodId = item.Value.CurrentVaccinePeriodId,
                            Id = item.Value.Id
                        }
                        );
                }
            }
        }

        async void GoToPostPage()
        {
            var route = $"{nameof(AddClusterPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Clusters.Clear();
        }
    }
}
