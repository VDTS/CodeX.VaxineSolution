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

namespace VaxineApp.Views.Home.Profile
{
    [QueryProperty(nameof(Profile), nameof(Profile))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public string Profile { get; set; }
        public EditProfilePage()
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