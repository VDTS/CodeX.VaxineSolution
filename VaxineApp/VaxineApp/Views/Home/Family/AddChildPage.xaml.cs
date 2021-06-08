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
    [QueryProperty(nameof(FamilyId), nameof(FamilyId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        public string FamilyId { get; set; }
        public AddChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Guid.TryParse(FamilyId, out var result);
            BindingContext = new AddChildViewModel(result) ;
        }
    }
}