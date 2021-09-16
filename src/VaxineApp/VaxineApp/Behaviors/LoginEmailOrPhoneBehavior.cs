using Utility.Validations;
using Xamarin.Forms;

namespace VaxineApp.Behaviors
{
    public class LoginEmailOrPhoneBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry emailEntry)
        {
            emailEntry.TextChanged += EmailEntryChanged;
            base.OnAttachedTo(emailEntry);
        }

        private void EmailEntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (EmailValidators.IsEmailValid(entry.Text) || PhoneNumberValidator.IsPhoneNumberValid(entry.Text))
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
