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

    public DateTime BirthDate { get; set; }

    
    [RegularExpression(@"^[1-9]{1}[0-9]{9}[02468]{1}$")]
    [StringLength(PatientConsts.IdentityNumberMaxLength)]
    public string IdentityNumber { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(PatientConsts.EmailAddressMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.MobilePhoneNumberMaxLength)]
    public string MobilePhoneNumber { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.MobilePhoneNumberMaxLength)]
    public string EmergencyPhoneNumber { get; set; } = null!;

    [Required]
    public Gender Gender { get; set; }

    public int No { get; set; } 

    public string MotherName { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public BloodType BloodType { get; set; }

    public Type Type { get; set; }

    public Guid CompanyId { get; set; }

    public Guid CountryId { get; set; }
}