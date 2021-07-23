using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Area.Masjeed;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Masjeed
{
    [QueryProperty(nameof(Masjeed), nameof(Masjeed))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasjeedDetailsPage : ContentPage
    {
        public string Masjeed { get; set; }

        public MasjeedDetailsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<MasjeedModel>(Masjeed);
            BindingContext = new MasjeedDetailsViewModel(result);
        }
    }
}