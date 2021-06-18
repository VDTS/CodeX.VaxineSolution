using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Services
{
    public class DbService : IDbService
    {
        public string BaseUrl { get; set; }
        public DbService()
        {
            ApiHelper.InitializeClient();
            BaseUrl = $"https://my-first-project-test-a22d1-default-rtdb.firebaseio.com/";
        }
        public string Post(string data, string Node)
        {
            string url = $"{BaseUrl}Kandahar-Area/{Node}.json";

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
                string url = $"{BaseUrl}Kandahar-Area/{Node}.json";
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
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{BaseUrl}Kandahar-Area/{Node}.json"))
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
    }
}
