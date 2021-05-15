﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using Xamarin.CommunityToolkit.ObjectModel;
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
        private GetFamilyModel _selectedFamily;

        public GetFamilyModel SelectedFamily
        {
            get { return _selectedFamily; }
            set
            {
                _selectedFamily = value;
                RaisedPropertyChanged(nameof(SelectedFamily));
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

        public ICommand AddFamilyCommand { private set; get; }
        public ICommand TapOnItemCommand { private set; get; }
        public AsyncCommand GetFamilyCommand { private set; get; }
        public FamilyListViewModel()
        {

            TapOnItemCommand = new Command(CollectionView_SelectionChanged);
            Family = new List<GetFamilyModel>();
            GetFamily();
            GetFamilyCommand = new AsyncCommand(Refresh);
            AddFamilyCommand = new Command(AddFamily);
        }

        public async void GetFamily()
        {
            var data = await Data.GetFamily();
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
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetFamily();

            IsBusy = false;
        }

        void Clear()
        {
            Family.Clear();
        }

        private async void CollectionView_SelectionChanged(object sender)
        {
            if (SelectedFamily == null)
            {
                return;
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new FamilyDetailsPage(SelectedFamily));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
