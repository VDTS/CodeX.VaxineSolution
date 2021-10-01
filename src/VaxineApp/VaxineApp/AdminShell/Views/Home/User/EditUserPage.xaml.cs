
using Newtonsoft.Json;
using VaxineApp.AdminShell.ViewModels.Home.User;
using VaxineApp.Core.Models.AccountModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.User
{
    [QueryProperty(nameof(User), nameof(User))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        public string? User { get; set; }
        public EditUserPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (User != null)
            {
                var result = JsonConvert.DeserializeObject<FirebaseUserModel>(User);
                if (result != null) BindingContext = new EditUserViewModel(result);
            }
        }
    }
}