using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Family;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
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
            var result = JsonConvert.DeserializeObject<GetFamilyModel>(Family);
            BindingContext = new EditFamilyViewModel(result);
        }
    }
}