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


        // Get Methods
        public async Task<GetTeamModel> GetTeam(string ClusterName, string TeamNo)
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
        public async Task<List<ChildModel>> GetChild()
        {
            return (await Firebase
              .Child("Child")
              .OnceAsync<DataModel>()).Select(item => new ChildModel
              {
                  FullName = item.Object.FullName,
                  DOB = item.Object.DOB,
                  Gender = item.Object.Gender,
                  OPV0 = item.Object.OPV0,
                  RINo = item.Object.RINo

              }).ToList();
        }
        public async Task<List<ClinicModel>> GetClinic(string ClusterName)
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
        public async Task<List<DoctorModel>> GetDoctor(string ClusterName)
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

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Doctor").OnceAsync<DoctorModel>())
                .Select(item => new DoctorModel
                {
                    Name = item.Object.Name,
                    IsHeProvindingSupportForSIAAndVaccination = item.Object.IsHeProvindingSupportForSIAAndVaccination
                }).ToList();

        }
        public async Task<List<InfluencerModel>> GetInfluencer(string ClusterName)
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

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Influencer").OnceAsync<InfluencerModel>())
                .Select(item => new InfluencerModel
                {
                    Name = item.Object.Name,
                    Contact = item.Object.Contact,
                    DoesHeProvidingSupport = item.Object.DoesHeProvidingSupport,
                    Position = item.Object.Position
                }).ToList();

        }
        public async Task<List<MasjeedModel>> GetMasjeed(string ClusterName)
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

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Masjeed").OnceAsync<MasjeedModel>())
                .Select(item => new MasjeedModel
                {
                    MasjeedName = item.Object.MasjeedName,
                    KeyInfluencer = item.Object.KeyInfluencer,
                    DoesImamSupportsVaccine = item.Object.DoesImamSupportsVaccine ,
                    DoYouHavePermissionForAdsInMasjeed = item.Object.DoYouHavePermissionForAdsInMasjeed
                }).ToList();

        }
        public async Task<List<SchoolModel>> GetSchool(string ClusterName)
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

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/School").OnceAsync<SchoolModel>())
                .Select(item => new SchoolModel
                {
                    SchoolName = item.Object.SchoolName,
                    KeyInfluencer = item.Object.KeyInfluencer
                }).ToList();

        }
        public async Task<List<GetFamilyModel>> GetFamily(string ClusterName)
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

            return (await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Families").OnceAsync<GetFamilyModel>())
                .Select(item => new GetFamilyModel
                {
                    HouseNo = item.Object.HouseNo,
                    ParentName = item.Object.ParentName,
                    PhoneNumber = item.Object.PhoneNumber
                }).ToList();

        }
        public async Task<ProfileModel> GetProfile(string Email)
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
              }).ToList()
              .Where(item => item.Email == Email).FirstOrDefault();
        }
        

        // Post Methods
        public async Task PostData(Object data, string URL)
        {
            await Firebase
              .Child(URL)
              .PostAsync(data);
        }
        public async Task PostTeam(Object data, string ClusterName, string URL)
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
        public async Task PostClinic(Object data, string URL)
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
        public async Task PostChild(Object data, string URL, int HouseNo)
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

            var f = (await Firebase.Child("Kandahar-Area").Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Families/{f}").PostAsync(data);
        }
        public async Task PostDoctor(Object data, string URL)
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

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Doctor").PostAsync(data);
        }
        public async Task PostInfluencer(Object data, string URL)
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

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Influencer").PostAsync(data);
        }
        public async Task PostMasjeed(Object data, string URL)
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

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Masjeed").PostAsync(data);
        }
        public async Task PostSchool(Object data, string URL)
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

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/School").PostAsync(data);
        }
        public async Task PostFamily(Object data, string URL)
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

            await Firebase.Child($"Kandahar-Area/{j}/Teams/{p}/Families").PostAsync(data);
        }


        // Put
        public async Task PutTeam(string ClusterName, string TeamNo, object data)
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
        public async Task PutProfile(string Email, Object profile)
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
