using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class InfluencerViewModel : BaseViewModel
    {
        private List<InfluencerModel> _influencer;
        public List<InfluencerModel> Influencer
        {
            get { return _influencer; }
            set
            {
                _influencer = value;
                RaisedPropertyChanged(nameof(Influencer));
            }
        }

        // Commands
        public ICommand GetInfluencerCommand { private set; get; }
        public ICommand AddInfluencerCommand { private set; get; }

        // Constructor
        public InfluencerViewModel()
        {
            Influencer = new List<InfluencerModel>();
            GetInfluencer();
            GetInfluencerCommand = new Command(GetInfluencer);
            AddInfluencerCommand = new Command(AddInfluencer);
        }

        public async void GetInfluencer()
        {
            var data = await Data.GetInfluencer("T");
            foreach (var item in data)
            {
                Influencer.Add(
                    new InfluencerModel
                    {
                       Name = item.Name,
                       Contact = item.Contact,
                       Position = item.Position, 
                       DoesHeProvidingSupport = item.DoesHeProvidingSupport
                    }
                    );
            }
        }

        // Route Methods
        async void AddInfluencer()
        {
            var route = $"{nameof(AditInfluencerPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
