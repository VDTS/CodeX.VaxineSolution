using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Home.Area.Clinic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Clinic
{
    [QueryProperty(nameof(Clinic), nameof(Clinic))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditClinicPage : ContentPage
    {
        public string Clinic { get; set; }
        public EditClinicPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ClinicModel>(Clinic);
            BindingContext = new EditClinicViewModel(result);
        }
    }
}