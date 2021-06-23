using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.Notifications
{
    public class NotificationsViewModel : ViewModelBase
    {
        INotificationManager notificationManager;
        public ICommand SendNotificationCommand { private set; get; }
        public ICommand ScheduleNotificationCommand { private set; get; }

        public NotificationsViewModel()
        {
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };

            SendNotificationCommand = new Command<EventArgs>(OnSendClick);
            ScheduleNotificationCommand = new Command<EventArgs>(OnScheduleClick);
        }

        void OnSendClick(EventArgs e)
        {
            string title = $"Title";
            string message = $"Body";
            notificationManager.SendNotification(title, message);
        }

        void OnScheduleClick(EventArgs e)
        {
            string title = $"Title Scheduled";
            string message = $"Body Scheduled";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                //stackLayout.Children.Add(msg);
            });
        }
    }
}
