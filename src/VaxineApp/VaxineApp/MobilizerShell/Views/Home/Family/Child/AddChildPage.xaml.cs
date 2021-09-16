using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Family.Child;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Family.Child
{
    [QueryProperty(nameof(Family), nameof(Family))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        public string Family { get; set; }
        public AddChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
            BindingContext = new AddChildViewModel(result);
        }
    }
}