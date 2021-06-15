using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Home.Area.School;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area.School
{
    [QueryProperty(nameof(School), nameof(School))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSchoolPage : ContentPage
    {
        public string School { get; set; }
        public EditSchoolPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<SchoolModel>(School);
            BindingContext = new EditSchoolViewModel(result);
        }
    }
}