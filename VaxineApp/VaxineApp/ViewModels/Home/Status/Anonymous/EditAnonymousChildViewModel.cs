﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Status.Anonymous;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status.Anonymous
{
    public class EditAnonymousChildViewModel : ViewModelBase
    {
        // Property
        private AnonymousChildModel anonymousChild;
        public AnonymousChildModel AnonymousChild
        {
            get
            {
                return anonymousChild;
            }
            set
            {
                anonymousChild = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public EditAnonymousChildViewModel(AnonymousChildModel anonymousChild)
        {
            // Property
            AnonymousChild = anonymousChild;

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put(object obj)
        {
            var jsonData = JsonConvert.SerializeObject(AnonymousChild);
            var data = await DataService.Put(jsonData, $"AnonymousChild/{Preferences.Get("TeamId", "")}/{AnonymousChild.FId}");
            if (data == "Submit")
            {
                StandardMessagesDisplay.EditDisplaymessage($"{AnonymousChild.FullName}'s Family ");
                var route = $"//{nameof(AnonymousChildPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
        }
    }

}