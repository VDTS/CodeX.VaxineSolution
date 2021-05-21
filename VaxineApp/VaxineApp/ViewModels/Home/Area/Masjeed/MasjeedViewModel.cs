using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : BaseViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
            }
        }
        private List<MasjeedModel> _masjeed;
        public List<MasjeedModel> Masjeed
        {
            get { return _masjeed; }
            set
            {
                _masjeed = value;
                RaisedPropertyChanged(nameof(Masjeed));
            }
        }
        // Commands
        public ICommand AddMasjeedCommand { private set; get; }
        public AsyncCommand GetMasjeedCommand { private set; get; }


        // Constructor
        public MasjeedViewModel()
        {
            Masjeed = new List<MasjeedModel>();
            GetMasjeed();
            GetMasjeedCommand = new AsyncCommand(Refresh);
            AddMasjeedCommand = new Command(AddMasjeed);
        }

        // Methods

        public async void GetMasjeed()
        {
            var data = await Data.GetMasjeed();
            foreach (var item in data)
            {
                Masjeed.Add(
                    new MasjeedModel
                    {
                        MasjeedName = item.MasjeedName,
                        KeyInfluencer = item.KeyInfluencer,
                        DoYouHavePermissionForAdsInMasjeed = item.DoYouHavePermissionForAdsInMasjeed,
                        DoesImamSupportsVaccine  = item.DoesImamSupportsVaccine
                    }
                    );
            }
        }
        // Route Methods
        async void AddMasjeed()
        {
            var route = $"{nameof(AditMasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetMasjeed();

            IsBusy = false;
        }

        void Clear()
        {
            Masjeed.Clear();
        }
    }
}
