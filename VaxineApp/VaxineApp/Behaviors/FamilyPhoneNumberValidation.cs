using UtilityLib.Validations;
using Xamarin.Forms;

namespace VaxineApp.Behaviors
{
    public class FamilyPhoneNumberValidation : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry emailEntry)
        {
            emailEntry.TextChanged += PhoneNumberEntryChanged;
            base.OnAttachedTo(emailEntry);
        }

        private void PhoneNumberEntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (PhoneNumberValidator.IsPhoneNumberValid(entry.Text))
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
