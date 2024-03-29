﻿using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DataAccess.Services
{
    public class DbService
    {
        Auth Account;

        public string FirebaseBaseUrl { get; }
        public static string AppCenterAndroidXamarinKey { get; set; }

        public DbService(string appCenterAndroidXamarinKey)
        {
            AppCenterAndroidXamarinKey = appCenterAndroidXamarinKey;
        }

        public DbService(string FirebaseBaseUrl, string FirebaseApiKey)
        {
            AppCenter.Start($"android={AppCenterAndroidXamarinKey};",
                     typeof(Analytics), typeof(Crashes));

            Account = new Auth(FirebaseApiKey);
            this.FirebaseBaseUrl = FirebaseBaseUrl;
        }

        public async Task<string> Post(string data, string Node)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return "ConnectionError";
            }
            else
            {
                try
                {
                    string url = $"{FirebaseBaseUrl}Kandahar-Area/{Node}.json?auth={Account.IdToken().Result}";
                    using var httpClient = new HttpClient();
                    using var request = new HttpRequestMessage(new HttpMethod("POST"), url);
                    request.Content = new StringContent(data);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(responseBody).GetValue("name").ToString();
                    }
                    else
                    {
                        return "Error";
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    return "ErrorTracked";
                }
            }
        }
        public async Task<string> Get(string Node)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return "ConnectionError";
            }
            else
            {
                try
                {
                    string url = $"{FirebaseBaseUrl}Kandahar-Area/{Node}.json?auth={Account.IdToken().Result}";
                    using var httpClient = new HttpClient();
                    using var request = new HttpRequestMessage(new HttpMethod("GET"), url);
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content.ToString() == "null")
                        {
                            return "null";
                        }
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return "Error";
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    return "ErrorTracked";
                }
            }
        }
        public async Task<string> Delete(string Node)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return "ConnectionError";
            }
            else
            {
                try
                {
                    using var httpClient = new HttpClient();
                    using var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{FirebaseBaseUrl}Kandahar-Area/{Node}.json?auth={Account.IdToken().Result}");
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "null";
                    }
                    else
                    {
                        return "Error";
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    return "ErrorTracked";
                }
            }
        }
        public async Task<string> Put(string data, string Node)
        {
            try
            {
                using var httpClient = new HttpClient();
                using var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{FirebaseBaseUrl}Kandahar-Area/{Node}.json?auth={Account.IdToken().Result}");
                request.Content = new StringContent(data);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return "Submit";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}
