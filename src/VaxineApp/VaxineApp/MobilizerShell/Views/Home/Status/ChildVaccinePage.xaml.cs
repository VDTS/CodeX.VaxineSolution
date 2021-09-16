using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Status;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status
{
    [QueryProperty(nameof(Child), nameof(Child))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildVaccinePage : ContentPage
    {
        public string Child { get; set; }
        public ChildVaccinePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ChildModel>(Child);
            BindingContext = new ChildVaccineViewModel(result);
        }
    }
}