using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace VaxineApp.ViewModels.Shared
{
    public class GoogleMapViewModel : BaseViewModel
    {
        private string _pageName;
        public string PageName {
            get { return _pageName; }
            set
            {
                _pageName = value;
                RaisedPropertyChanged(nameof(PageName));
            }
        }
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisedPropertyChanged(nameof(Longitude));
            }
        }
        private double _latitude;
        public double Latitude {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisedPropertyChanged(nameof(Latitude));
            }
        }
        private Position _position;
        public Position Position {
            get { return _position; }
            set
            {
                _position = value;
                RaisedPropertyChanged(nameof(Position));
            }
        }
        private string _label;
        public string Label {
            get { return _label; }
            set
            {
                _label = value;
                RaisedPropertyChanged(nameof(Label));
            }
        }
        private string _address;
        public string Address {
            get { return _address; }
            set
            {
                _address = value;
                RaisedPropertyChanged(nameof(Address));
            }
        }
        public GoogleMapViewModel()
        {
            GetLocation();
            PageName = "Google Map";
            Label = "Kandahar";
            Address = "Kandahar City, Kandahar, Af";
            //Position = new Position(Latitude, Longitude);
            Position = new Position(30.342, 40.454);

        }

        public async Task GetLocation()
        {
            // https://stackoverflow.com/questions/38137560/get-device-location-in-latitude-longitude-in-xamarin-forms
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                Longitude = location.Longitude;
                Latitude = location.Latitude;
            }
        }
    }
}
