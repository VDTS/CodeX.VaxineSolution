using Newtonsoft.Json;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Area.Masjeed;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Masjeed
{
    [QueryProperty(nameof(Masjeed), nameof(Masjeed))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMasjeedPage : ContentPage
    {
        public string Masjeed { get; set; }
        public EditMasjeedPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<MasjeedModel>(Masjeed);
            BindingContext = new EidtMasjeedViewModel(result);
        }
    }
}