using Volo.Abp.Application.Dtos;
using System;
using System.Globalization;

namespace Pusula.Training.HealthCare.Patients;

public class GetPatientsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDateMin { get; set; }
    public DateTime? BirthDateMax { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PassportNumber { get; set; }
    public string? Email { get; set; }
    public string? MobilePhoneNumber { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public int? No { get; set; }
    public string? MotherName { get; set; }
    public string? FatherName { get; set; }
    public BloodType? BloodType { get; set; }
    public Type? Type { get; set; }
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

    public GetPatientsInput()
    {
    }
}