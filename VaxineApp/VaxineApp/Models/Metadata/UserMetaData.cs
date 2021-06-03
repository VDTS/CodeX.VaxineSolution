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
        public string DeviceManufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceVersion { get; set; }
        public string DevicePlatform { get; set; }
        public UserMetaData()
        {
            DeviceName = CrossDeviceInfo.Current.DeviceName;
            DeviceManufacturer = CrossDeviceInfo.Current.Manufacturer;
            DeviceModel = CrossDeviceInfo.Current.Model;
            DeviceVersion = CrossDeviceInfo.Current.Version;
            DevicePlatform = CrossDeviceInfo.Current.Platform.ToString();
        }
    }
}
