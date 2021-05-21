using DataAccess.Models;
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
using Xamarin.Essentials;

namespace DataAccess
{
    public class DbContext
    {
        // Firebase URL
        public FirebaseClient Firebase { private set; get; }
        public string ClusterName { get; set; }
        public string Team { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }


        // Constructor
        public DbContext()
        {
            Firebase = new FirebaseClient(UserSecretsManager.Settings["firebase"]);
            ClusterName = Preferences.Get("PrefCluster", "T");
            Team = Preferences.Get("PrefTeam", "1");
            Area = Preferences.Get("PrefArea", "Kandahar-Area");
            Email = Preferences.Get("PrefEmail", "ahmad@gmail.com");
        }


        // Get Methods
        public async Task<TeamModel> GetTeam()
        {
            var j = (await Firebase.Child(Area)
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            return (await Firebase
              .Child(Area).Child(j).Child("Teams")
              .OnceAsync<JObject>()).Select(item => new TeamModel
              {
                  CHWName = item.Object.GetValue("CHWName").ToString(),
                  SocialMobilizerId = int.Parse(item.Object.GetValue("SocialMobilizerId").ToString()),
                  TeamNo = item.Object.GetValue("TeamNo").ToString()

              }).Where(item => item.TeamNo == Team).FirstOrDefault();
        }
        public async Task<List<ClinicModel>> GetClinic()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Clinics").OnceAsync<ClinicModel>())
                .Select(item => new ClinicModel
                {
                    ClinicName = item.Object.ClinicName,
                    Fixed = item.Object.Fixed,
                    Outreach = item.Object.Outreach
                }).ToList();

        }
        public async Task<List<ChildModel>> GetChild(int HouseNo)
        {

            var j = (await Firebase.Child(Area)
               .OnceAsync<JObject>())
               .ToList()
               .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
               .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();


            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Families/{f}/Childs").OnceAsync<ChildModel>())
                .Select(item => new ChildModel
                {
                    FullName = item.Object.FullName,
                    DOB = DateTime.Parse(item.Object.DOB.ToString()),
                    Gender = item.Object.Gender,
                    OPV0 = bool.Parse(item.Object.OPV0.ToString()),
                    RINo = int.Parse(item.Object.RINo.ToString())
                }).ToList();

        }
        public async Task<List<DoctorModel>> GetDoctor()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Doctor").OnceAsync<DoctorModel>())
                .Select(item => new DoctorModel
                {
                    Name = item.Object.Name,
                    IsHeProvindingSupportForSIAAndVaccination = item.Object.IsHeProvindingSupportForSIAAndVaccination
                }).ToList();

        }
        public async Task<List<InfluencerModel>> GetInfluencer()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Influencer").OnceAsync<InfluencerModel>())
                .Select(item => new InfluencerModel
                {
                    Name = item.Object.Name,
                    Contact = item.Object.Contact,
                    DoesHeProvidingSupport = item.Object.DoesHeProvidingSupport,
                    Position = item.Object.Position
                }).ToList();

        }
        public async Task<List<MasjeedModel>> GetMasjeed()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Masjeed").OnceAsync<MasjeedModel>())
                .Select(item => new MasjeedModel
                {
                    MasjeedName = item.Object.MasjeedName,
                    KeyInfluencer = item.Object.KeyInfluencer,
                    DoesImamSupportsVaccine = item.Object.DoesImamSupportsVaccine,
                    DoYouHavePermissionForAdsInMasjeed = item.Object.DoYouHavePermissionForAdsInMasjeed
                }).ToList();

        }
        public async Task<List<SchoolModel>> GetSchool()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/School").OnceAsync<SchoolModel>())
                .Select(item => new SchoolModel
                {
                    SchoolName = item.Object.SchoolName,
                    KeyInfluencer = item.Object.KeyInfluencer
                }).ToList();

        }
        public async Task<List<GetFamilyModel>> GetFamily()
        {

            var j = (await Firebase.Child(Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{Area}/{j}/Teams/{p}/Families").OnceAsync<GetFamilyModel>())
                .Select(item => new GetFamilyModel
                {
                    HouseNo = item.Object.HouseNo,
                    ParentName = item.Object.ParentName,
                    PhoneNumber = item.Object.PhoneNumber
                }).ToList();

        }
        public async Task<ProfileModel> GetProfile()
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
                  Role = item.Object.Role,
                  Cluster = item.Object.Cluster,
                  Team = item.Object.Team,
                  Area = item.Object.Area

              }).ToList()
              .Where(item => item.Email == Email).FirstOrDefault();
        }


        // Post Methods
        public async Task PostTeam(Object data)
        {
            var j = (await Firebase.Child(Area)
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            await Firebase
              .Child($"Kandahar-Area/{j}/Teams")
              .PostAsync(data);
        }
        public async Task PostClinic(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Clinics").PostAsync(data);
        }
        public async Task PostChild(Object data, int HouseNo)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Families/{f}/Childs").PostAsync(data);
        }
        public async Task PostDoctor(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Doctor").PostAsync(data);
        }
        public async Task PostInfluencer(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Influencer").PostAsync(data);
        }
        public async Task PostMasjeed(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Masjeed").PostAsync(data);
        }
        public async Task PostSchool(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child("Kandahar-Area").Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/School").PostAsync(data);
        }
        public async Task PostFamily(Object data)
        {
            var j = (await Firebase.Child(Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{Area}/{j}/Teams/{p}/Families").PostAsync(data);
        }


        // Put
        public async Task PutTeam(object data)
        {
            var c = (await Firebase
              .Child(Area)
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("ClusterName").ToString() == ClusterName).FirstOrDefault();

            var t = (await Firebase
              .Child(Area).Child(c.Key).Child("Teams")
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("TeamNo").ToString() == Team).FirstOrDefault();

            await Firebase
              .Child(Area).Child(c.Key).Child("Teams").Child(t.Key)
              .PutAsync(data);
        }
        public async Task PutProfile(Object profile)
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



        // Get Statistics
        public async Task<int> GetClinicStats()
        {
            var data = await GetClinic();
            return data.Count;
        }

        public async Task<int> GetHouseholdsStats()
        {
            var data = await GetFamily();
            return data.Count;
        }

        public async Task<int> GetChildrenStats()
        {
            int data = 0;
            var family = await GetFamily();
            foreach (var item in family)
            {
                var child = await GetChild(item.HouseNo);
                foreach (var item2 in child)
                {
                    data++;
                }
            }
            return data;
        }

        public async Task<int> GetMasjeedsStats()
        {
            var data = await GetMasjeed();
            return data.Count;
        }

        public async Task<int> GetSchoolsStats()
        {
            var data = await GetSchool();
            return data.Count;
        }

        public async Task<int> GetInfluencersStats()
        {
            var data = await GetInfluencer();
            return data.Count;
        }

        public async Task<int> GetDoctorsStats()
        {
            var data = await GetDoctor();
            return data.Count;
        }


        // Get Data for Charts (Insights)

        public async Task<List<FemaleVsMaleChildModel>> GetFemaleVsMaleChilds()
        {
            int female = 0;
            int male = 0;
            var family = await GetFamily();
            foreach (var item in family)
            {
                var child = await GetChild(item.HouseNo);
                foreach (var item2 in child)
                {
                    if(item2.Gender == "Male")
                    {
                        male++;
                    }
                    else
                    {
                        female++;
                    }
                }
            }
            return new List<FemaleVsMaleChildModel> {
                new FemaleVsMaleChildModel
                {
                    Indicator = "Male",
                    Counts = male
                },
                new FemaleVsMaleChildModel
                {
                    Indicator = "Female",
                    Counts = female
                }
            };
        }
    }
}
