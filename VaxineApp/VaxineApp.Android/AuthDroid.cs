using System;
using System.Threading.Tasks;
using Firebase.Auth;
using VaxineApp;
using VaxineApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace VaxineApp.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = user.User.GetIdToken(false);
                return token.ToString();
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        public bool SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                var signUpTask = FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);

                return signUpTask.Result != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}