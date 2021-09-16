using SQLite;
using System;
using System.IO;

namespace RealCache.RealCacheLib
{
    public class SQLiteCache
    {
        private SQLiteConnection _database;
        public void Initialize(string user)
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{user}-cache.db3");
                _database = new SQLiteConnection(dbPath);
            }
            _database.CreateTable<Data>();
        }
        public void InsertData(Data data)
        {
            _database.Insert(data);
        }
        public void DeleteData(string key)
        {
            _database.Execute($"DELETE FROM Data WHERE Key == {key}");
        }
        public string Get(string key)
        {
            return _database.Table<Data>()
                            .Where(x => x.Key == key)
                            .FirstOrDefault()
                            .Value
                            .ToString();
        }
        public bool IsAvailableInSQLiteCache(string key)
        {
            var data = _database.Table<Data>()
                .Where(x => x.Key == key)
                .FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
