using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
using Xamarin.Forms;

namespace VaxineApp
{
    public partial class AccessShell : Xamarin.Forms.Shell
    {
        public AccessShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
            Routing.RegisterRoute(nameof(ResetByEmail), typeof(ResetByEmail));
            Routing.RegisterRoute(nameof(ResetByPhoneNumber), typeof(ResetByPhoneNumber));

        }
    }
}
