using DataAccess.Models;
using DataAccess.Models.Nest;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbContext
    {
        // Firebase URL
        public FirebaseClient Firebase { private set; get; }


        // Constructor
        public DbContext()
        {
            Firebase = new FirebaseClient(UserSecretsManager.Settings["firebase"]);
        }


        // Get
        
        public async Task<GetTeamModel> GetNeArea(string ClusterName, string TeamNo)
        {
            var j = (await Firebase.Child("Kandahar-Area")
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            return (await Firebase
              .Child("Kandahar-Area").Child(j).Child("Teams")
              .OnceAsync<JObject>()).Select(item => new GetTeamModel
              {
                  CHWName = item.Object.GetValue("CHWName").ToString(),
                  SocialMobilizerId = int.Parse(item.Object.GetValue("SocialMobilizerId").ToString()),
                  TeamNo = item.Object.GetValue("TeamNo").ToString()

              }).Where(item => item.TeamNo == TeamNo).FirstOrDefault();
        }
        public async Task<List<ChildModel>> GetChilds()
        {
            return (await Firebase
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
       
        public async Task<List<ClinicModel>> GetNeClinic(string ClusterName)
        {

            var j = (await Firebase.Child("Kandahar-Area")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child("Kandahar-Area").Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == "1")
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Clinics").OnceAsync<ClinicModel>())
                .Select(item => new ClinicModel
                {
                    ClinicName = item.Object.ClinicName,
                    Fixed = item.Object.Fixed,
                    Outreach = item.Object.Outreach
                }).ToList();

        }

        public async Task<List<ProfileModel>> GetProfiles()
        {
            return (await Firebase
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
            await Firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>();
            return allPersons.Where(a => a.Email == Email).FirstOrDefault();
        }


        // Set
        public async Task AddDataNode(Object data, string URL)
        {
            await Firebase
              .Child(URL)
              .PostAsync(data);
        }
        public async Task AddAreaDataNode(Object data, string ClusterName, string URL)
        {
            var j = (await Firebase.Child("Kandahar-Area")
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            await Firebase
              .Child($"Kandahar-Area/{j}/{URL}")
              .PostAsync(data);
        }
        public async Task SaveClinic(Object data, string URL)
        {
            var j = (await Firebase.Child("Kandahar-Area")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == "T")
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child("Kandahar-Area").Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == "1")
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Clinics").PostAsync(data);
        }

        // Put
        public async Task UpdateArea(string ClusterName, string TeamNo, object data)
        {
            var c = (await Firebase
              .Child("Kandahar-Area")
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("ClusterName").ToString() == ClusterName).FirstOrDefault();

            var t = (await Firebase
              .Child("Kandahar-Area").Child(c.Key).Child("Teams")
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("TeamNo").ToString() == TeamNo).FirstOrDefault();

            await Firebase
              .Child("Kandahar-Area").Child(c.Key).Child("Teams").Child(t.Key)
              .PutAsync(data);
        }
        public async Task UpdatePerson(string Email, Object profile)
        {
            var toUpdatePerson = (await Firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>()).Where(a => a.Object.Email == Email).FirstOrDefault();

            await Firebase
              .Child("Profile")
              .Child(toUpdatePerson.Key)
              .PutAsync(profile);
        }


        // Delete
    }
}
