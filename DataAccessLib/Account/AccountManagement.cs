using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.DataAccessLib;

namespace DataAccessLib.Account
{
    public class AccountManagement : IAccountManagement
    {
        // Property
        readonly string ApiKey = Constants.FirebaseApiKey;
        readonly string BaseUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
        private string Url;

        // ctor
        public AccountManagement()
        {
            Url = string.Concat(BaseUrl, ApiKey);
        }


        // Methdos
        public async Task<string> ChangeAccountPassword(string token, string password)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:update?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    var r = new Root() { idToken = token, password = password, returnSecureToken = true };
                    string s1 = JsonConvert.SerializeObject(r);
                    request.Content = new StringContent(s1);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> ChangeEmail(string email, string token)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:update?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    var r = new EmailRoot() { idToken = token, email = email, returnSecureToken = true };
                    string s1 = JsonConvert.SerializeObject(r);
                    request.Content = new StringContent(s1);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> SignIn(string email, string password)
        {
            var identityModel = new SignInModel() { email = email, password = password, returnSecureToken = true };
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), Url))
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(identityModel));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var a = response.Content.ReadAsStringAsync().Result;
                        var s1 = JsonConvert.DeserializeObject<JObject>(a);
                        return s1.GetValue("idToken").ToString();
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> VerifyEmail(string Token)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    VerifyEmailModel verifyEmailModel = new VerifyEmailModel() { idToken = Token, requestType = "VERIFY_EMAIL" };
                    string s1 = JsonConvert.SerializeObject(verifyEmailModel);
                    request.Content = new StringContent(s1);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> SendPasswordResetcode(string email)
        {
            using (var httpClient = new HttpClient())
            {
                ResetCodeRoot resetCodeRoot = new ResetCodeRoot() { email = email, requestType = "PASSWORD_RESET" };
                var resetCodeRootJson = JsonConvert.SerializeObject(resetCodeRoot);
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    request.Content = new StringContent(resetCodeRootJson);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> VerifyPasswordResetCode(string resetCode)
        {
            ResetCodeReturnRoot resetCodeRoot = new ResetCodeReturnRoot() { oobCode = resetCode };
            var JsonData = JsonConvert.SerializeObject(resetCodeRoot);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    request.Content = new StringContent(JsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> ConfirmPasswordReset(string resetCode, string NewPassword)
        {
            ConfirmPasswordRoot confirmPasswordRoot = new ConfirmPasswordRoot() { oobCode = resetCode, newPassword = NewPassword };
            var jsonData = JsonConvert.SerializeObject(confirmPasswordRoot);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }

    }
}
