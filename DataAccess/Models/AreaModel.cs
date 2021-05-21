using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class TeamModel
    {
        public string TeamNo { get; set; }
        public int SocialMobilizerId { get; set; }
        public string CHWName { get; set; }
    }
    public class ClinicModel
    {
        public string ClinicName { get; set; }
        public string Fixed { get; set; }
        public string Outreach { get; set; }
    }
    public class DoctorModel
    {
        public string Name { get; set; }
        public bool IsHeProvindingSupportForSIAAndVaccination { get; set; }
    }
    public class InfluencerModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
        public bool DoesHeProvidingSupport { get; set; }
    }
    public class MasjeedModel
    {
        public string MasjeedName { get; set; }
        public string KeyInfluencer { get; set; }
        public bool DoesImamSupportsVaccine { get; set; }
        public bool DoYouHavePermissionForAdsInMasjeed { get; set; }
    }
    public class SchoolModel
    {
        public string SchoolName { get; set; }
        public string KeyInfluencer { get; set; }
    }
}
