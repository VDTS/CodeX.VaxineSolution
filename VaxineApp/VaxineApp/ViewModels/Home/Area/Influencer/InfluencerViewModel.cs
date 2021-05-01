using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class InfluencerViewModel : BaseViewModel
    {
        public ICommand AddInfluencerCommand { private set; get; }
        public InfluencerViewModel()
        {
            AddInfluencerCommand = new Command(AddInfluencer);
        }
        async void AddInfluencer()
        {
            var route = $"{nameof(AditInfluencerPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
