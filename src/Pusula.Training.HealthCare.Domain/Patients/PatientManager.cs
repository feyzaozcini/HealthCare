using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Patients;

public class PatientManager(IPatientRepository patientRepository) : DomainService
{
    public virtual async Task<Patient> CreateAsync(
        Guid? companyId,
        string firstName, 
        string lastName, 
        DateTime birthDate, 
        string identityNumber, 
        string? passportNumber, 
        string? email, 
        string mobilePhoneNumber, 
        string? emergencyPhoneNumber, 
        Gender gender,
        string? motherName, 
        string? fatherName, 
        BloodType? bloodType, 
        Type? type,
        Guid? primaryCountryId,
        Guid? primaryCityId,
        Guid? primaryDistrictId,
        Guid? primaryVillageId,
        string? primaryAddressDescription,
        Guid? secondaryCountryId,
        Guid? secondaryCityId,
        Guid? secondaryDistrictId,
        Guid? secondaryVillageId,
        string? secondaryAddressDescription
    )
    {
        var patient = new Patient(
         GuidGenerator.Create(),
         companyId,firstName, lastName, birthDate, identityNumber, passportNumber, email, mobilePhoneNumber, emergencyPhoneNumber,gender,
         motherName,fatherName,bloodType,type,primaryCountryId, primaryCityId, primaryDistrictId,primaryVillageId,primaryAddressDescription,
         secondaryCountryId,secondaryCityId,secondaryDistrictId,secondaryVillageId,secondaryAddressDescription
         );

        return await patientRepository.InsertAsync(patient);
    }

    public virtual async Task<Patient> UpdateAsync(
        Guid id,
        Guid? companyId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string identityNumber,
        string? passportNumber,
        string? email,
        string mobilePhoneNumber,
        string? emergencyPhoneNumber,
        Gender gender,
        string? motherName,
        string? fatherName,
        BloodType? bloodType,
        Type? type,
        Guid? primaryCountryId,
        Guid? primaryCityId,
        Guid? primaryDistrictId,
        Guid? primaryVillageId,
        string? primaryAddressDescription,
        Guid? secondaryCountryId,
        Guid? secondaryCityId,
        Guid? secondaryDistrictId,
        Guid? secondaryVillageId,
        string? secondaryAddressDescription,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        var patient = await patientRepository.GetAsync(id);

        patient.SetCompanyId(companyId);
        patient.SetFirstName(firstName);
        patient.SetLastName(lastName);
        patient.SetBirthDate(birthDate);
        patient.SetIdentityNumber(identityNumber);
        patient.SetPassportNumber(passportNumber);
        patient.SetEmail(email);
        patient.SetMobilePhoneNumber(mobilePhoneNumber);
        patient.SetEmergencyPhoneNumber(emergencyPhoneNumber);
        patient.SetGender(gender);
        patient.SetMotherName(motherName);
        patient.SetFatherName(fatherName);
        patient.SetBloodType(bloodType);
        patient.SetType(type);
        patient.SetPrimaryCountryId(primaryCountryId);
        patient.SetPrimaryCityId(primaryCityId);
        patient.SetPrimaryDistrictId(primaryDistrictId);
        patient.SetPrimaryVillageId(primaryVillageId);
        patient.SetPrimaryAddressDescription(primaryAddressDescription);
        patient.SetSecondaryCountryId(secondaryCountryId);
        patient.SetSecondaryCityId(secondaryCityId);
        patient.SetSecondaryDistrictId(secondaryDistrictId);
        patient.SetSecondaryVillageId(secondaryVillageId);
        patient.SetSecondaryAddressDescription(secondaryAddressDescription);
        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await patientRepository.UpdateAsync(patient);
    }

}