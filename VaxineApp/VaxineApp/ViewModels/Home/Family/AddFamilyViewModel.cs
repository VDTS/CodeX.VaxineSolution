using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Family;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class AddFamilyViewModel : BaseViewModel
    {
        // Properites

        private int _houseNo;
        public int HouseNo {
            get { return _houseNo; }
            set
            {
                _houseNo = value;
                RaisedPropertyChanged(nameof(HouseNo));
            }
        }
        private string _parentName;
        public string ParentName {
            get { return _parentName; }
            set
            {
                _parentName = value;
                RaisedPropertyChanged(nameof(ParentName));
            }
        }
        private string _phoneNumber;
        public string PhoneNumber {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisedPropertyChanged(nameof(PhoneNumber));
            }
        }

        // Commands

        public ICommand SaveFamilyCommand { private set; get; }
        // Constructor
        public AddFamilyViewModel()
        {
            SaveFamilyCommand = new Command(SaveFamily);
        }


        // Method
        public async void SaveFamily()
        {
            await Data.PostFamily(new FamilyModel
            {
                HouseNo = HouseNo,
                ParentName = ParentName,
                PhoneNumber = PhoneNumber
            }, "T");
            var route = $"{nameof(FamilyListPage)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}
