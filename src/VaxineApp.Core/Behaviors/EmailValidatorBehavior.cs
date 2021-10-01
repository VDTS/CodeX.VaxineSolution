using System;
using System.Collections.Generic;
using System.Text;
using Utility.Validations;
using Xamarin.Forms;

namespace VaxineApp.Core.Behaviors
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry emailEntry)
        {
            emailEntry.TextChanged += EmailEntryChanged;
            base.OnAttachedTo(emailEntry);
        }

        private void EmailEntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (EmailValidators.IsEmailValid(entry.Text))
            {
                entry.TextColor = Color.Black;
            }
            else
            {
                entry.TextColor = Color.Red;
            }
        }

        protected override void OnDetachingFrom(Entry emailEntry)
        {
            emailEntry.TextChanged += EmailEntryChanged;
            base.OnDetachingFrom(emailEntry);
        }
    }
}
