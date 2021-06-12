﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace VaxineApp.Behaviors
{
    public class LoginEmailBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry emailEntry)
        {
            emailEntry.TextChanged += EmailEntryChanged;
            base.OnAttachedTo(emailEntry);
        }

        private void EmailEntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry) sender;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                string emailRegex = @"^([\w\. \-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                bool isMatched = Regex.IsMatch(entry.Text, emailRegex);
                if (isMatched)
                {
                    entry.TextColor = Color.Black;
                }
                else
                {
                    entry.TextColor = Color.Red;
                }
            }
        }

        protected override void OnDetachingFrom(Entry emailEntry)
        {
            emailEntry.TextChanged += EmailEntryChanged;
            base.OnDetachingFrom(emailEntry);
        }
    }
}