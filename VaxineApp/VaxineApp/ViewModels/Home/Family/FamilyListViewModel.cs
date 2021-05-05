using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home.Family;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyListViewModel : BaseViewModel
    {
        private List<GetFamilyModel> _family;
        public List<GetFamilyModel> Family
        {
            get { return _family; }
            set
            {
                _family = value;
                RaisedPropertyChanged(nameof(Family));
            }
        }
        public ICommand AddFamilyCommand { private set; get; }
        public ICommand GetFamilyCommand { private set; get; }
        public FamilyListViewModel()
        {
            Family = new List<GetFamilyModel>();
            GetFamily();
            GetFamilyCommand = new Command(GetFamily);
            AddFamilyCommand = new Command(AddFamily);
        }

        public async void GetFamily()
        {
            var data = await Data.GetFamily("T");
            foreach (var item in data)
            {
                Family.Add(
                    new GetFamilyModel
                    {
                        ParentName = item.ParentName,
                        PhoneNumber = item.PhoneNumber,
                        HouseNo = item.HouseNo
                    }
                    );
            }
        }
        async void AddFamily()
        {
            var route = $"{nameof(AddFamilyPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
