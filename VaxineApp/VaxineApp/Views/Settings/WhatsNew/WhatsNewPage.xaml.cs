using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Home.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Settings.WhatsNew
{
    [QueryProperty(nameof(Profile), nameof(Profile))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhatsNewPage : ContentPage
    {
        public WhatsNewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ProfileModel>(Profile);
            BindingContext = new EditProfileViewModel(result);
        }
    }
}