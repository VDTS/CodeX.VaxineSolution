﻿using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class EidtMasjeedViewModel : ViewModelBase
    {
        // Property
        private MasjeedModel masjeed;
        public MasjeedModel Masjeed
        {
            get
            {
                return masjeed;
            }
            set
            {
                masjeed = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        // ctor
        public EidtMasjeedViewModel(MasjeedModel masjeed)
        {
            // Property
            Masjeed = masjeed;

            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Masjeed);
            var data = await DataService.Put(jsonData, $"Masjeed/{Preferences.Get("TeamId", "")}/{Masjeed.FId}");
            if (data == "Submit")
            {
                StandardMessagesDisplay.EditDisplaymessage(Masjeed.MasjeedName);
                var route = $"//{nameof(MasjeedPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
        }
    }
}
