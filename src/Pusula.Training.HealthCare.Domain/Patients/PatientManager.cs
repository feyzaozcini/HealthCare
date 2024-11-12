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
        Guid companyId,
        Guid countryId,
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
        Type type)
    {
        Check.NotNull(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
        Check.NotNull(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
        Check.NotNull(identityNumber, nameof(identityNumber));
        Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
        Check.NotNull(email, nameof(email));
        Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
        Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
        Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.MobilePhoneNumberMaxLength, 0);
        Check.Length(emergencyPhoneNumber, nameof(emergencyPhoneNumber), PatientConsts.MobilePhoneNumberMaxLength, 0);
        

        var patient = new Patient(
         GuidGenerator.Create(),
         companyId,countryId,firstName, lastName, birthDate, identityNumber, passportNumber, email, mobilePhoneNumber, emergencyPhoneNumber,gender, motherName,fatherName,bloodType,type
         );

        return await patientRepository.InsertAsync(patient);
    }

    public virtual async Task<Patient> UpdateAsync(
        Guid id,
        Guid companyId,
        Guid countryId,
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
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotNull(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
        Check.NotNull(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
        Check.NotNull(identityNumber, nameof(identityNumber));
        Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
        Check.NotNull(email, nameof(email));
        Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
        Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
        Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.MobilePhoneNumberMaxLength, 0);
        Check.Length(emergencyPhoneNumber, nameof(emergencyPhoneNumber), PatientConsts.MobilePhoneNumberMaxLength, 0);

        var patient = await patientRepository.GetAsync(id);

        patient.CompanyId = companyId;
        patient.CountryId = countryId;
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


        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await patientRepository.UpdateAsync(patient);
    }

}