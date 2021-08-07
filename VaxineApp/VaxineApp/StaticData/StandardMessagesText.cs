namespace VaxineApp.StaticData
{
    public static class StandardMessagesText
    {
        // Github Issue Submited
        public static string GithubIssueSubmit { get; set; } = "Thank you! Your feedback has been submitted, and we will soon take actions";
        // Error
        public static string ErrorTracked { get; set; } = "Error occured and Tracked by App Center";
        public static string Error { get; set; } = "Error Occured";
        // No internet Connection
        public static string NoConnectionToastMessage { get; set; } = "No Connection, check data or wifi!";
        // Add
        public static string AddTitle { get; set; } = "Added";
        public static string AddBody(string input)
        {
            return $"{input} has been submitted";
        }

        // Delete
        public static string DeleteTitle { get; set; } = null;
        public static string DeleteBody(string input)
        {
            return $"Do you want to remove {input}";
        }

        public static string DeletedMessageBody { get; set; } = "Item Deleted";

        // Edit
        public static string EditTitle { get; set; } = "Updated";
        public static string EditBody(string input)
        {
            return $"{input} has been updated";
        }

        // Canceled
        public static string CanceledTitle { get; set; } = "Operation Canceled";
        public static string CanceledBody { get; set; } = "Try again later";

        // No data
        public static string NoDataTitle { get; set; } = "No data available";
        public static string NoDataBody { get; set; } = "No data available at the moment, come later or add data";

        // Invalid Data
        public static string InvalidDataTitle { get; set; } = "Invalid Data";
        public static string InvalidDataBody { get; set; } = "Add valid data to required Fields";

        // Features under construction
        public static string FeatureUnderConstructionTitle = "Feature not found";
        public static string FeatureUnderConstructionBody = "This features is under implementation and will be available in future releases. Follow App updates page for more info...";

        // No item Selected
        public static string NoItemSelectedTitle { get; set; } = null;
        public static string NoItemSelectedBody { get; set; } = "Select an item, and come back to operate";

        // Validators
        public static string ChildAgeValidatorTitle { get; set; } = "Greater than 5 yr";
        public static string ChildAgeValidatorBody(string input)
        {
            return $"{input} is greater than 5 years, and can't be added to vaccine process. See who are eligible for vaccine.";
        }

        public static string FamilyDuplicateValidatorTitle { get; set; } = "Already Exists";
        public static string FamilyDuplicateValidatorBody(int input)
        {
            return $"{input} already exists, you can add new family by deleting old one or you can edit the old one.";
        }

        public static string ChildRecursiveDeletionNotAllowedTitle { get; set; } = "Canceled";
        public static string ChildRecursiveDeletionNotAllowedBody(string input1, int input2)
        {
            return $"{input1} has {input2} vaccines on timeline, delete them before to proceed.";
        }

        public static string FamilyRecursiveDeletionNotAllowedTitle { get; set; } = "Canceled";
        public static string FamilyRecursiveDeletionNotAllowedBody(string input1, int input2)
        {
            return $"{input1}'s Family has {input2} childs, delete them before to proceed.";
        }

        public static string InvalidPhoneNumberTitle { get; set; } = "Invalid Phone number";
        public static string InvalidPhoneNumberBody { get; set; } = "Number must start with +93, 0093 or 0.";

        public static string PeriodNotAvailableTitle { get; set; } = "Period not available";
        public static string PeriodNotAvailableBody { get; set; } = "You can't add vaccine now, wait for starting the period, and follow REMT message centre.";

        public static string ValidationRulesViolationTitle(string input)
        {
            return $"Invalid {input}";
        }
        public static string ValidationRulesViolationBody(string body)
        {
            return body;
        }
        // Account Management
        public static string EmailChangedTitle { get; set; } = "Email Updated";
        public static string EmailChangedBody(string input)
        {
            return $"Email changed to {input}.";
        }

        public static string EmailVerificationSendTitle { get; set; } = "Email Verification Send";
        public static string EmailVerificationSendBody(string input)
        {
            return $"An verification email to {input} send, confirm it.";
        }

        public static string PasswordValidatorTitle { get; set; } = "Password Error";
        public static string PasswordValidatorBody { get; set; } = "Password violates password rules";

        public static string EmailValidatorTitle { get; set; } = "Email error";
        public static string EmailValidatorBody { get; set; } = "Email is baddly formatted";

        public static string EmailMatchValidatorTitle { get; set; } = null;
        public static string EmailMatchValidatorBody { get; set; } = "Emails are not the same";

        public static string PasswordChangedTitle { get; set; } = "Succeed";
        public static string PasswordChangedBody { get; set; } = "Password changed";

    }
}
