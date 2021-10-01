using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.AdminShell.Views.Announcements;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Announcements
{
    public class AnnouncementsViewModel : ViewModelBase
    {
        // Property
        private AnnouncementsModel? selectAnnouncements;
        public AnnouncementsModel? SelectAnnouncements
        {
            get
            {
                return selectAnnouncements;
            }
            set
            {
                selectAnnouncements = value;
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

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

        // ctor
        public AnnouncementsViewModel()
        {
            // Property
            Announcements = new ObservableCollection<AnnouncementsModel>();
            SelectAnnouncements = new AnnouncementsModel();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public async void GoToDetailsPage()
        {
            if (SelectAnnouncements.AreEmpty())
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectAnnouncements);
                var route = $"{nameof(AnnouncementsDetailsPage)}?Announcement={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectAnnouncements = null;
            }
        }


        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
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

                    if (data != null)
                        foreach (KeyValuePair<string, AnnouncementsModel> item in data)
                        {
                            Announcements?.Add(
                                new AnnouncementsModel
                                {
                                    Id = item.Value.Id,
                                    Content = item.Value.Content,
                                    IsActive = item.Value.IsActive,
                                    MessageDateTime = item.Value.MessageDateTime,
                                    Title = item.Value.Title,
                                    FId = item.Key
                                }
                                );
                        }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddAnnouncementPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Announcements?.Clear();
        }
    }
}
