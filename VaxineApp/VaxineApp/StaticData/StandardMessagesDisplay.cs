using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesDisplay
    {
        public async static void AddDisplayMessage(string input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.AddTitle, StandardMessagesText.AddBody(input), "OK");
        }
        public async static void EditDisplaymessage(string input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EditTitle, StandardMessagesText.EditBody(input), "OK");
        }
        public async static void NoDataDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.NoDataTitle, StandardMessagesText.NoDataBody, "OK");
        }
        public async static void CanceledDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.CanceledTitle, StandardMessagesText.CanceledBody, "OK");
        }
        public async static void InvalidDataDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.InvalidDataTitle, StandardMessagesText.InvalidDataBody, "OK");
        }
        public async static void FeatureUnderConstructionTitleDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.FeatureUnderConstructionTitle, StandardMessagesText.FeatureUnderConstructionBody, "OK");
        }
        public async static Task<bool> DeleteDisplayMessage(string input)
        {
            return await App.Current.MainPage.DisplayAlert(StandardMessagesText.DeleteTitle, StandardMessagesText.DeleteBody(input), "Yes", "No");
        }
        public async static void NoItemSelectedDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.NoItemSelectedTitle, StandardMessagesText.NoItemSelectedBody, "OK");
        }


        // Validators
        public async static void ChildAgeValidator(string input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.ChildAgeValidatorTitle, StandardMessagesText.ChildAgeValidatorBody(input), "OK");
        }

        public async static void FamilyDuplicateValidator(int input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.FamilyDuplicateValidatorTitle, StandardMessagesText.FamilyDuplicateValidatorBody(input), "OK");
        }
        public async static void ChildRecursiveDeletionNotAllowed(string child, int vaccineCount)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.ChildRecursiveDeletionNotAllowedTitle, StandardMessagesText.ChildRecursiveDeletionNotAllowedBody(child, vaccineCount), "OK");
        }
        public async static void FamilyRecursiveDeletionNotAllowed(string family, int childCount)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.ChildRecursiveDeletionNotAllowedTitle, StandardMessagesText.ChildRecursiveDeletionNotAllowedBody(family, childCount), "OK");
        }

        // Account Management
        public async static void EmailChanged(string email)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EmailChangedTitle, StandardMessagesText.EmailChangedBody(email), "OK");
        }

        public async static void EmailVerificationSend(string email)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EmailVerificationSendTitle, StandardMessagesText.EmailVerificationSendBody(email), "OK");
        }
    }
}
