﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Services;
using VaxineApp.Views.Home;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        DbContext firebaseHelper = new DbContext();
        private List<ChildrenModel> _childs;
        public List<ChildrenModel> Childs {
            get { return _childs; }
            set
            {
                _childs = value;
                RaisedPropertyChanged(nameof(Childs));
            }

        }
        public ICommand RegistrationPageCommand { private set; get; }
        public async void Add()
        {
            var route = $"{nameof(RegistrationPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GetChild()
        {
            Childs = await firebaseHelper.GetChilds();
        }
        public StatusViewModel()
        {
            GetChild();
            RegistrationPageCommand = new Command(Add);
        }
    }
}
