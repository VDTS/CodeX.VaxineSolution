using Newtonsoft.Json;
using System;
using VaxineApp.MobilizerShell.ViewModels.Home.Status.Vaccine;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status.Vaccine
{
    [QueryProperty(nameof(Vaccine), nameof(Vaccine))]
    [QueryProperty(nameof(ChildId), nameof(ChildId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditVaccinePage : ContentPage
    {
        public string Vaccine { get; set; }
        public string ChildId { get; set; }
        public EditVaccinePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<VaccineModel>(Vaccine);
            BindingContext = new EditVaccineViewModel(result, Guid.Parse(ChildId));
        }
    }
}