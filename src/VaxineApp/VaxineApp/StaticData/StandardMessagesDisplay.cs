using System.Threading.Tasks;
using VaxineApp.AndroidNativeApi;
using Xamarin.Forms;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesDisplay
    {
        // Actions
        public static void CommonToastMessage(string message)
        {
            DependencyService.Get<IToast>()?.MakeToast(message);
        }
        public static void ItemDeletedToast()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ItemDeletedText);
        }
        public static void AddDisplayMessage(string? input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.AddBody(input));
        }
        public static void IssueSubmitToast()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.GithubIssueSubmit);
        }
        public static void EditDisplaymessage(string? input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.EditBody(input));
        }
        public async static Task<bool> DeleteDisplayMessage(string? input)
        {
            return await App.Current.MainPage.DisplayAlert(null, StandardMessagesText.DeleteBody(input), "Yes", "No");
        }

        // Errors
        public static void InputToast(string message)
        {
            DependencyService.Get<IToast>()?.MakeToast(message);
        }
        public static void ErrorTracked()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ErrorTracked);
        }
        public static void Error()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.Error);
        }
        public static void NoConnectionToast()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoConnectionToastMessage);
        }
        public static void NoDataDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoDataBody);
        }
        public static void CanceledDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.CanceledBody);
        }
        public static void FeatureUnderConstructionTitleDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.FeatureUnderConstructionBody);
        }
        public static void NoItemSelectedDisplayMessage()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.NoItemSelectedBody);
        }

        // Validators
        public static void FamilyDuplicateValidator(int input)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.FamilyDuplicateValidatorBody(input));
        }
        public static void ChildRecursiveDeletionNotAllowed(string? child, int vaccineCount)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.ChildRecursiveDeletionNotAllowedBody(child, vaccineCount));
        }
        public static void FamilyRecursiveDeletionNotAllowed(string? family, int childCount)
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.FamilyRecursiveDeletionNotAllowedBody(family, childCount));
        }
        public static void PeriodNotAvailable()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.PeriodNotAvailableBody);
        }
        public async static void ValidationRulesViolation(string? title, string? body)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.ValidationRulesViolationTitle(title), StandardMessagesText.ValidationRulesViolationBody(body), "OK");
        }

        // Account Management
        public static void EmailChanged(string email)
        {
            DependencyService.Get<IToast>()?.MakeToast("Email changed");
        }
        public static void EmailVerificationSend(string email)
        {
            DependencyService.Get<IToast>()?.MakeToast($"Email verification sent to : {email}");
        }
        public async static void EmailValidator()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EmailValidatorTitle, StandardMessagesText.EmailValidatorBody, "OK");
        }
        public async static void EmailMatchValidator()
        {
            await App.Current.MainPage.DisplayAlert(null, StandardMessagesText.EmailMatchValidatorBody, "OK");
        }
        public static void PasswordChanged()
        {
            DependencyService.Get<IToast>()?.MakeToast("Password Changed");
        }

        // Admin User Management
        public static void UserAdded()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.UserAddedMessage);
        }
        public static void UserRemoved()
        {
            DependencyService.Get<IToast>()?.MakeToast(StandardMessagesText.UserRemovedMessage);
        }
    }
}
