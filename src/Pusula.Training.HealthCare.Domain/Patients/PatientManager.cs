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
        string passportNumber, 
        string email, 
        string mobilePhoneNumber, 
        string emergencyPhoneNumber, 
        Gender gender,
        string motherName, 
        string fatherName, 
        BloodType bloodType, 
        Type type,
        Guid? primaryCountryId,
        Guid? primaryCityId,
        Guid? primaryDistrictId,
        Guid? primaryVillageId,
        string primaryAddressDescription,
        Guid? secondaryCountryId,
        Guid? secondaryCityId,
        Guid? secondaryDistrictId,
        Guid? secondaryVillageId,
        string secondaryAddressDescription
    )
    {
        Check.NotNull(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
        Check.NotNull(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
        Check.NotNull(identityNumber, nameof(identityNumber));
        Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
        Check.Length(passportNumber, nameof(passportNumber), PatientConsts.PassportNumberMaxLength, PatientConsts.PassportNumberMinLength);
        Check.NotNull(email, nameof(email));
        Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
        Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
        Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);
        Check.Length(emergencyPhoneNumber, nameof(emergencyPhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);
        Check.Length(motherName, nameof(motherName), PatientConsts.FirstNameMaxLength, 0);
        Check.Length(fatherName, nameof(fatherName), PatientConsts.FirstNameMaxLength, 0);

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
        Guid companyId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string identityNumber,
        string passportNumber,
        string email,
        string mobilePhoneNumber,
        string emergencyPhoneNumber,
        Gender gender,
        string motherName,
        string fatherName,
        BloodType bloodType,
        Type type,
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
        Check.NotNull(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
        Check.NotNull(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
        Check.NotNull(identityNumber, nameof(identityNumber));
        Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
        Check.Length(passportNumber, nameof(passportNumber), PatientConsts.PassportNumberMaxLength, PatientConsts.PassportNumberMinLength);
        Check.NotNull(email, nameof(email));
        Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
        Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
        Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);
        Check.Length(emergencyPhoneNumber, nameof(emergencyPhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);
        Check.Length(motherName, nameof(motherName), PatientConsts.FirstNameMaxLength, 0);
        Check.Length(fatherName, nameof(fatherName), PatientConsts.FirstNameMaxLength, 0);

        var patient = await patientRepository.GetAsync(id);

        patient.CompanyId = companyId;
        patient.FirstName = firstName;
        patient.LastName = lastName;
        patient.BirthDate = birthDate;
        patient.IdentityNumber = identityNumber;
        patient.PassportNumber = passportNumber;
        patient.Email = email;
        patient.MobilePhoneNumber = mobilePhoneNumber;
        patient.EmergencyPhoneNumber = emergencyPhoneNumber;
        patient.Gender = gender;
        patient.MotherName = motherName;
        patient.FatherName = fatherName;
        patient.BloodType = bloodType;
        patient.Type = type;
        patient.PrimaryCountryId = primaryCountryId ?? Guid.Empty;
        patient.PrimaryCityId = primaryCityId ?? Guid.Empty;
        patient.PrimaryDistrictId = primaryDistrictId ?? Guid.Empty;
        patient.PrimaryVillageId = primaryVillageId ?? Guid.Empty;
        patient.PrimaryAddressDescription = primaryAddressDescription;
        patient.SecondaryCountryId = secondaryCountryId ?? Guid.Empty;
        patient.SecondaryCityId = secondaryCityId ?? Guid.Empty;
        patient.SecondaryDistrictId = secondaryDistrictId ?? Guid.Empty;
        patient.SecondaryVillageId = secondaryVillageId ?? Guid.Empty;
        patient.SecondaryAddressDescription = secondaryAddressDescription;

        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await patientRepository.UpdateAsync(patient);
    }

}