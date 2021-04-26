using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Services;
using VaxineApp.Views.Home;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class ChildViewModel : BaseViewModel
    {
        DbContext firebaseHelper = new DbContext();

        private string _houseNo;
        public string HouseNo
        {
            get { return _houseNo; }
            set
            {
                _houseNo = value;
                RaisedPropertyChanged(nameof(HouseNo));
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private DateTime _dOB;

        public DateTime DOB
        {
            get { return _dOB; }
            set { _dOB = value; }
        }

        private string _oPV0;

        public string OPV0
        {
            get { return _oPV0; }
            set { _oPV0 = value; }
        }

        private string _rINO;

        public string RINo
        {
            get { return _rINO; }
            set { _rINO = value; }
        }



        public ICommand SaveDataCommand { private set; get; }
        public async void SaveData()
        {
            try
            {
                await firebaseHelper.Add(
                    new ChildModel
                    {
                        FullName = FullName,
                        Gender = Gender,
                        DOB = DOB,
                        HouseNo = HouseNo,
                        OPV0 = OPV0,
                        RINo = RINo
                    }
                    );
                var route = $"{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ChildViewModel()
        {
            SaveDataCommand = new Command(SaveData);
        }
    }
}
