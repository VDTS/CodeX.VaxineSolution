﻿using System;
using VaxineApp.Font;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AccessShellDir.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ShowHidePassword(object sender, EventArgs e)
        {
            if (PasswordField.IsPassword == true)
            {
                PasswordField.IsPassword = false;
                ShowHideButton.Text = MaterialDesignIcons.Eye;
            }
            else
            {
                PasswordField.IsPassword = true;
                ShowHideButton.Text = MaterialDesignIcons.EyeOff;
            }
        }
    }
}