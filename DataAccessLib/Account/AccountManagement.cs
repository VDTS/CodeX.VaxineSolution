﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.DataAccessLib;
using Xamarin.Essentials;

namespace DataAccessLib.Account
{
    public class AccountManagement : IAccountManagement
    {
        // Endpoints
        private readonly string ChangeAccountPasswordRequestEndpoint;
        private readonly string ChangeEmailRequestEndpoint;
        private readonly string SignInRequestEndpoint;
        private readonly string VerifyEmailRequestEndpoint;
        private readonly string SendPasswordResetCodeRequestEndpoint;
        private readonly string VerifyPasswordResetCodeRequestEndpoint;
        private readonly string ConfirmPasswordResetRequestEndpoint;

        // RequestUri
        private readonly string ChangeAccountPasswordRequestUri;
        private readonly string ChangeEmailRequestUri;
        private readonly string SignInRequestUri;
        private readonly string VerifyEmailRequestUri;
        private readonly string SendPasswordResetCodeRequestUri;
        private readonly string VerifyPasswordResetCodeRequestUri;
        private readonly string ConfirmPasswordResetRequestUri;

        // ctor
        public AccountManagement()
        {
            // Endpoints
            ChangeAccountPasswordRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:update?key=";
            ChangeEmailRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:update?key=";
            SignInRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
            VerifyEmailRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
            SendPasswordResetCodeRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
            VerifyPasswordResetCodeRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=";
            ConfirmPasswordResetRequestEndpoint = @"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=";


            // RequestUri
            ChangeAccountPasswordRequestUri = string.Concat(ChangeAccountPasswordRequestEndpoint, Constants.FirebaseApiKey);
            ChangeEmailRequestUri = string.Concat(ChangeEmailRequestEndpoint, Constants.FirebaseApiKey);
            SignInRequestUri = string.Concat(SignInRequestEndpoint, Constants.FirebaseApiKey);
            VerifyEmailRequestUri = string.Concat(VerifyEmailRequestEndpoint, Constants.FirebaseApiKey);
            SendPasswordResetCodeRequestUri = string.Concat(SendPasswordResetCodeRequestEndpoint, Constants.FirebaseApiKey);
            VerifyPasswordResetCodeRequestUri = string.Concat(VerifyPasswordResetCodeRequestEndpoint, Constants.FirebaseApiKey);
            ConfirmPasswordResetRequestUri = string.Concat(ConfirmPasswordResetRequestEndpoint, Constants.FirebaseApiKey);
        }


        // Methdos
        public async Task<string> ChangeAccountPassword(string token, string password)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), ChangeAccountPasswordRequestUri))
                {
                    var r = new ChangeAccountPasswordModel() { idToken = token, password = password, returnSecureToken = true };
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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), ChangeEmailRequestUri))
                {
                    var r = new ChangeEmailModel() { idToken = token, email = email, returnSecureToken = true };
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
            var identityModel = new AccountModels() { email = email, password = password, returnSecureToken = true };
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), SignInRequestUri))
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(identityModel));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var a = response.Content.ReadAsStringAsync().Result;
                        var s1 = JsonConvert.DeserializeObject<JObject>(a);
                        Preferences.Set("UserLocalId", s1.GetValue("localId").ToString());
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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), VerifyEmailRequestUri))
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
                SendPasswordResetCodeModel resetCodeRoot = new SendPasswordResetCodeModel() { email = email, requestType = "PASSWORD_RESET" };
                var resetCodeRootJson = JsonConvert.SerializeObject(resetCodeRoot);
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), SendPasswordResetCodeRequestUri))
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
            VerifyPasswordResetCodeModel resetCodeRoot = new VerifyPasswordResetCodeModel() { oobCode = resetCode };
            var JsonData = JsonConvert.SerializeObject(resetCodeRoot);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), VerifyPasswordResetCodeRequestUri))
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
            ConfirmPasswordResetModel confirmPasswordRoot = new ConfirmPasswordResetModel() { oobCode = resetCode, newPassword = NewPassword };
            var jsonData = JsonConvert.SerializeObject(confirmPasswordRoot);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), ConfirmPasswordResetRequestUri))
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
