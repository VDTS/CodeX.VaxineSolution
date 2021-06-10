using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Databases
{
    public interface ILocalDataService
    {
        void Initialize(string user);
        void InsertData(Data data);
        void DeleteData(string key);
        string Get(string key);
    }


}
