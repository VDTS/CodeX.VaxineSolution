﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Announcements
{
    public class AnnouncementsViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<AnnouncementsModel> announcements;
        public ObservableCollection<AnnouncementsModel> Announcements
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
            var Date = DateTime.UtcNow;
            var Month = Date.Month > 9 ? Date.Month.ToString() : $"0{Date.Month}";
            var Day = Date.Day > 9 ? Date.Day.ToString() : $"0{Date.Day}";
            var data = await DataService.Get($"Announcements/{Date.Year}-{Month}-{Day}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, AnnouncementsModel>>(data);
                foreach (KeyValuePair<string, AnnouncementsModel> item in clinic)
                {
                    Announcements.Add(new AnnouncementsModel
                    {
                        FIdDate = item.Key.ToString(),
                        ActiveTill = item.Value.ActiveTill,
                        Content = item.Value.Content,
                        Time = item.Value.Time,
                        Title = item.Value.Title
                    });
                }
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }
        public void Clear()
        {
            Announcements.Clear();
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