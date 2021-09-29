namespace VaxineApp.Models.AccountModels
{
    public class FirebaseUserModel
    {
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string UId { get; set; }
        public bool EmailVerified {  get; set; }
    }
}
