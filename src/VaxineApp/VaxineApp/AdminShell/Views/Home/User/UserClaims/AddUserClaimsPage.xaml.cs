using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.AdminShell.ViewModels.Home.User.UserClaims;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.User.UserClaims
{
    [QueryProperty(nameof(FullName), nameof(FullName))]
    [QueryProperty(nameof(Email), nameof(Email))]
    [QueryProperty(nameof(PhoneNumber), nameof(PhoneNumber))]
    [QueryProperty(nameof(Role), nameof(Role))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserClaimsPage : ContentPage
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public AddUserClaimsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (FullName != null && Email != null && PhoneNumber != null && Role != null)
            {
                BindingContext = new AddUserClaimsViewModel(FullName, Email, PhoneNumber, Role);
            }
        }
    }
}