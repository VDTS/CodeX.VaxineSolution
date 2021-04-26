using System;
using System.Collections.Generic;
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
        public async Task Add(ChildModel child)
        {
            await firebase
              .Child("Child")
              .PostAsync(child);
        }
    }
}
