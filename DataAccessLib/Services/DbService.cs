using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.DataAccessLib;

namespace DataAccessLib.Services
{
    public class DbService : IDbService
    {
        public DbService()
        {
            ApiHelper.InitializeClient();
        }
        public string Post(string data, string Node)
        {
            string url = $"{Constants.FirebaseBaseUrl}Kandahar-Area/{Node}.json";

            using (var client = new HttpClient())
            {
                var res = client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
                try
                {
                    res.Result.EnsureSuccessStatusCode();
                    return "Data posted";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
            return "Data posted";
        }
        public async Task<string> Get(string Node)
        {
            try
            {
                string url = $"{Constants.FirebaseBaseUrl}Kandahar-Area/{Node}.json";
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return "Error";
                    }

                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public async Task<string> Delete(string Node)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{Constants.FirebaseBaseUrl}Kandahar-Area/{Node}.json"))
                {
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Deleted";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
        }
        public async Task<string> Put(string data, string Node)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), $"{Constants.FirebaseBaseUrl}Kandahar-Area/{Node}.json"))
                {
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
            }
        }
    }
}
