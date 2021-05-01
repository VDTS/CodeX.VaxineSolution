using System;
using System.Collections.Generic;
using VaxineApp.Models;
using VaxineApp.Views;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using System.Windows.Input;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Home.Area.Area;
using VaxineApp.Views.Home.Area.Clinic;
using VaxineApp.Views.Home.Area.Doctor;
using VaxineApp.Views.Home.Area.Influencer;
using VaxineApp.Views.Home.Area.Masjeed;
using VaxineApp.Views.Home.Area.School;
using VaxineApp.Views.Home.Registration;

namespace VaxineApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(StatusPage), typeof(StatusPage));
            Routing.RegisterRoute(nameof(VaccinePage), typeof(VaccinePage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(EditProfile), typeof(EditProfile));
            Routing.RegisterRoute(nameof(AreaPage), typeof(AreaPage));
            Routing.RegisterRoute(nameof(EditAreaPage), typeof(EditAreaPage));
            Routing.RegisterRoute(nameof(ClinicPage), typeof(ClinicPage));
            Routing.RegisterRoute(nameof(DoctorPage), typeof(DoctorPage));
            Routing.RegisterRoute(nameof(InfluencerPage), typeof(InfluencerPage));
            Routing.RegisterRoute(nameof(MasjeedPage), typeof(MasjeedPage)); 
            Routing.RegisterRoute(nameof(SchoolPage), typeof(SchoolPage));
            Routing.RegisterRoute(nameof(FamilyPage), typeof(FamilyPage));

        }
    }
}
