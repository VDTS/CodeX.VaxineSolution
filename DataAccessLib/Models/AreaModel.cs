using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Models
{
    public class ClusterModel
    {
        public Guid Id { get; set; }
        public string ClusterName { get; set; }
        public ClusterModel()
        {
            Id = new Guid();
        }
    }
    public class TeamModel
    {
        public Guid Id { get; set; }
        public string TeamNo { get; set; }
        public int SocialMobilizerId { get; set; }
        public string CHWName { get; set; }
        public TeamModel()
        {
            Id = new Guid();
        }
    }
    public class ClinicModel
    {
        public Guid Id { get; set; }
        public string ClinicName { get; set; }
        public string Fixed { get; set; }
        public string Outreach { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ClinicModel()
        {
            Id = new Guid();
        }
    }
    public class DoctorModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsHeProvindingSupportForSIAAndVaccination { get; set; }
        public DoctorModel()
        {
            Id = new Guid();
        }
    }
    public class InfluencerModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
        public bool DoesHeProvidingSupport { get; set; }
        public InfluencerModel()
        {
            Id = new Guid();
        }
    }
    public class MasjeedModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string MasjeedName { get; set; }
        public string KeyInfluencer { get; set; }
        public bool DoesImamSupportsVaccine { get; set; }
        public bool DoYouHavePermissionForAdsInMasjeed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public MasjeedModel()
        {
            Id = new Guid();
        }
    }
    public class SchoolModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string KeyInfluencer { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public SchoolModel()
        {
            Id = new Guid();
        }
    }
}
