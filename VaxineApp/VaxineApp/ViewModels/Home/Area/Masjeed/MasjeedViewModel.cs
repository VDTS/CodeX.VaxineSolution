using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : BaseViewModel
    {
        public ICommand AddMasjeedCommand { private set; get; }
        public MasjeedViewModel()
        {
            AddMasjeedCommand = new Command(AddMasjeed);
        }
        async void AddMasjeed()
        {
            var route = $"{nameof(AditMasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
