﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Newtonsoft.Json;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Validations;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class AddChildViewModel : ViewModelBase
    {
        // Property
        GetFamilyModel Family;

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
            // Property
            Family = family;
            Child = new ChildModel();

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put()
        {
            if (ChildValidator.IsChildUnder5(Child.DOB))
            {
                Child.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));
                Child.Id = Guid.NewGuid();
                Child.DOB = Child.DOB.ToUniversalTime();

                var data = JsonConvert.SerializeObject(Child);

                string a = await DataService.Post(data, $"Child/{Family.Id}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Child.FullName);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(FamilyListPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Older than 5 yrs", "Children older than 5 years not allowed", "OK");
            }
        }
    }
}
