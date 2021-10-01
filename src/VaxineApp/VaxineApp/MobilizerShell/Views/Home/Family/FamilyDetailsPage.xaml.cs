using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Family;
using VaxineApp.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Family
{
    [QueryProperty(nameof(Family), nameof(Family))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyDetailsPage : ContentPage
    {
        public string? Family { get; set; }
        public FamilyDetailsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Family != null)
            {
                var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
                if (result != null) BindingContext = new FamilyDetailsViewModel(result);
            }
        }
    }
}