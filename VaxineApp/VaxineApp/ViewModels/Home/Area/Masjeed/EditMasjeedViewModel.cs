using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class EidtMasjeedViewModel : BaseViewModel
    {
        private MasjeedModel _masjeed;
        public MasjeedModel Masjeed
        {
            get { return _masjeed; }
            set
            {
                _masjeed = value;
                RaisedPropertyChanged(nameof(Masjeed));
            }
        }
        public ICommand UpdateMasjeedCommand { private set; get; }

        public EidtMasjeedViewModel(MasjeedModel masjeed)
        {
            Masjeed = masjeed;
            UpdateMasjeedCommand = new Command(UpdateMasjeed);
        }

        private async void UpdateMasjeed(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    }
}
