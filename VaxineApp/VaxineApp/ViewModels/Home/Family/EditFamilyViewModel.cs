using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class EditFamilyViewModel : ViewModelBase
    {
        // Property
        private GetFamilyModel family;
        public GetFamilyModel Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                OnPropertyChanged();
            }
        }


        // Commands
        public ICommand PutCommand { private set; get; }
        // Constructor
        public EditFamilyViewModel(GetFamilyModel family)
        {
            // Property
            Family = family;

            // Command
            PutCommand = new Command(Put);
        }
        private async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Family);
            var data = await DataService.Put(jsonData, $"Family/{Preferences.Get("TeamId", "")}/{Family.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(FamilyListPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    }
}
