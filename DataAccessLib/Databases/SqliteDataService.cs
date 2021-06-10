using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLib.Databases
{
    public class SqliteDataService : ILocalDataService
    {
        private SQLiteConnection _database;
        public void Initialize(string user)
        {
            if(_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{user}-database.db3");
                _database = new SQLiteConnection(dbPath);
            }
            _database.CreateTable<Data>();
        }
        public async void InsertData(Data data)
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
    }
}
