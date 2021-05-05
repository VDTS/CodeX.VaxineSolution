using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : BaseViewModel
    {

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
        public ICommand GetMasjeedCommand { private set; get; }


        // Constructor
        public MasjeedViewModel()
        {
            Masjeed = new List<MasjeedModel>();
            GetMasjeed();
            GetMasjeedCommand = new Command(GetMasjeed);
            AddMasjeedCommand = new Command(AddMasjeed);
        }

        // Methods

        public async void GetMasjeed()
        {
            var data = await Data.GetMasjeed("T");
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
    }
}
