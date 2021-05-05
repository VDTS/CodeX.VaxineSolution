using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyDetailsPage : ContentPage
    {
        GetFamilyModel Family { get; set; }
        public FamilyDetailsPage(GetFamilyModel family)
        {
            InitializeComponent();
            Family = family;
            this.BindingContext = Family;
        }
    }
}