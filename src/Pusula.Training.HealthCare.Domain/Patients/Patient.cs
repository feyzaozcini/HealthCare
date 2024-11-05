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

        public virtual Guid CompanyId { get; set; }

        public virtual Guid CountryId { get; set; }

        protected Patient()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = DateTime.Now;
            Email = string.Empty;
            MobilePhoneNumber = string.Empty;
        }

        public Patient(Guid id, Guid companyId,Guid countryId,string firstName, string lastName, DateTime birthDate, string identityNumber, string passportNumber, string email, string mobilePhoneNumber, string emergencyPhoneNumber, Gender gender, int no, string motherName, string fatherName, BloodType bloodType, Type type)
        {

            Id = id;
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

            Id = id;
            CompanyId = companyId;
            CountryId = countryId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            IdentityNumber = identityNumber;
            PassportNumber = passportNumber;
            Email = email;
            MobilePhoneNumber = mobilePhoneNumber;
            EmergencyPhoneNumber = emergencyPhoneNumber;
            No = no;
            MotherName = motherName;
            FatherName = fatherName;
            BloodType = bloodType;
            Type = type;
        }
    }
}