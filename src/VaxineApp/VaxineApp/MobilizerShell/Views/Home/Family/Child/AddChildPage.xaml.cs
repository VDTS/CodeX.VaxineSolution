using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using VaxineApp.MobilizerShell.ViewModels.Home.Family.Child;
using VaxineApp.Models;
using VaxineApp.StaticData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Family.Child
{
    [QueryProperty(nameof(Family), nameof(Family))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        public string? Family { get; set; }
        public AddChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                if (Family != null)
                {
                    var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
                    if (result != null) BindingContext = new AddChildViewModel(result);
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