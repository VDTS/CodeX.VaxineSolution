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
using Newtonsoft.Json;

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
            var data = await DataService.Get($"Family/c0cda6a9-759a-4e87-b8cb-49af170bd24e");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
            foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
            {
                var data2 = await DataService.Get($"Child/{item.Value.Id}");
                var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                List<ChildModel> lp = new List<ChildModel>();
                foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                {
                    lp.Add(
                    new ChildModel
                    {
                        Id = item2.Value.Id,
                        Gender = item2.Value.Gender,
                        DOB = item2.Value.DOB,
                        OPV0 = item2.Value.OPV0,
                        RINo = item2.Value.RINo
                    });
                }
                FamilyGroup.Add(new ChildGroupbyFamilyModel(item.Value.HouseNo, lp));
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
