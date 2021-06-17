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