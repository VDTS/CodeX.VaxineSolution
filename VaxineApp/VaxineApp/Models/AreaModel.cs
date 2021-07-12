using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VaxineApp.Models
{
    public class ClusterModel
    {
        public Guid Id { get; set; }
        public string ClusterName { get; set; }
        public string CurrentVaccinePeriodId { get; set; }
        public ClusterModel()
        {
            Id = new Guid();
        }
    }

    public class ClusterValidator : AbstractValidator<ClusterModel>
    {
        public ClusterValidator()
        {
            RuleFor(c => c.ClusterName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty");
        }
    }

    public class TeamModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string TeamNo { get; set; }
        public int SocialMobilizerId { get; set; }
        public string CHWName { get; set; }
        public TeamModel()
        {
            Id = new Guid();
        }
    }
    public class TeamValidator : AbstractValidator<TeamModel>
    {
        public TeamValidator()
        {
            RuleFor(t => t.TeamNo).NotEmpty();
            RuleFor(t => t.SocialMobilizerId).NotEmpty();
            RuleFor(t => t.CHWName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
    public class ClinicModel
    {
        public Guid Id { get; set; }
        public string FId { get; set; }
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
    public class ClinicValidator : AbstractValidator<ClinicModel>
    {
        public ClinicValidator()
        {
            RuleFor(c => c.ClinicName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
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
    public class DoctorValidator : AbstractValidator<DoctorModel>
    {
        public DoctorValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
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
    public class InfluencerValidator : AbstractValidator<InfluencerModel>
    {
        public InfluencerValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
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
    public class MasjeedValidator : AbstractValidator<MasjeedModel>
    {
        public MasjeedValidator()
        {
            RuleFor(m => m.MasjeedName)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");

        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
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
    public class SchoolValidator : AbstractValidator<SchoolModel>
    {
        public SchoolValidator()
        {
            RuleFor(s => s.SchoolName)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
        }
        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
