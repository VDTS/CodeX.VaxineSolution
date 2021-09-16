using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Family;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Family
{
    [QueryProperty(nameof(Family), nameof(Family))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFamilyPage : ContentPage
    {
        public string Family { get; set; }
        public EditFamilyPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
            BindingContext = new EditFamilyViewModel(result);
        }
    }
}