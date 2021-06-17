using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using VaxineApp.Views.Home.Family.Child;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyDetailsViewModel : BaseViewModel
    {
        public ICommand AddChildCommand { private set; get; }
        public ICommand EditChildCommand { private set; get; }

        private ChildModel _selectedChild;

        public ChildModel SelectedChild
        {
            get { return _selectedChild; }
            set
            {
                _selectedChild = value;
                RaisedPropertyChanged(nameof(SelectedChild));
            }
        }


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
            SelectedChild = new ChildModel();
            Childs = new ObservableCollection<ChildModel>();
            LoadData();
            AddChildCommand = new Command(AddChild);
            EditChildCommand = new Command(EditChild);
        }
        private async void LoadData()
        {
            var data = await DataService.Get($"Child/{Family.Id}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data);
                foreach (KeyValuePair<string, ChildModel> item in clinic)
                {
                    Childs.Add(
                         new ChildModel
                         {
                             FullName = item.Value.FullName,
                             DOB = item.Value.DOB,
                             Gender = item.Value.Gender,
                             OPV0 = item.Value.OPV0,
                             RINo = item.Value.RINo
                         });
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        private async void EditChild()
        {
            if (SelectedChild.FullName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedChild);
                var route = $"{nameof(EditChildPage)}?Child={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedChild = null;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Child", "Select a child", "OK");
            }
        }
        public async void AddChild()
        {
            var JsonFamily = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(AddChildPage)}?Family={JsonFamily}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
