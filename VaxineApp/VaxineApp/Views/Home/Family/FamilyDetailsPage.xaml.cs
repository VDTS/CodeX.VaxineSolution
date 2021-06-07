using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Family;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyDetailsPage : ContentPage
    {
        public FamilyDetailsPage(GetFamilyModel family)
        {
            InitializeComponent();
            BindingContext = new FamilyDetailsViewModel(family);
        }
    }
}