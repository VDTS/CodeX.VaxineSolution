using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Status;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildVaccinePage : ContentPage
    {
        public ChildVaccinePage(ChildModel child)
        {
            InitializeComponent();
            BindingContext = new ChildVaccineViewModel(child);
        }
    }
}