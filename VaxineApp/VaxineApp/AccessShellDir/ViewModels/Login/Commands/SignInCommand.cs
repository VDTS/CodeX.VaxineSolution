using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace VaxineApp.AccessShellDir.ViewModels.Login.Commands
{
    public class SignInCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly LoginViewModel LoginViewModel;

        public SignInCommand(LoginViewModel loginViewModel)
        {
            this.LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var param = parameter as String;
            if (!string.IsNullOrWhiteSpace(LoginViewModel.InputUserEmail))
            {
                if (!string.IsNullOrEmpty(param))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            var param = parameter as String;
            LoginViewModel.SignIn(LoginViewModel.InputUserEmail, param);
        }
    }
}
