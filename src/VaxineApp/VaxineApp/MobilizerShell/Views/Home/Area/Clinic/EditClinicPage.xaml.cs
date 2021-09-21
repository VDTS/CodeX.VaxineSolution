using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Area.Clinic;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Area.Clinic
{
    [QueryProperty(nameof(Clinic), nameof(Clinic))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditClinicPage : ContentPage
    {
        public string? Clinic { get; set; }
        public EditClinicPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Clinic != null)
            {
                var result = JsonConvert.DeserializeObject<ClinicModel>(Clinic);
                if (result != null) BindingContext = new EditClinicViewModel(result);
            }
        }
    }
}