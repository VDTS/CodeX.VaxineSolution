using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using VaxineApp.ViewModels.Base;
using Newtonsoft.Json;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class AddChildViewModel : BaseViewModel
    {
        GetFamilyModel Family;
        public ICommand AddChildCommand { private set; get; }
        public AddChildViewModel(GetFamilyModel _family)
        {
            Family = _family;
            AddChildCommand = new Command(AddChild);
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisedPropertyChanged(nameof(FullName));
            }
        }
        private DateTime _dOB;
        public DateTime DOB
        {
            get { return _dOB; }
            set
            {
                _dOB = value;
                RaisedPropertyChanged(nameof(DOB));
            }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisedPropertyChanged(nameof(Gender));
            }
        }
        private bool _oPV0;
        public bool OPV0
        {
            get { return _oPV0; }
            set
            {
                _oPV0 = value;
                RaisedPropertyChanged(nameof(OPV0));
            }
        }
        private int _rINo;
        public int RINo
        {
            get { return _rINo; }
            set
            {
                _rINo = value;
                RaisedPropertyChanged(nameof(RINo));
            }
        }

        private async void AddChild()
        {
            if (DateTime.UtcNow.Year - DOB.Year <= 5)
            {
                ChildModel clinic = new ChildModel()
                {
                    Id = Guid.NewGuid(),
                    FullName = FullName,
                    DOB = DOB.Date.ToUniversalTime(),
                    Gender = Gender,
                    OPV0 = OPV0,
                    RINo = RINo,
                    RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""))
                };
            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, $"Child/{Family.Id}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(FamilyListPage)}";
            await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Older than 5 yrs", "Children older than 5 years not allowed", "OK");
            }
        }
    }
}
