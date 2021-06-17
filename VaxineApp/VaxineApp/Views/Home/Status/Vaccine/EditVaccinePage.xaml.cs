using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Status;
using VaxineApp.ViewModels.Home.Status.Vaccine;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status.Vaccine
{
    [QueryProperty(nameof(Vaccine), nameof(Vaccine))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditVaccinePage : ContentPage
    {
        public string Vaccine { get; set; }
        public EditVaccinePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<VaccineModel>(Vaccine);
            BindingContext = new EditVaccineViewModel(result);
        }
    }
}