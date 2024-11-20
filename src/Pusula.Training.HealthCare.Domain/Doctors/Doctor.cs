﻿using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors
{
    public class Doctor : Entity<Guid>
    {
        [NotNull]
        public virtual DateTime BirthDate { get; private set; }

        [NotNull]
        public virtual Gender Gender { get; private set; }

        [NotNull]
        public virtual string IdentityNumber { get; private set; } = string.Empty;

        [NotNull]
        public virtual Guid UserId { get; set; }

        [NotNull]
        public virtual Guid TitleId { get; set; }

        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }


        protected Doctor()
        {
            BirthDate = DateTime.Now;
            Gender = Gender.Unspecified;
            IdentityNumber = string.Empty;
            DoctorDepartments = new List<DoctorDepartment>();

        }

        public Doctor(Guid id, Guid userId, DateTime birthDate, Gender gender, Guid titleId, string identityNumber) 
        {
            Id = id;
            
            SetBirthDate(birthDate);

            SetGender(gender);

            SetIdentityNumber(identityNumber);

            Check.NotNull(userId, nameof(userId));
            UserId = userId;

            Check.NotNull(titleId, nameof(titleId));
            TitleId = titleId;

            DoctorDepartments = new List<DoctorDepartment>();
        }

        public void SetGender(Gender gender)
        {

            Check.NotNull(gender, nameof(gender));
            Gender = Enum.IsDefined(typeof(Gender), gender) ? gender : Gender.Unspecified;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            Check.NotNull(birthDate, nameof(birthDate));
            BirthDate = birthDate > DateTime.Now ? DateTime.Now : birthDate;
        }

        public void SetIdentityNumber(string identityNumber)
        {
            Check.Length(identityNumber, nameof(identityNumber), DoctorConsts.IdentityNumberMaxLength);
            Check.NotNull(identityNumber, nameof(identityNumber));
            IdentityNumber = identityNumber;
        }
    }
}
