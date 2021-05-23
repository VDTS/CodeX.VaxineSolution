using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Home.Status;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVaccinePage : ContentPage
    {
        public AddVaccinePage(int HouseNo)
        {
            InitializeComponent();
            BindingContext = new AddVaccineViewModel(HouseNo);
        }
    }
}