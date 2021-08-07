﻿using System.Threading.Tasks;
using VaxineApp.AndroidNativeApi;
using Xamarin.Forms;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesDisplay
    {
        // Github Issue Submited
        public static void IssueSubmitToast()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.GithubIssueSubmit);
        }
        // ✔️ Error Tracked
        public static void ErrorTracked()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ErrorTracked);
        }
        // ✔️ Error
        public static void Error()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.Error);
        }
        // ✔️ No Connection 
        public static void NoConnectionToast()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoConnectionToastMessage);
        }
        public static void AddDisplayMessage(string input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.AddBody(input));
        }
        public static void EditDisplaymessage(string input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.EditBody(input));
        }
        public static void NoDataDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoDataBody);
        }
        public static void CanceledDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.CanceledBody);
        }
        public static void InvalidDataDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.InvalidDataBody);
        }
        public static void FeatureUnderConstructionTitleDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.FeatureUnderConstructionBody);
        }
        public async static Task<bool> DeleteDisplayMessage(string input)
        {
            return await App.Current.MainPage.DisplayAlert(StandardMessagesText.DeleteTitle, StandardMessagesText.DeleteBody(input), "Yes", "No");
        }

        public static void DeletedDisplayMesage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.DeletedMessageBody);
        }
        public static void NoItemSelectedDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoItemSelectedBody);
        }


        // Validators
        public static void ChildAgeValidator(string input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ChildAgeValidatorBody(input));
        }

        public static void FamilyDuplicateValidator(int input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.FamilyDuplicateValidatorBody(input));
        }
        public static void ChildRecursiveDeletionNotAllowed(string child, int vaccineCount)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ChildRecursiveDeletionNotAllowedBody(child, vaccineCount));
        }
        public static void FamilyRecursiveDeletionNotAllowed(string family, int childCount)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ChildRecursiveDeletionNotAllowedBody(family, childCount));
        }

        public static void InvalidPhoneNumber()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.InvalidPhoneNumberBody);
        }
        public static void PeriodNotAvailable()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.PeriodNotAvailableBody);
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
        public async static void PasswordValidator()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.PasswordValidatorTitle, StandardMessagesText.PasswordValidatorBody, "OK");
        }

        public async static void EmailValidator()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EmailValidatorTitle, StandardMessagesText.EmailValidatorBody, "OK");
        }
        public async static void EmailMatchValidator()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EmailMatchValidatorTitle, StandardMessagesText.EmailMatchValidatorBody, "OK");
        }
        public async static void PasswordChagned()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.PasswordChangedTitle, StandardMessagesText.PasswordChangedBody, "OK");
        }
        public async static void ValidationRulesViolation(string title, string body)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.ValidationRulesViolationTitle(title), StandardMessagesText.ValidationRulesViolationBody(body), "OK");
        }
    }
}
