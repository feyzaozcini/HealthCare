using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Patients;

public class PatientUpdateDto : IHasConcurrencyStamp
{
    [Required]
    public Guid Id { get; set; } = default!;

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

    [StringLength(PatientConsts.PassportNumberMaxLength)]
    public string? PassportNumber { get; set; } = null!;

    
    [EmailAddress]
    [StringLength(PatientConsts.EmailAddressMaxLength)]
    public string? Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string MobilePhoneNumber { get; set; } = null!;

    
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string? EmergencyPhoneNumber { get; set; } = null!;

    [Required]
    public Gender Gender { get; set; }

    //public int No { get; set; }

    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string? MotherName { get; set; } = null!;

    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string? FatherName { get; set; } = null!;
    public BloodType? BloodType { get; set; }
    public Type? Type { get; set; }
    public Guid? CompanyId { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}