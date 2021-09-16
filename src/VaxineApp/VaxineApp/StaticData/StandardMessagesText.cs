namespace VaxineApp.StaticData
{
    public static class StandardMessagesText
    {
        // Actions
        public static string ItemDeletedText { get; set; } = "Item has been removed";
        public static string GithubIssueSubmit { get; set; } = "Thank you! Your feedback has been submitted";
        public static string AddBody(string input)
        {
            return $"{input} has been submitted";
        }
        public static string DeleteBody(string input)
        {
            return $"Do you want to remove {input}?";
        }
        public static string EditBody(string input)
        {
            return $"{input} has been updated";
        }

        // Errors
        public static string ErrorTracked { get; set; } = "Error occured and Tracked by App Center";
        public static string Error { get; set; } = "Error Occured";
        public static string NoConnectionToastMessage { get; set; } = "No Internet Connection";
        public static string CanceledBody { get; set; } = "Try again later";
        public static string NoDataBody { get; set; } = "No data available at the moment, add data or retry later";
        public static string InvalidDataBody { get; set; } = "Add valid data to required Fields";
        public static string NoItemSelectedBody { get; set; } = "No item selected";

        // Validators
        public static string FeatureUnderConstructionBody = "Feature not available in this release";
        public static string FamilyDuplicateValidatorBody(int input)
        {
            return $"{input} family already exists";
        }
        public static string ChildRecursiveDeletionNotAllowedBody(string input1, int input2)
        {
            return $"{input1} has {input2} vaccines on timeline, delete them before to proceed.";
        }
        public static string FamilyRecursiveDeletionNotAllowedBody(string input1, int input2)
        {
            return $"{input1}'s Family has {input2} childs, delete them before to proceed.";
        }
        public static string PeriodNotAvailableBody { get; set; } = "Vaccine period not started, for more info follow announcements";
        public static string ValidationRulesViolationTitle(string input)
        {
            return $"Invalid {input}";
        }
        public static string ValidationRulesViolationBody(string body)
        {
            return body;
        }

        // Account Management
        public static string EmailValidatorTitle { get; set; } = "Email error";
        public static string EmailValidatorBody { get; set; } = "Email is baddly formatted";
        public static string EmailMatchValidatorBody { get; set; } = "Emails are not the same";
    }
}
