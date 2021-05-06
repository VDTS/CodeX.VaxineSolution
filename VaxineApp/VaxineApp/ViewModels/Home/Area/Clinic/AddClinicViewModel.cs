using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class AddClinicViewModel :BaseViewModel
    {
        private string _clinicName;
        public string ClinicName
        {
            get { return _clinicName; }
            set
            {
                _clinicName = value;
                RaisedPropertyChanged(nameof(ClinicName));
            }
        }
        public string _fixed;
        public string Fixed
        {
            get { return _fixed; }
            set
            {
                _fixed = value;
                RaisedPropertyChanged(nameof(Fixed));
            }
        }
        public string _outreach;
        public string Outreach
        {
            get { return _outreach; }
            set
            {
                _outreach = value;
                RaisedPropertyChanged(nameof(Outreach));
            }
        }
        public ICommand SaveClinicCommand { private set; get; }
        public AddClinicViewModel()
        {
            SaveClinicCommand = new Command(SaveClinic);

        }
        public async void SaveClinic()
        {
            await Data.PostClinic(new ClinicModel
            {
                ClinicName = ClinicName,
                Fixed = Fixed,
                Outreach = Outreach
            }, "T");
            var route = $"//{nameof(ClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
