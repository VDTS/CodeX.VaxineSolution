using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using VaxineApp.Models;
using VaxineApp.Models.Home.Area;

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

        public async Task AddArea(AreaModel area)
        {
            await firebase
              .Child("Area")
              .PostAsync(area);
        }
        public async Task Add(ProfileModel profile)
        {
            await firebase
              .Child("Profile")
              .PostAsync(profile);
        }

        public async Task UpdatePerson(string Email, ProfileModel profile)
        {
            var toUpdatePerson = (await firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>()).Where(a => a.Object.Email == Email).FirstOrDefault();

            await firebase
              .Child("Profile")
              .Child(toUpdatePerson.Key)
              .PutAsync(profile);
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
       
        public async Task<List<ProfileModel>> GetProfiles()
        {
            return (await firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>()).Select(item => new ProfileModel
              {
                  FullName = item.Object.FullName,
                  FatherOrHusbandName = item.Object.FatherOrHusbandName,
                  Gender = item.Object.Gender,
                  Age = item.Object.Age,
                  Email = item.Object.Email,
                  Role = item.Object.Role
              }).ToList();
        }

        public async Task<ProfileModel> GetProfile(string Email)
        {
            var allPersons = await GetProfiles();
            await firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>();
            return allPersons.Where(a => a.Email == Email).FirstOrDefault();
        }
    }
}
