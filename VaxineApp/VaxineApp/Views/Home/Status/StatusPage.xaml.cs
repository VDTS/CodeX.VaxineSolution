using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        public StatusPage()
        {
            InitializeComponent();

        }

        async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var child = e.CurrentSelection.FirstOrDefault() as ChildModel;
            if (child == null)
            {
                return;
            }
            else
            {
                await Navigation.PushAsync(new ChildVaccinePage(child));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
        //private async void Registration_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new RegistrationPage());
        //}
    }
}