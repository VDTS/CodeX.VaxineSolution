using DataAccessLib.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UtilityLib.Extensions;
using VaxineApp.DataAccessLib;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DataAccessLib.Services
{
    public class Auth
    {
        // Endpoints
        private readonly string ChangeAccountPasswordRequestEndpoint;
        private readonly string ChangeEmailRequestEndpoint;
        private readonly string SignInRequestEndpoint;
        private readonly string VerifyEmailRequestEndpoint;
        private readonly string SendPasswordResetCodeRequestEndpoint;
        private readonly string VerifyPasswordResetCodeRequestEndpoint;
        private readonly string ConfirmPasswordResetRequestEndpoint;
        private readonly string RefreshIdTokenEndpoint;

        // RequestUri
        private readonly string ChangeAccountPasswordRequestUri;
        private readonly string ChangeEmailRequestUri;
        private readonly string SignInRequestUri;
        private readonly string VerifyEmailRequestUri;
        private readonly string SendPasswordResetCodeRequestUri;
        private readonly string VerifyPasswordResetCodeRequestUri;
        private readonly string ConfirmPasswordResetRequestUri;
        private readonly string RefreshIdTokenUri;

        // ctor
        public Auth()
        {
            // Endpoints
            ChangeAccountPasswordRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:update?key=";
            ChangeEmailRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:update?key=";
            SignInRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
            VerifyEmailRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
            SendPasswordResetCodeRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
            VerifyPasswordResetCodeRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=";
            ConfirmPasswordResetRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=";
            RefreshIdTokenEndpoint = @"https://securetoken.googleapis.com/v1/token?key=";

            // RequestUri
            ChangeAccountPasswordRequestUri = string.Concat(ChangeAccountPasswordRequestEndpoint, Constants.FirebaseApiKey);
            ChangeEmailRequestUri = string.Concat(ChangeEmailRequestEndpoint, Constants.FirebaseApiKey);
            SignInRequestUri = string.Concat(SignInRequestEndpoint, Constants.FirebaseApiKey);
            VerifyEmailRequestUri = string.Concat(VerifyEmailRequestEndpoint, Constants.FirebaseApiKey);
            SendPasswordResetCodeRequestUri = string.Concat(SendPasswordResetCodeRequestEndpoint, Constants.FirebaseApiKey);
            VerifyPasswordResetCodeRequestUri = string.Concat(VerifyPasswordResetCodeRequestEndpoint, Constants.FirebaseApiKey);
            ConfirmPasswordResetRequestUri = string.Concat(ConfirmPasswordResetRequestEndpoint, Constants.FirebaseApiKey);
            RefreshIdTokenUri = string.Concat(RefreshIdTokenEndpoint, Constants.FirebaseApiKey);
        }


        // Methdos
        public async Task<string> ChangeAccountPassword(string token, string password)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), ChangeAccountPasswordRequestUri);
            var r = new ChangeAccountPasswordModel() { IdToken = token, Password = password, ReturnSecureToken = true };
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
        public async Task<string> ChangeEmail(string email, string token)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), ChangeEmailRequestUri);
            var r = new ChangeEmailModel() { IdToken = token, Email = email, ReturnSecureToken = true };
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
        public async Task<string> SignIn(string email, string password)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return "ConnectionError";
            }
            else
            {
                try
                {
                    var identityModel = new AccountModels() { Email = email, Password = password, ReturnSecureToken = true };
                    using var httpClient = new HttpClient();
                    using var request = new HttpRequestMessage(new HttpMethod("POST"), SignInRequestUri);
                    request.Content = new StringContent(JsonConvert.SerializeObject(identityModel));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    var respon = (int)response.StatusCode;

                    if ((int)response.StatusCode == 400)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        JObject jo = JObject.Parse(responseBody);
                        return $"Error: {(string)jo.SelectToken("error.message")}";
                    }
                    else if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return $"Error + {response.Content.ReadAsStringAsync().Result}";
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    return "ErrorTracked";
                }
            }
        }
        public async Task<string> VerifyEmail(string Token)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), VerifyEmailRequestUri);
            VerifyEmailModel verifyEmailModel = new VerifyEmailModel() { IdToken = Token, RequestType = "VERIFY_EMAIL" };
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
        public async Task<string> SendPasswordResetcode(string email)
        {
            using var httpClient = new HttpClient();
            SendPasswordResetCodeModel resetCodeRoot = new SendPasswordResetCodeModel() { Email = email, RequestType = "PASSWORD_RESET" };
            var resetCodeRootJson = JsonConvert.SerializeObject(resetCodeRoot);
            using var request = new HttpRequestMessage(new HttpMethod("POST"), SendPasswordResetCodeRequestUri);
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
        public async Task<string> VerifyPasswordResetCode(string resetCode)
        {
            VerifyPasswordResetCodeModel resetCodeRoot = new VerifyPasswordResetCodeModel() { OobCode = resetCode };
            var JsonData = JsonConvert.SerializeObject(resetCodeRoot);
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), VerifyPasswordResetCodeRequestUri);
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
        public async Task<string> ConfirmPasswordReset(string resetCode, string NewPassword)
        {
            ConfirmPasswordResetModel confirmPasswordRoot = new ConfirmPasswordResetModel() { oobCode = resetCode, NewPassword = NewPassword };
            var jsonData = JsonConvert.SerializeObject(confirmPasswordRoot);
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), ConfirmPasswordResetRequestUri);
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

        // Token and refresh Token
        private async void RefreshToken()
        {
            var IdToken = await SecureStorage.GetAsync("IdToken");
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), RefreshIdTokenUri))
                {
                    var refreshToken = await SecureStorage.GetAsync("RefreshToken");

                    RefreshTokenModel rtm = new RefreshTokenModel() { GrantType = "refresh_token", RefreshToken = refreshToken };

                    request.Content = new StringContent(JsonConvert.SerializeObject(rtm));
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var jResponse = await response.Content.ReadAsStringAsync();
                        var jData = JsonConvert.DeserializeObject<JObject>(jResponse);
                        Preferences.Set("lastRefreshAt", DateTime.UtcNow);
                        await SecureStorage.SetAsync("IdToken", jData.GetValue("access_token").ToString());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Can't refresh token, log out and login again", "OK");
                    }
                }
            }
        }

        public async Task<string> IdToken()
        {
            var lastRefreshAt = Preferences.Get("lastRefreshAt", DateTime.UtcNow);
            var diff = lastRefreshAt.TimeDifferenceBySecs();
            if (diff <= 3580)
            {
                return await SecureStorage.GetAsync("IdToken");
            }
            else
            {
                RefreshToken();
                return await SecureStorage.GetAsync("IdToken");
            }
        }
    }
}
