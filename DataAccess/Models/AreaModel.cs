using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class AreaModel
    {
        public string ClusterName { get; set; }
        public string TeamNo { get; set; }
        public int SocialMobilizerId { get; set; }
        public string CHWName { get; set; }
        public int TotalHouseholds { get; set; }
        public int TotalMasjeeds { get; set; }
        public int TotalChildren { get; set; }
        public List<MasjeedModel> Masjeeds { get; set; }
        public List<SchoolModel> Schools { get; set; }
        public List<InfluencerModel> Influencers { get; set; }
        public List<ClinicModel> Clinics { get; set; }
        public List<DoctorModel> Doctors { get; set; }

    }

    public class MasjeedModel
    {
        public string MasjeedName { get; set; }
        public string KeyInfluencer { get; set; }
        public string DoesImamSupportsVaccine { get; set; }
        public bool DoYouHavePermissionForAdsInMasjeed { get; set; }
    }

    public class SchoolModel
    {
        public string SchoolName { get; set; }
        public string KeyInfluencer { get; set; }
    }

    public class InfluencerModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
        public bool DoesHeProvidingSupport { get; set; }
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
}
