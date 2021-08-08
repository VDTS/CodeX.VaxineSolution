﻿using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class AddChildViewModel : ViewModelBase
    {
        // Validator Class
        ChildValidator ChildValidator { get; set; }

        // Property
        public GetFamilyModel Family { get; set; }

        private ChildModel child;
        public ChildModel Child
        {
            get
            {
                return child;
            }
            set
            {
                child = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }

        // ctor
        public AddChildViewModel(GetFamilyModel family)
        {
            // Objects
            ChildValidator = new ChildValidator();
            Family = family;

            // Property
            Child = new ChildModel();

            // Command
            PutCommand = new Command(Post);
        }

        private async void Post()
        {
            Child.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));
            Child.Id = Guid.NewGuid();

            var time = DateTime.Now;
            DateTime dateTime = new DateTime(Child.DOB.Year, Child.DOB.Month, Child.DOB.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

            Child.DOB = dateTime;

            var result = ChildValidator.Validate(Child);
            if (result.IsValid)
            {
                var jData = JsonConvert.SerializeObject(Child);

                string postResponse = await DataService.Post(jData, $"Child/{Family.Id}");
                if (postResponse == "ConnectionError")
                {
                    StandardMessagesDisplay.NoConnectionToast();
                }
                else if (postResponse == "Error")
                {
                    StandardMessagesDisplay.Error();
                }
                else if (postResponse == "ErrorTracked")
                {
                    StandardMessagesDisplay.ErrorTracked();
                }
                else
                {
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalChilds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalChilds");
                    StandardMessagesDisplay.EditDisplaymessage(Child.FullName);

                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
