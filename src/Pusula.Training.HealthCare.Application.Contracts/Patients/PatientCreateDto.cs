using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Patients;

public class PatientCreateDto
{
    [Required]
    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(PatientConsts.LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [RegularExpression(@"^[1-9]{1}[0-9]{9}[02468]{1}$")]
    [StringLength(PatientConsts.IdentityNumberMaxLength)]
    public string IdentityNumber { get; set; } = null!;

    [StringLength(PatientConsts.PassportNumberMaxLength)]
    public string PassportNumber { get; set; } = null!;

    [EmailAddress]
    [StringLength(PatientConsts.EmailAddressMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string MobilePhoneNumber { get; set; } = null!;

    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string EmergencyPhoneNumber { get; set; } = null!;

    [Required]
    public Gender Gender { get; set; }

    //public int No { get; set; }

    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string MotherName { get; set; } = null!;

    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string FatherName { get; set; } = null!;
    public BloodType BloodType { get; set; }

    [Required]
    public Type Type { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? PrimaryCountryId { get; set; }
    public Guid? PrimaryCityId { get; set; }
    public Guid? PrimaryDistrictId { get; set; }
    public Guid? PrimaryVillageId { get; set; }
    public string? PrimaryAddressDescription { get; set; }
    public Guid? SecondaryCountryId { get; set; }
    public Guid? SecondaryCityId { get; set; }
    public Guid? SecondaryDistrictId { get; set; }
    public Guid? SecondaryVillageId { get; set; }
    public string? SecondaryAddressDescription { get; set; }
}