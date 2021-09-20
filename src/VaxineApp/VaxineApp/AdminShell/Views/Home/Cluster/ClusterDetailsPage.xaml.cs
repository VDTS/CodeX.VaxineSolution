using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using VaxineApp.AdminShell.ViewModels.Home.Cluster;
using VaxineApp.Models;
using VaxineApp.StaticData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.Cluster
{
    [QueryProperty(nameof(Cluster), nameof(Cluster))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClusterDetailsPage : ContentPage
    {
        public string? Cluster { get; set; }
        public ClusterDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                if (Cluster != null)
                {
                    var result = JsonConvert.DeserializeObject<ClusterModel>(Cluster);
                    if (result != null) BindingContext = new ClusterDetailsViewModel(result);
                }
            }
            catch (Exception ex) 
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }
    }
}