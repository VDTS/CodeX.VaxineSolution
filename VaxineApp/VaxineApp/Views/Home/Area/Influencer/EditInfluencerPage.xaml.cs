using Newtonsoft.Json;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Area.Influencer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Influencer
{
    [QueryProperty(nameof(Influencer), nameof(Influencer))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInfluencerPage : ContentPage
    {
        public string Influencer { get; set; }
        public EditInfluencerPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<InfluencerModel>(Influencer);
            BindingContext = new EditInfluecerViewModel(result);
        }
    }
}