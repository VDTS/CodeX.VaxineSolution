using DataAccessLib.Services;
using RealCacheLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace VaxineApp.RealCacheLib
{
    public class RequestsHandler
    {
        SQLiteCache sqliteCache = new SQLiteCache();
        protected DbService DataService = new DbService(Constants.FirebaseBaseUrl, Constants.FirebaseApiKey);
        public RequestsHandler()
        {
            sqliteCache.Initialize(Preferences.Get("ProfileEmail", ""));
        }
        public async Task<string> Get(string key)
        {
            try
            {
                if (sqliteCache.IsAvailableInSQLiteCache(key))
                {
                    return sqliteCache.Get(key);
                }
                else
                {
                    var data = await DataService.Get(key);
                    sqliteCache.InsertData(new Data { Key = key, Value = data });
                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
