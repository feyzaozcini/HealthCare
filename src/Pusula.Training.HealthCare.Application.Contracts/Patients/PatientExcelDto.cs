using System;

namespace Pusula.Training.HealthCare.Patients;

public class PatientExcelDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string MobilePhoneNumber { get; set; } = null!;
    public Gender Gender { get; set; }
    public Type type { get; set; }
    public string? PatientCompany { get; set; }
    public string? PrimaryCountry { get; set; }
    public string? PrimaryCity { get; set; }
    public string? PrimaryDistrict { get; set; }
    public string? PrimaryVillage { get; set; }
    public string? PrimaryAddressDescription { get; set; }
    public string? SecondaryCountry { get; set; }
    public string? SecondaryCity { get; set; }
    public string? SecondaryDistrict { get; set; }
    public string? SecondaryVillage { get; set; }
    public string? SecondaryAddressDescription { get; set; }
}