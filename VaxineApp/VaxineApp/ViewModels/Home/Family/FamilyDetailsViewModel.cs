using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyDetailsViewModel : BaseViewModel
    {
        public ICommand AddChildCommand { private set; get; }
        private ObservableCollection<ChildModel> _childs;
        public ObservableCollection<ChildModel> Childs
        {
            get { return _childs; }
            set
            {
                _childs = value;
                RaisedPropertyChanged(nameof(Childs));
            }
        }
        public GetFamilyModel Family { get; set; }
        public FamilyDetailsViewModel(GetFamilyModel family)
        {
            Family = family;
            Childs = new ObservableCollection<ChildModel>();
            LoadData();
            AddChildCommand = new Command(AddChild);
        }
        private async void LoadData()
        {
            var data = await Data.GetChild(Family.HouseNo);
            foreach (var item in data)
            {
                Childs.Add(
                    new ChildModel
                    {
                        FullName = item.FullName,
                        DOB = item.DOB,
                        Gender = item.Gender,
                        OPV0 = item.OPV0,
                        RINo = item.RINo
                    });
            }
        }
        public async void AddChild()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddChildPage(Family.HouseNo));
        }
    }
}
