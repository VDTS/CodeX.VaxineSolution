
using VaxineApp.AdminShell.ViewModels.Home.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();

            var userViewModel = ((UserViewModel)this.BindingContext);
            userViewModel.Get();
        }
    }
}