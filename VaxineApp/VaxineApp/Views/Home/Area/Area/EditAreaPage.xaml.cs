using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Area.Area;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.Area
{
    [QueryProperty(nameof(Team), nameof(Team))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAreaPage : ContentPage
    {
        public string Team { get; set; }
        public EditAreaPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<TeamModel>(Team);
            BindingContext = new EditAreaViewModel(result);
        }
    }
}