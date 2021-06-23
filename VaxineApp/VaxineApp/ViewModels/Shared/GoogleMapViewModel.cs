using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.MVVMHelper;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace VaxineApp.ViewModels.Shared
{
    public class GoogleMapViewModel : ViewModelBase
    {
        // Property
        private string pageName;
        public string PageName {
            get
            {
                return pageName;
            }
            set
            {
                pageName = value;
                OnPropertyChanged();
            }
        }

        private double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        private double latitude;
        public double Latitude {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        private Position position;
        public Position Position {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        private string label;
        public string Label {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        // ctor
        public GoogleMapViewModel()
        {
            // Property
            PageName = "Google Map";
            Label = "Kandahar";
            Address = "Kandahar City, Kandahar, Af";

            // Get
            GetLocation();
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
