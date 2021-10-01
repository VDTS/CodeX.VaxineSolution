using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Announcements
{
    public class AnnouncementsViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<AnnouncementsModel>? announcements;
        public ObservableCollection<AnnouncementsModel>? Announcements
        {
            get
            {
                return announcements;
            }
            set
            {
                announcements = value;
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

        // Commands
        public ICommand PullRefreshCommand { private set; get; }
        // ctor
        public AnnouncementsViewModel()
        {
            // Property
            Announcements = new ObservableCollection<AnnouncementsModel>();

            // get
            Get();

            // Commands
            PullRefreshCommand = new Command(Refresh);
        }

        private async void Get()
        {
            var jData = await DataService.Get($"Announcements");

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
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, AnnouncementsModel>>(jData);

                    if(data != null)
                    foreach (KeyValuePair<string, AnnouncementsModel> item in data)
                    {
                        Announcements?.Add(new AnnouncementsModel
                        {
                            Id = item.Value.Id,
                            Content = item.Value.Content,
                            IsActive = item.Value.IsActive,
                            MessageDateTime = item.Value.MessageDateTime,
                            Title = item.Value.Title
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }
        public void Clear()
        {
            Announcements?.Clear();
        }
        public async void Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);

            Clear();
            Get();

            IsBusy = false;
        }
    }
}
