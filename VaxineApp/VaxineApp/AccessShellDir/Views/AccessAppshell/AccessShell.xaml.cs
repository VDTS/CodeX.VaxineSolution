using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
using Xamarin.Forms;

namespace VaxineApp.AccessShellDir.Views.AccessAppshell
{
    public partial class AccessShell : Xamarin.Forms.Shell
    {
        public AccessShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
        }
    }
}
