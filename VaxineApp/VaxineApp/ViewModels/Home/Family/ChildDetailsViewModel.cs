using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using DataAccess;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.Home.Family
{
    public class ChildDetailsViewModel : BaseViewModel
    {
        private int _houseNo;
        public int HouseNo
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
            set
            {
                _fullName = value;
                RaisedPropertyChanged(nameof(FullName));
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
        [DataType(DataType.Date)]

        private DateTime _dOB;

        public DateTime DOB
        {
            get
            {
                if (_dOB == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return _dOB;
                }
            }
            set
            {
                _dOB = value;
                RaisedPropertyChanged(nameof(DOB));
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

        private int _rINO;

        public int RINo
        {
            get { return _rINO; }
            set
            {
                _rINO = value;
                RaisedPropertyChanged(nameof(RINo));
            }
        }



        public ICommand SaveDataCommand { private set; get; }
        public async void SaveData()
        {
            try
            {
                await Data.PostData(
                    new DataModel
                    {
                        FullName = FullName,
                        Gender = Gender,
                        DOB = DOB,
                        HouseNo = HouseNo,
                        OPV0 = OPV0,
                        RINo = RINo
                    }, "Child"
                    );
                var route = $"{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ChildDetailsViewModel()
        {
            SaveDataCommand = new Command(SaveData);
        }
    }
}
