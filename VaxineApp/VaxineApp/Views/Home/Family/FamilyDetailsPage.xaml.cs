using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Home.Area;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyDetailsPage : ContentPage
    {
        public GetFamilyModel Family { get; set; }
        protected DbContext Data = new DbContext();
        public FamilyDetailsPage(GetFamilyModel family)
        {
            InitializeComponent();
            AddChildCommand = new Command(AddChild);
            Family = family;
            PageContent.BindingContext = this;
        }
        public FamilyDetailsPage()
        {

        }
        public ICommand AddChildCommand { private set; get; }

        public async void AddChild()
        {
            var route = $"//{new AddChildPage(Family.HouseNo)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddChildPage(Family.HouseNo));
            //var route = $"{new AddChildPage(Family.HouseNo)}";
            //await Shell.Current.GoToAsync(route);
        }
    }
}