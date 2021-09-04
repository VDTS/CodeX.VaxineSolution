using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Family.Child;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family.Child
{
    [QueryProperty(nameof(Family), nameof(Family))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        public string Family { get; set; }
        public AddChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<FamilyModel>(Family);
            BindingContext = new AddChildViewModel(result) ;
        }
    }
}