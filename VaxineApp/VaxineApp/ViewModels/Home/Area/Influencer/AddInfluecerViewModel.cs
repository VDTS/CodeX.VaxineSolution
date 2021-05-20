using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class AddInfluecerViewModel : BaseViewModel
    {
        // Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisedPropertyChanged(nameof(Name));
            }
        }
        private string _postition;
        public string Position
        {
            get { return _postition; }
            set
            {
                _postition = value;
                RaisedPropertyChanged(nameof(Position));
            }
        }
        private string _contact;
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisedPropertyChanged(nameof(Contact));
            }
        }
        private bool _doesHeProvidingSupport;
        public bool DoesHeProvidingSupport
        {
            get { return _doesHeProvidingSupport; }
            set
            {
                _doesHeProvidingSupport = value;
                RaisedPropertyChanged(nameof(DoesHeProvidingSupport));
            }
        }

        public ICommand SaveInfluencerCommand { private set; get; }
        public AddInfluecerViewModel()
        {
            SaveInfluencerCommand = new Command(SaveInfluencer);

        }

        // Methods
        public async void SaveInfluencer()
        {
            await Data.PostInfluencer(new InfluencerModel
            {
                Name = Name,
                Contact = Contact,
                DoesHeProvidingSupport = DoesHeProvidingSupport,
                Position = Position
            });

            var route = $"//{nameof(InfluencerPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
