using System;
using System.Collections.Generic;
using VaxineApp.Models;
using VaxineApp.Views;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

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
        }
    }
}
