using System.Collections.ObjectModel;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.RecycleBin
{
    public class RecycleBinViewModel : ViewModelBase
    {
        // Property
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


        // Debug Code
        public ObservableCollection<Items>? items { get; set; }

        // Command
        public ICommand EmptyRecycleBinCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand RestoreCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public RecycleBinViewModel()
        {
            // Command
            EmptyRecycleBinCommand = new Command(EmptyRecycleBin);
            RestoreCommand = new Command(Restore);
            DeleteCommand = new Command(Delete);

            items = new ObservableCollection<Items>();
            #region MyRegion
            items.Add(new Items
            {
                Name = "Item 01",
                Details = "There are details for the deleted items"
            });
            items.Add(new Items
            {
                Name = "Item 01",
                Details = "There are details for the deleted items"
            });
            items.Add(new Items
            {
                Name = "Item 01",
                Details = "There are details for the deleted items"
            });
            items.Add(new Items
            {
                Name = "Item 01",
                Details = "There are details for the deleted items"
            });
            items.Add(new Items
            {
                Name = "Item 01",
                Details = "There are details for the deleted items"
            });
            #endregion
        }

        private void Delete(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private void Restore(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private void EmptyRecycleBin(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }
    }

    #region MyRegion
    public class Items
    {
        public string? Name { get; set; }
        public string? Details { get; set; }
    }
    #endregion
}
