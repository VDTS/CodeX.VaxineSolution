using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using VaxineApp.Models;

namespace VaxineApp.Services
{
    public class DbContext
    {
        FirebaseClient firebase = new FirebaseClient(UserSecretsManager.Settings["firebase"]);
        public async Task Add(DataModel child)
        {
            await firebase
              .Child("Child")
              .PostAsync(child);
        }

        public async Task<List<ChildModel>> GetChilds()
        {
            return (await firebase
              .Child("Child")
              .OnceAsync<DataModel>()).Select(item => new ChildModel
              {
                  FullName = item.Object.FullName,
                  HouseNo = item.Object.HouseNo,
                  DOB = item.Object.DOB,
                  Gender = item.Object.Gender,
                  OPV0 = item.Object.OPV0,
                  RINo = item.Object.RINo
                  
              }).ToList();
        }
    }
}
