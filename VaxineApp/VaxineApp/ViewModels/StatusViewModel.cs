using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {

        public ICommand RegistrationPageCommand { private set; get; }
        public async void Add()
        {
            var route = $"{nameof(RegistrationPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public StatusViewModel()
        {
            RegistrationPageCommand = new Command(Add);
        }
    }
}
