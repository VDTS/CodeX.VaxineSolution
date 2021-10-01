using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Status.Vaccine;
using VaxineApp.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status.Vaccine
{
    [QueryProperty(nameof(Child), nameof(Child))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVaccinePage : ContentPage
    {
        public string Child { get; set; }
        public AddVaccinePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ChildModel>(Child);
            BindingContext = new AddVaccineViewModel(result);
        }
    }
}