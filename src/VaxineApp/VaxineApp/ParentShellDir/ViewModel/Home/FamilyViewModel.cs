using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ParentShellDir.Views.Home;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.ParentShellDir.ViewModel.Home
{
    public class FamilyViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<ChildModel>? childs;
        public ObservableCollection<ChildModel>? Childs
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

        private ChildModel? selectedChild;
        public ChildModel? SelectedChild
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

        private FamilyModel? family;
        public FamilyModel? Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

        // ctor
        public FamilyViewModel()
        {
            // Property
            SelectedChild = new ChildModel();
            Childs = new ObservableCollection<ChildModel>();
            Family = new FamilyModel();

            // Get
            GetFamily();
            Get();

            // Command
            PullRefreshCommand = new Command(Refresh);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        private async void GetFamily()
        {
            var jData = await DataService.Get($"Family/c0cda6a9-759a-4e87-b8cb-49af170bd24e/-MbXlzV80PxnP0zTdwLa");

            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                Family = JsonConvert.DeserializeObject<FamilyModel>(jData);
            }
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Childs?.Clear();
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Child/{Family?.Id}");

            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(jData);

                if(data != null)
                foreach (KeyValuePair<string, ChildModel> item in data)
                {
                    Childs?.Add(
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
        }

        public async void GoToDetailsPage()
        {
            if (SelectedChild == null)
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedChild);
                var route = $"{nameof(ChildVaccinePage)}?Child={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedChild = null;
            }
        }

        public void GoToMapPage()
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

    }
}
