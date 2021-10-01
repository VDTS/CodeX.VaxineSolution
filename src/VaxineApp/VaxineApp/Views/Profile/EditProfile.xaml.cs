using Newtonsoft.Json;
using VaxineApp.Core.Models;
using VaxineApp.ViewModels.Home.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Profile
{
    [QueryProperty(nameof(Profile), nameof(Profile))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        public string? Profile { get; set; }
        public EditProfile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (Profile != null)
            {
                var result = JsonConvert.DeserializeObject<ProfileModel>(Profile);
                if (result != null) BindingContext = new EditProfileViewModel(result);
            }
        }
    }
}