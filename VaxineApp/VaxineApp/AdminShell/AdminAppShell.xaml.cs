using VaxineApp.AdminShell.Views.Home;
using VaxineApp.Views.Announcements;
using VaxineApp.Views.Help;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Settings.AboutUs;
using VaxineApp.Views.Settings.AppUpdates;
using VaxineApp.Views.Settings.Feedback;
using VaxineApp.Views.Settings.Font;
using VaxineApp.Views.Settings.Language;
using VaxineApp.Views.Settings.Main;
using VaxineApp.Views.Settings.Notifications;
using VaxineApp.Views.Settings.PrivacyPolicy;
using VaxineApp.Views.Settings.Themes;
using Xamarin.Forms;

namespace VaxineApp.AdminShell
{
    public partial class AdminAppShell : Xamarin.Forms.Shell
    {
        public AdminAppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ClusterPage), typeof(ClusterPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(EditProfile), typeof(EditProfile));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(HelpPage), typeof(HelpPage));
            Routing.RegisterRoute(nameof(AboutUsPage), typeof(AboutUsPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(PrivacyPolicyPage), typeof(PrivacyPolicyPage));
            Routing.RegisterRoute(nameof(AppUpdatesPage), typeof(AppUpdatesPage));
            Routing.RegisterRoute(nameof(FontPage), typeof(FontPage));
            Routing.RegisterRoute(nameof(ThemesPage), typeof(ThemesPage));
            Routing.RegisterRoute(nameof(LanguagePage), typeof(LanguagePage));
            Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        }
    }
}
