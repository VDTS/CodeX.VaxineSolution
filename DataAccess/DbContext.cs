using DataAccess.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbContext
    {
        readonly FirebaseClient firebase = new FirebaseClient(UserSecretsManager.Settings["firebase"]);
        public async Task AddDataNode(Object data, string Type)
        {
            await firebase
              .Child(Type)
              .PostAsync(data);
        }

        public async Task UpdateArea(string ClusterName, Object Area, string Type)
        {
            var toUpdatePerson = (await firebase
              .Child(Type)
              .OnceAsync<AreaModel>()).Where(a => a.Object.ClusterName == ClusterName).FirstOrDefault();

            await firebase
              .Child(Type)
              .Child(toUpdatePerson.Key)
              .PutAsync(Area);
        }
        public async Task UpdatePerson(string Email, Object profile)
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

        public async Task<AreaModel> GetArea()
        {
            return (await firebase
              .Child("Area")
              .OnceAsync<AreaModel>()).Select(item => new AreaModel
              {
                  ClusterName = item.Object.ClusterName,
                  CHWName = item.Object.CHWName,
                  SocialMobilizerId = item.Object.SocialMobilizerId,
                  TeamNo = item.Object.TeamNo
              }).FirstOrDefault();
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
