using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients
{
    public class Patient : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string FirstName { get; private set; }

        [NotNull]
        public virtual string LastName { get; private set; }

        [NotNull]
        public virtual DateTime BirthDate { get; private set; }

        //T�rk Hastalar� i�in TC Kimlik No
        public virtual string IdentityNumber { get; private set; }

        //Yabanc� Hastalar i�in Pasaport No
        public virtual string? PassportNumber { get; private set; }

        
        public virtual string? Email { get; private set; }

        [NotNull]
        public virtual string MobilePhoneNumber { get; private set; }

        public virtual string? EmergencyPhoneNumber { get; private set; }   //Home Phone Number Emergency Oldu

        public virtual Gender Gender { get; private set; }

        public virtual int No { get; set; }

        public virtual string? MotherName { get; private set; }

        public virtual string? FatherName { get; private set; }

        public virtual BloodType? BloodType { get; private set; }

        //Hasta T�rleri
        public virtual Type? Type { get; private set; }

        public virtual Guid? CompanyId { get; private set; }

        public virtual Guid? PrimaryCountryId { get; private set; }
        public virtual Guid? PrimaryCityId { get; private set; }
        public virtual Guid? PrimaryDistrictId { get; private set; }
        public virtual Guid? PrimaryVillageId { get; private set; }
        public virtual string? PrimaryAddressDescription { get; private set; }


        public virtual Guid? SecondaryCountryId { get; private set; }
        public virtual Guid? SecondaryCityId { get; private set; }
        public virtual Guid? SecondaryDistrictId { get; private set; }
        public virtual Guid? SecondaryVillageId { get; private set; }
        public virtual string? SecondaryAddressDescription { get; private set; }



        protected Patient()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = DateTime.Now;
            Email = string.Empty;
            MobilePhoneNumber = string.Empty;

            //Gender = Gender.Unspecified;
        }

        public Patient(Guid id, Guid? companyId,string firstName, string lastName, DateTime birthDate, string identityNumber, string? passportNumber,
            string? email, string mobilePhoneNumber, string? emergencyPhoneNumber, Gender gender, string? motherName, string? fatherName, BloodType? bloodType,
            Type? type, Guid? primaryCountryId, Guid? primaryCityId, Guid? primaryDistrictId, Guid? primaryVillageId, string? primaryAddressDescription,
            Guid? secondaryCountryId, Guid? secondaryCityId, Guid? secondaryDistrictId, Guid? secondaryVillageId, string? secondaryAddressDescription)
        {
            Id = id;
            SetCompanyId(companyId);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetBirthDate(birthDate);
            SetIdentityNumber(identityNumber);
            SetPassportNumber(passportNumber);
            SetEmail(email);
            SetMobilePhoneNumber(mobilePhoneNumber);
            SetEmergencyPhoneNumber(emergencyPhoneNumber);
            SetGender(gender);
            SetMotherName(motherName);
            SetFatherName(fatherName);
            SetBloodType(bloodType);
            SetType(type);
            SetPrimaryCountryId(primaryCountryId);
            SetPrimaryCityId(primaryCityId);
            SetPrimaryDistrictId(primaryDistrictId);
            SetPrimaryVillageId(primaryVillageId);
            SetPrimaryAddressDescription(primaryAddressDescription);
            SetSecondaryCountryId(secondaryCountryId);
            SetSecondaryCityId(secondaryCityId);
            SetSecondaryDistrictId(secondaryDistrictId);
            SetSecondaryVillageId(secondaryVillageId);
            SetSecondaryAddressDescription(secondaryAddressDescription);
        }

        public void SetCompanyId(Guid? companyId) => CompanyId = companyId;

        public void SetFirstName(string firstName)
        {
            Check.NotNull(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            Check.NotNull(lastName, nameof(lastName));
            Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
            LastName = lastName;
        }

        public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;

        public void SetIdentityNumber(string identityNumber)
        {
            Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
            IdentityNumber = identityNumber;
        }

        public void SetPassportNumber(string? passportNumber) => PassportNumber = passportNumber;


        public void SetEmail(string? email)
        {
            Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
            Email = email;
        }

        public void SetMobilePhoneNumber(string mobilePhoneNumber)
        {
            Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
            Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);
            MobilePhoneNumber = mobilePhoneNumber;
        }

        public void SetEmergencyPhoneNumber(string? emergencyPhoneNumber) => EmergencyPhoneNumber = emergencyPhoneNumber;
        public void SetGender(Gender gender) => Gender = gender;
        public void SetMotherName(string? motherName) => MotherName = motherName;
        public void SetFatherName(string? fatherName) => FatherName = fatherName;
        public void SetBloodType(BloodType? bloodType) => BloodType = bloodType;
        public void SetType(Type? type) => Type = type;
        public void SetPrimaryCountryId(Guid? primaryCountryId) => PrimaryCountryId = primaryCountryId;
        public void SetPrimaryCityId(Guid? primaryCityId) => PrimaryCityId = primaryCityId;
        public void SetPrimaryDistrictId(Guid? primaryDistrictId) => PrimaryDistrictId = primaryDistrictId;
        public void SetPrimaryVillageId(Guid? primaryVillageId) => PrimaryVillageId = primaryVillageId;
        public void SetPrimaryAddressDescription(string? primaryAddressDescription) => PrimaryAddressDescription = primaryAddressDescription;
        public void SetSecondaryCountryId(Guid? secondaryCountryId) => SecondaryCountryId = secondaryCountryId;
        public void SetSecondaryCityId(Guid? secondaryCityId) => SecondaryCityId = secondaryCityId;
        public void SetSecondaryDistrictId(Guid? secondaryDistrictId) => SecondaryDistrictId = secondaryDistrictId;
        public void SetSecondaryVillageId(Guid? secondaryVillageId) => SecondaryVillageId = secondaryVillageId;
        public void SetSecondaryAddressDescription(string? secondaryAddressDescription) => SecondaryAddressDescription = secondaryAddressDescription;
    }
}