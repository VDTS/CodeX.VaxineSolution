using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.Models.Metadata
{
    public class UserMetaData
    {
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceVersion { get; set; }
        public string DevicePlatform { get; set; }
        public string IMEI { get; set; }
        public DateTime TimeStamp { get; set; }
        public UserMetaData()
        {
            TimeStamp = DateTime.Now;
            DeviceName = CrossDeviceInfo.Current.DeviceName;
            DeviceId = CrossDeviceInfo.Current.Id;
            DeviceManufacturer = CrossDeviceInfo.Current.Manufacturer;
            DeviceModel = CrossDeviceInfo.Current.Model;
            DeviceVersion = CrossDeviceInfo.Current.Version;
            DevicePlatform = CrossDeviceInfo.Current.Platform.ToString();
        }
    }
}
