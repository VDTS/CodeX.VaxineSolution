using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Area.Masjeed;
using VaxineApp.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Area.Masjeed
{
    [QueryProperty(nameof(Masjeed), nameof(Masjeed))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasjeedDetailsPage : ContentPage
    {
        public string? Masjeed { get; set; }

        public MasjeedDetailsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Masjeed != null)
            {
                var result = JsonConvert.DeserializeObject<MasjeedModel>(Masjeed);
                if (result != null) BindingContext = new MasjeedDetailsViewModel(result);
            }
        }
    }
}