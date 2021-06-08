using Newtonsoft.Json;
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
            var data = await DataService.Get($"Child/{Family.Id}");
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
        public async void AddChild()
        {
            var JsonFamily = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(AddChildPage)}?Family={JsonFamily}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
