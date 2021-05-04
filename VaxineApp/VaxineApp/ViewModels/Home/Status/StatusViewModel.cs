using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using DataAccess;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status
{
    public class StatusViewModel : BaseViewModel
    {
        private List<ChildModel> _childs;
        public List<ChildModel> Childs
        {
            get { return _childs; }
            set
            {
                _childs = value;
                RaisedPropertyChanged(nameof(Childs));
            }

        }
        public ICommand RegistrationPageCommand { private set; get; }
        public ICommand FamiliesCommand { private set; get; }

        //public ICommand CollectionView_SelectionChangedCommand { private set; get; }
        //public async void Add()
        //{
        //    var route = $"{nameof(RegistrationPage)}";
        //    await Shell.Current.GoToAsync(route);
        //}
        public async void GetChild()
        {
            var child = await Data.GetChild();
            foreach (var item in child)
            {
                Childs.Add(
                        new ChildModel
                        {
                            FullName = item.FullName,
                            Gender = item.Gender,
                            DOB = item.DOB,
                            HouseNo = item.HouseNo,
                            OPV0 = item.OPV0,
                            RINo = item.RINo
                        }
                    );
            }
        }


        public StatusViewModel()
        {
            Childs = new List<ChildModel>();
            GetChild();
            //RegistrationPageCommand = new Command(Add);
            //FamiliesCommand = new Command(Families);
            //CollectionView_SelectionChangedCommand = new Command<>(CollectionView_SelectionChanged);
        }

        //async void Families(object obj)
        //{
        //    var route = $"{nameof(FamilyPage)}";
        //    await Shell.Current.GoToAsync(route);
        //}
    }
}
