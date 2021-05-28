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

        // Constructor
        public DbContext()
        {
            Firebase = new FirebaseClient(@"https://my-first-project-test-a22d1-default-rtdb.firebaseio.com/");
        }


        // Get Methods
        public async Task<TeamModel> GetTeam()
        {
            var j = (await Firebase.Child(SharedData.Area)
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            return (await Firebase
              .Child(SharedData.Area).Child(j).Child("Teams")
              .OnceAsync<JObject>()).Select(item => new TeamModel
              {
                  CHWName = item.Object.GetValue("CHWName").ToString(),
                  SocialMobilizerId = int.Parse(item.Object.GetValue("SocialMobilizerId").ToString()),
                  TeamNo = item.Object.GetValue("TeamNo").ToString()

              }).Where(item => item.TeamNo == SharedData.Team).FirstOrDefault();
        }
        public async Task<List<ClinicModel>> GetClinic()
        {

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Clinics").OnceAsync<ClinicModel>())
                .Select(item => new ClinicModel
                {
                    ClinicName = item.Object.ClinicName,
                    Fixed = item.Object.Fixed,
                    Outreach = item.Object.Outreach,
                    Latitude = double.Parse(item.Object.Latitude.ToString()),
                    Longitude = double.Parse(item.Object.Longitude.ToString())

                }).ToList();

        }
        public async Task<List<ChildModel>> GetChild(int HouseNo)
        {

            var j = (await Firebase.Child(SharedData.Area)
               .OnceAsync<JObject>())
               .ToList()
               .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
               .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();


            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families/{f}/Childs").OnceAsync<ChildModel>())
                .Select(item => new ChildModel
                {
                    HouseNo = item.Object.HouseNo,
                    FullName = item.Object.FullName,
                    DOB = DateTime.Parse(item.Object.DOB.ToString()),
                    Gender = item.Object.Gender,
                    OPV0 = bool.Parse(item.Object.OPV0.ToString()),
                    RINo = int.Parse(item.Object.RINo.ToString())
                }).ToList();

        }
        public async Task<List<DoctorModel>> GetDoctor()
        {

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Doctor").OnceAsync<DoctorModel>())
                .Select(item => new DoctorModel
                {
                    Name = item.Object.Name,
                    IsHeProvindingSupportForSIAAndVaccination = item.Object.IsHeProvindingSupportForSIAAndVaccination
                }).ToList();

        }
        public async Task<List<InfluencerModel>> GetInfluencer()
        {

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Influencer").OnceAsync<InfluencerModel>())
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

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Masjeed").OnceAsync<MasjeedModel>())
                .Select(item => new MasjeedModel
                {
                    MasjeedName = item.Object.MasjeedName,
                    KeyInfluencer = item.Object.KeyInfluencer,
                    DoesImamSupportsVaccine = item.Object.DoesImamSupportsVaccine,
                    DoYouHavePermissionForAdsInMasjeed = item.Object.DoYouHavePermissionForAdsInMasjeed,
                    Latitude = double.Parse(item.Object.Latitude.ToString()),
                    Longitude = double.Parse(item.Object.Longitude.ToString())
                }).ToList();

        }
        public async Task<List<SchoolModel>> GetSchool()
        {

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/School").OnceAsync<SchoolModel>())
                .Select(item => new SchoolModel
                {
                    SchoolName = item.Object.SchoolName,
                    KeyInfluencer = item.Object.KeyInfluencer,
                    Latitude = double.Parse(item.Object.Latitude.ToString()),
                    Longitude = double.Parse(item.Object.Longitude.ToString())
                }).ToList();

        }
        public async Task<List<GetFamilyModel>> GetFamily()
        {

            var j = (await Firebase.Child(SharedData.Area)
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                        .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                        .OnceAsync<JObject>())
                        .ToList()
                        .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                        .Select(item => item.Key).FirstOrDefault();

            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families").OnceAsync<GetFamilyModel>())
                .Select(item => new GetFamilyModel
                {
                    HouseNo = item.Object.HouseNo,
                    ParentName = item.Object.ParentName,
                    PhoneNumber = item.Object.PhoneNumber
                }).ToList();

        }
        public async Task<List<VaccineModel>> GetVaccine(int HouseNo)
        {

            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            var o = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families").Child(f).Child("Childs")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();


            return (await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families/{f}/Childs/{o}/Vaccine").OnceAsync<VaccineModel>())
                .Select(item => new VaccineModel
                {
                    VaccineStatus = item.Object.VaccineStatus,
                    VaccinePeriod = item.Object.VaccinePeriod
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
              .Where(item => item.Email == SharedData.Email).FirstOrDefault();
        }


        // Post Methods
        public async Task PostTeam(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
            .OnceAsync<JObject>())
            .ToList()
            .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
            .Select(item => item.Key).FirstOrDefault();

            await Firebase
              .Child($"{SharedData.Area}/{j}/Teams")
              .PostAsync(data);
        }
        public async Task PostClinic(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Clinics").PostAsync(data);
        }
        public async Task PostChild(Object data, int HouseNo)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families/{f}/Childs").PostAsync(data);
        }
        public async Task PostDoctor(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Doctor").PostAsync(data);
        }
        public async Task PostInfluencer(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Influencer").PostAsync(data);
        }
        public async Task PostMasjeed(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Masjeed").PostAsync(data);
        }
        public async Task PostSchool(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/School").PostAsync(data);
        }
        public async Task PostFamily(Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families").PostAsync(data);
        }
        public async Task PostVaccine(int HouseNo, Object data)
        {
            var j = (await Firebase.Child(SharedData.Area)
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
                .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            var f = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            var o = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Families").Child(f).Child("Childs")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => int.Parse(item.Object.GetValue("HouseNo").ToString()) == HouseNo)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child($"{SharedData.Area}/{j}/Teams/{p}/Families/{f}/Childs/{o}/Vaccine").PostAsync(data);
        }


        // Put
        public async Task PutTeam(object data)
        {
            var c = (await Firebase
              .Child(SharedData.Area)
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName).FirstOrDefault();

            var t = (await Firebase
              .Child(SharedData.Area).Child(c.Key).Child("Teams")
              .OnceAsync<JObject>()).Where(a => a.Object.GetValue("TeamNo").ToString() == SharedData.Team).FirstOrDefault();

            await Firebase
              .Child(SharedData.Area).Child(c.Key).Child("Teams").Child(t.Key)
              .PutAsync(data);
        }
        public async Task PutProfile(Object profile)
        {
            var toUpdatePerson = (await Firebase
              .Child("Profile")
              .OnceAsync<ProfileModel>()).Where(a => a.Object.Email == SharedData.Email).FirstOrDefault();

            await Firebase
              .Child("Profile")
              .Child(toUpdatePerson.Key)
              .PutAsync(profile);
        }


        // Delete

        public async Task DelClinic(string ClinicName)
        {
            var j = (await Firebase.Child(SharedData.Area)
               .OnceAsync<JObject>())
               .ToList()
               .Where(item => item.Object.GetValue("ClusterName").ToString() == SharedData.ClusterName)
               .Select(item => item.Key).FirstOrDefault();

            var p = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("TeamNo").ToString() == SharedData.Team)
                .Select(item => item.Key).FirstOrDefault();

            var d = (await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Clinics")
                .OnceAsync<JObject>())
                .ToList()
                .Where(item => item.Object.GetValue("ClinicName").ToString() == ClinicName)
                .Select(item => item.Key).FirstOrDefault();

            await Firebase.Child(SharedData.Area).Child(j).Child("Teams").Child(p).Child("Clinics").Child(d).DeleteAsync();

        }

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
