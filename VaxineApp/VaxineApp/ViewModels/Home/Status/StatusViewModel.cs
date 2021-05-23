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
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;
using VaxineApp.ViewModels.Base;

namespace VaxineApp.ViewModels.Home.Status
{
    public class StatusViewModel : BaseViewModel
    {
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
        private ObservableCollection<ChildGroupbyFamilyModel> _childGroupedbyFamily;
        public ObservableCollection<ChildGroupbyFamilyModel> FamilyGroup
        {
            get { return _childGroupedbyFamily; }
            set
            {
                _childGroupedbyFamily = value;
                RaisedPropertyChanged(nameof(FamilyGroup));
            }

        }
        public ICommand RegistrationPageCommand { private set; get; }
        public AsyncCommand GetFamilyCommand { private set; get; }
        public ICommand FamiliesCommand { private set; get; }

        public async void GetChild()
        {
            var family = await Data.GetFamily();
            foreach (var item in family)
            {
                var child = await Data.GetChild(item.HouseNo);
                List<ChildModel> lp = new List<ChildModel>();
                foreach (var item2 in child)
                {
                    lp.Add(
                    new ChildModel
                    {
                            HouseNo = item.HouseNo,
                            FullName = item2.FullName,
                            Gender = item2.Gender,
                            DOB = item2.DOB,
                            OPV0 = item2.OPV0,
                            RINo = item2.RINo
                    });
                }
                FamilyGroup.Add(new ChildGroupbyFamilyModel(item.HouseNo, lp));
            }

        }


        public StatusViewModel()
        {
            FamilyGroup = new ObservableCollection<ChildGroupbyFamilyModel>();
            GetChild();
            GetFamilyCommand = new AsyncCommand(Refresh);
            //RegistrationPageCommand = new Command(Add);
            //FamiliesCommand = new Command(Families);
            //CollectionView_SelectionChangedCommand = new Command<>(CollectionView_SelectionChanged);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetChild();

            IsBusy = false;
        }

        void Clear()
        {
            FamilyGroup.Clear();
        }
    }
}
