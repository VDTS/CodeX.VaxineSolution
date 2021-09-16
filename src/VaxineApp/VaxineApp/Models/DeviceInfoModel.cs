using System;
using Xamarin.Essentials;

namespace VaxineApp.Models
{
    public class DeviceInfoModel
    {
        public string DeviceName { get; set; }
        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceVersion { get; set; }
        public string DevicePlatform { get; set; }
        public string IMEI { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DeviceInfoModel()
        {
            TimeStamp = DateTime.Now;
            DeviceName = DeviceInfo.Name;
            DeviceManufacturer = DeviceInfo.Manufacturer;
            DeviceModel = DeviceInfo.Model;
            DeviceVersion = DeviceInfo.VersionString;
            DevicePlatform = DeviceInfo.Platform.ToString();

            GetLocation();
        }

        public async void GetLocation()
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
