using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class EditInfluecerViewModel : BaseViewModel
    {
        private InfluencerModel _influencer;
        public InfluencerModel Influencer
        {
            get { return _influencer; }
            set
            {
                _influencer = value;
                RaisedPropertyChanged(nameof(Influencer));
            }
        }
        public ICommand UpdateInfluencerCommand { private set; get; }

        public EditInfluecerViewModel(InfluencerModel influencer)
        {
            Influencer = influencer;
            UpdateInfluencerCommand = new Command(UpdateInfluencer);
        }

        private async void UpdateInfluencer(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    } 
}
