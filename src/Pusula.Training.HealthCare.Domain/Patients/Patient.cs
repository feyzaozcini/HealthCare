using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients
{
    public class Patient : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string FirstName { get; set; }

        [NotNull]
        public virtual string LastName { get; set; }

        [NotNull]
        public virtual DateTime BirthDate { get; set; }

        //Türk Hastalarý için TC Kimlik No
        public virtual string IdentityNumber { get; set; }

        //Yabancý Hastalar için Pasaport No
        public virtual string PassportNumber { get; set; }

        [NotNull]
        public virtual string Email { get; set; }

        [NotNull]
        public virtual string MobilePhoneNumber { get; set; }

        public virtual string EmergencyPhoneNumber { get; set; }   //Home Phone Number Emergency Oldu

        public virtual Gender Gender { get; set; }

        public virtual int No { get; set; }

        public virtual string MotherName { get; set; }

        public virtual string FatherName { get; set; }

        public virtual BloodType BloodType { get; set; }

        //Hasta Türleri
        public virtual Type Type { get; set; }

        public virtual Guid? CompanyId { get; set; }

        public virtual Guid? PrimaryCountryId { get; set; }
        public virtual Guid? PrimaryCityId { get; set; }
        public virtual Guid? PrimaryDistrictId { get; set; }
        public virtual Guid? PrimaryVillageId { get; set; }
        public virtual string? PrimaryAddressDescription { get; set; }


        public virtual Guid? SecondaryCountryId { get; set; }
        public virtual Guid? SecondaryCityId { get; set; }
        public virtual Guid? SecondaryDistrictId { get; set; }
        public virtual Guid? SecondaryVillageId { get; set; }
        public virtual string? SecondaryAddressDescription { get; set; }



        protected Patient()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = DateTime.Now;
            Email = string.Empty;
            MobilePhoneNumber = string.Empty;

            //Gender = Gender.Unspecified;
        }

        public Patient(Guid id, Guid? companyId,string firstName, string lastName, DateTime birthDate, string identityNumber, string passportNumber,
            string email, string mobilePhoneNumber, string emergencyPhoneNumber, Gender gender, string motherName, string fatherName, BloodType bloodType,
            Type type, Guid? primaryCountryId, Guid? primaryCityId, Guid? primaryDistrictId, Guid? primaryVillageId, string primaryAddressDescription,
            Guid? secondaryCountryId, Guid? secondaryCityId, Guid? secondaryDistrictId, Guid? secondaryVillageId, string secondaryAddressDescription)
        {

            Id = id;
            Check.NotNull(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength, 0);
            Check.NotNull(lastName, nameof(lastName));
            Check.Length(lastName, nameof(lastName), PatientConsts.LastNameMaxLength, 0);
            Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength, 0);
            Check.NotNull(email, nameof(email));
            Check.Length(email, nameof(email), PatientConsts.EmailAddressMaxLength, 0);
            Check.NotNull(mobilePhoneNumber, nameof(mobilePhoneNumber));
            Check.Length(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength, PatientConsts.PhoneNumberMinLength);

            Id = id;
            CompanyId = companyId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            IdentityNumber = identityNumber;
            PassportNumber = passportNumber;
            Email = email;
            MobilePhoneNumber = mobilePhoneNumber;
            EmergencyPhoneNumber = emergencyPhoneNumber;
            Gender = gender;
            MotherName = motherName;
            FatherName = fatherName;
            BloodType = bloodType;
            Type = type;
            PrimaryCountryId = primaryCountryId;
            PrimaryCityId = primaryCityId;
            PrimaryDistrictId = primaryDistrictId;
            PrimaryVillageId = primaryVillageId;
            PrimaryAddressDescription = primaryAddressDescription;
            SecondaryCountryId = secondaryCountryId;
            SecondaryCityId = secondaryCityId;
            SecondaryDistrictId = secondaryDistrictId;
            SecondaryVillageId = secondaryVillageId;
            SecondaryAddressDescription = secondaryAddressDescription;
        }
    }
}