﻿using System;
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
using VaxineApp.Views.Home.Family;
using VaxineApp.Views.Login;
using VaxineApp.Views.Settings;
using VaxineApp.Views.Help;
using VaxineApp.Views.Shared;
using VaxineApp.Views.Settings.AppUpdates;
using VaxineApp.Views.Settings.AboutUs;
using VaxineApp.Views.Settings.Feedback;
using VaxineApp.Views.Settings.PrivacyPolicy;
using VaxineApp.Views.Settings.Main;

namespace VaxineApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(StatusPage), typeof(StatusPage));
            Routing.RegisterRoute(nameof(AddVaccinePage), typeof(AddVaccinePage));
            Routing.RegisterRoute(nameof(ChildVaccinePage), typeof(ChildVaccinePage));
           
            Routing.RegisterRoute(nameof(FamilyListPage), typeof(FamilyListPage));
            Routing.RegisterRoute(nameof(FamilyDetailsPage), typeof(FamilyDetailsPage));
            Routing.RegisterRoute(nameof(AddFamilyPage), typeof(AddFamilyPage));
            Routing.RegisterRoute(nameof(AddChildPage), typeof(AddChildPage));
            Routing.RegisterRoute(nameof(GoogleMapPage), typeof(GoogleMapPage));

                Routing.RegisterRoute(nameof(AreaPage), typeof(AreaPage));
                Routing.RegisterRoute(nameof(EditAreaPage), typeof(EditAreaPage));

                Routing.RegisterRoute(nameof(MasjeedPage), typeof(MasjeedPage));
                Routing.RegisterRoute(nameof(AddMasjeedPage), typeof(AddMasjeedPage));

                Routing.RegisterRoute(nameof(SchoolPage), typeof(SchoolPage));
                Routing.RegisterRoute(nameof(AddSchoolPage), typeof(AddSchoolPage));

                Routing.RegisterRoute(nameof(InfluencerPage), typeof(InfluencerPage));
                Routing.RegisterRoute(nameof(AddInfluencerPage), typeof(AddInfluencerPage));

                Routing.RegisterRoute(nameof(ClinicPage), typeof(ClinicPage));
                Routing.RegisterRoute(nameof(AddClinicPage), typeof(AddClinicPage));

                Routing.RegisterRoute(nameof(DoctorPage), typeof(DoctorPage));
                Routing.RegisterRoute(nameof(AddDoctorPage), typeof(AddDoctorPage));

            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(EditProfile), typeof(EditProfile));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(HelpPage), typeof(HelpPage));
            Routing.RegisterRoute(nameof(AboutUsPage), typeof(AboutUsPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(PrivacyPolicyPage), typeof(PrivacyPolicyPage));
            Routing.RegisterRoute(nameof(AppUpdatesPage), typeof(AppUpdatesPage));
        }
    }
}
