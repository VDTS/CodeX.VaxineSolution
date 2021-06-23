﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using VaxineApp.Views.Home.Family.Child;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyDetailsViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Property
        private ObservableCollection<ChildModel> childs;
        public ObservableCollection<ChildModel> Childs
        {
            get
            {
                return childs;
            }
            set
            {
                childs = value;
                OnPropertyChanged();
            }
        }

        private ChildModel selectedChild;
        public ChildModel SelectedChild
        {
            get
            {
                return selectedChild;
            }
            set
            {
                selectedChild = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public GetFamilyModel Family { get; set; }


        // Command
        public ICommand GoToSubPutPageCommand { private set; get; }
        public ICommand PostCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand DialerCommand { private set; get; }

        public ICommand SubPutCommand { private set; get; }
        public ICommand GoToSubPostPageCommand { private set; get; }
        public ICommand SubDeleteCommand { private set; get; }
        public ICommand SelectionCommand { private set; get; }
        public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }

        // ctor
        public FamilyDetailsViewModel(GetFamilyModel family)
        {
            // Property
            Family = family;
            SelectedChild = new ChildModel();
            Childs = new ObservableCollection<ChildModel>();

            // Get
            Get();

            // Command
            GoToSubPostPageCommand = new Command(GoToSubPostPage);
            GoToSubPutPageCommand = new Command(GoToSubPutPage);
            GoToPutPageCommand = new Command(GoToPutPage);
            DialerCommand = new Command<string>(Dialer);
            //SelectionCommand = new Command();
            //CancelSelectionCommand = new Command();
            PullRefreshCommand = new Command(Refresh);
        }

        private async void Dialer(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                await App.Current.MainPage.DisplayAlert("Error!", "Null number", "OK");
            }
            catch (FeatureNotSupportedException ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't dail the number", "OK");
            }
        }

        private async void GoToSubPutPage()
        {
            if (SelectedChild.FullName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedChild);
                var route = $"{nameof(EditChildPage)}?Child={jsonClinic}&FamilyId={Family.Id}";
                await Shell.Current.GoToAsync(route);
                SelectedChild = null;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Child", "Select a child", "OK");
            }
        }

        private async void GoToSubPostPage()
        {
            var JsonFamily = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(AddChildPage)}?Family={JsonFamily}";
            await Shell.Current.GoToAsync(route);
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Childs.Clear();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public async void Get()
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
                             FId = item.Key.ToString(),
                             Id = item.Value.Id,
                             FullName = item.Value.FullName,
                             DOB = item.Value.DOB,
                             Gender = item.Value.Gender,
                             OPV0 = item.Value.OPV0,
                             RINo = item.Value.RINo,
                             RegisteredBy = item.Value.RegisteredBy
                         });
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }

        public void GoToPostPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToPutPage()
        {
            var jsonClinic = JsonConvert.SerializeObject(Family);
            var route = $"{nameof(EditFamilyPage)}?Family={jsonClinic}";
            await Shell.Current.GoToAsync(route);
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public void SaveAsPDF()
        {
            throw new NotImplementedException();
        }
    }
}
