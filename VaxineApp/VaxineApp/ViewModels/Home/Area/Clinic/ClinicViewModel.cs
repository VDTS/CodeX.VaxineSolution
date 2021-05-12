using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.ViewModels.Home.Area.Area;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.CommunityToolkit.ObjectModel;
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
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
            }
        }

        //Commands
        public ICommand AddClinicCommand { private set; get; }
        public ICommand GetClinicCommand { private set; get; }

        // Constructor
        public ClinicViewModel()
        {
            GetClinic();
            GetClinicCommand = new AsyncCommand(Refresh);
            AddClinicCommand = new Command(AddClinic);
            Clinics = new List<ClinicModel>();
        }

        // Methods
        public async void GetClinic()
        {
            var data = await Data.GetClinic();
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
        
        // GoTo Routes
        async void AddClinic()
        {
            var route = $"{nameof(AddClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetClinic();

            IsBusy = false;
        }

        void Clear()
        {
            Clinics.Clear();
        }
    }
}
