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
        public async Task<string> ChangeAccountPassword(string password)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:update?key=AIzaSyCmnKWt1n88-OrBDUX9hdFTUujHUhowjp8"))
                {
                    string s1 = string.Concat("{\"idToken\":\"","On2NAlvvSBPSCXtQXgr6I1jwMRL2\"",",\"password\":\"", password, ",\"returnSecureToken\":true}");
                    request.Content = new StringContent(s1);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Password Changed!";
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
                        // pick only Token from response.
                        //var s1 = JsonConvert.DeserializeObject<JObject>(response.Content.ToString());
                        //return s1.GetValue("idToken").ToString();
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
