using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.ViewModels.Home.Area.Area;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : BaseViewModel
    {
        // Properties
        private List<ClinicModel> _clinics;
        public List<ClinicModel> Clinics
        {
            get { return _clinics; }
            set
            {
                _clinics = value;
                RaisedPropertyChanged(nameof(Clinics));
            }
        }

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
        // Commands
        public ICommand AddClinicCommand { private set; get; }
        public ICommand SaveClinicCommand { private set; get; }
        public ICommand GetClinicCommand { private set; get; }

        // Constructor
        public ClinicViewModel()
        {
            GetClinic();
            GetClinicCommand = new Command(GetClinic);
            AddClinicCommand = new Command(AddClinic);
            SaveClinicCommand = new Command(SaveClinic);
            Clinics = new List<ClinicModel>();
        }

        // Methods
        public async void GetClinic()
        {
            var data = await Data.GetNeClinic("T");
            foreach (var item in data)
            {
                Clinics.Add(
                    new ClinicModel
                    {
                        ClinicName = item.ClinicName,
                        Fixed = item.Fixed,
                        Outreach = item.Outreach
                    }
                    );
            }
        }
        public async void SaveClinic()
        {
            await Data.SaveClinic(new ClinicModel
            {
                ClinicName = ClinicName,
                Fixed = Fixed,
                Outreach = Outreach
            }, "T");
            var route = $"{nameof(ClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }
        // GoTo Routes
        async void AddClinic()
        {
            var route = $"{nameof(AddClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
