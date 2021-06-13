﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;
using VaxineApp.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace VaxineApp.ViewModels.Home.Status
{
    public class StatusViewModel : BaseViewModel
    {
        private ChildModel _selectedItem;

        public ChildModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisedPropertyChanged(nameof(SelectedItem));
            }
        }
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
        public ICommand SelectionChangedCommand { private set; get; }
        public ICommand FamiliesCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }

        public async void GetChild()
        {
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
                foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
                {
                    var data2 = await DataService.Get($"Child/{item.Value.Id}");
                    if (data2 != "null" & data2 != "Error")
                    {
                        var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                        List<ChildModel> lp = new List<ChildModel>();
                        foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                        {
                            lp.Add(
                            new ChildModel
                            {
                                Id = item2.Value.Id,
                                FullName = item2.Value.FullName,
                                Gender = item2.Value.Gender,
                                DOB = item2.Value.DOB,
                                OPV0 = item2.Value.OPV0,
                                RINo = item2.Value.RINo
                            });
                        }
                        FamilyGroup.Add(new ChildGroupbyFamilyModel(item.Value.HouseNo, lp));
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        public StatusViewModel()
        {
            SaveAsPDFCommand = new Command(SaveAsPDF);
            FamilyGroup = new ObservableCollection<ChildGroupbyFamilyModel>();
            GetChild();
            GetFamilyCommand = new AsyncCommand(Refresh);
            //RegistrationPageCommand = new Command(Add);
            //FamiliesCommand = new Command(Families);
            SelectionChangedCommand = new Command(SelectionChanged);
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void SelectionChanged(object obj)
        {
            if (SelectedItem == null)
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedItem);
                var route = $"{nameof(ChildVaccinePage)}?Child={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedItem = null;
            }
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
