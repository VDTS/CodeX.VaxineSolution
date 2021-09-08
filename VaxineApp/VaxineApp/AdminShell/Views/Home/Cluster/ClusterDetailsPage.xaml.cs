using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.AdminShell.ViewModels.Home.Cluster;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.Cluster
{
    [QueryProperty(nameof(Cluster), nameof(Cluster))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClusterDetailsPage : ContentPage
    {
        public string Cluster { get; set; }
        public ClusterDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ClusterModel>(Cluster);
            BindingContext = new ClusterDetailsViewModel(result);
        }
    }
}