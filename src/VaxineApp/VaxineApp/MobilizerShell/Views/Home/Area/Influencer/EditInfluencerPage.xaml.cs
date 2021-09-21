using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Area.Influencer;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Area.Influencer
{
    [QueryProperty(nameof(Influencer), nameof(Influencer))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInfluencerPage : ContentPage
    {
        public string? Influencer { get; set; }
        public EditInfluencerPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Influencer != null)
            {
                var result = JsonConvert.DeserializeObject<InfluencerModel>(Influencer);
                if (result != null) BindingContext = new EditInfluecerViewModel(result);
            }
        }
    }
}