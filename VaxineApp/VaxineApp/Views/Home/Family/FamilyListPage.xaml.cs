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
    public partial class FamilyListPage : ContentPage
    {
        public FamilyListPage()
        {
            InitializeComponent();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var family = e.CurrentSelection.FirstOrDefault() as GetFamilyModel;
            if (family == null)
            {
                return;
            }
            else
            {
                await Navigation.PushAsync(new FamilyDetailsPage(family));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}