using System;
using System.Collections.Generic;
using System.Text;
using Utility.Validations;
using Xamarin.Forms;

namespace VaxineApp.Core.Behaviors
{
    public class InternationPhoneNumberValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry emailEntry)
        {
            emailEntry.TextChanged += PhoneNumberEntryChanged;
            base.OnAttachedTo(emailEntry);
        }

        private void PhoneNumberEntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (PhonuNumberWithInternationCodeValidator.IsPhoneNumberValid(entry.Text))
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
            emailEntry.TextChanged += PhoneNumberEntryChanged;
            base.OnDetachingFrom(emailEntry);
        }
    }
}
