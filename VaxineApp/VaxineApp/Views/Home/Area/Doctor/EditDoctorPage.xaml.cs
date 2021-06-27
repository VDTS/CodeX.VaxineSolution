using Newtonsoft.Json;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Area.Doctor;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Doctor
{
    [QueryProperty(nameof(Doctor), nameof(Doctor))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDoctorPage : ContentPage
    {
        public string Doctor { get; set; }
        public EditDoctorPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<DoctorModel>(Doctor);
            BindingContext = new EditDoctorViewModel(result);
        }
    }
}