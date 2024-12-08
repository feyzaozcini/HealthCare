using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRule : Entity<Guid>
    {
        [CanBeNull]
        public virtual Guid? DoctorId { get; set; }
        [CanBeNull]
        public virtual Guid? DepartmentId { get;  set; }

        public virtual Gender Gender { get; private set; } // Kuralın cinsiyet seçeneği

        public virtual int Age { get; private set; } // Kuralın yaş aralığı

        public string Description { get; private set; } // Kuralın açıklaması

        protected AppointmentRule()
        {
            
        }

        public AppointmentRule(Guid id, Guid? doctorId, Guid? departmentId,Gender gender,int age, string description)
        {
            Id = id;
            DepartmentId = departmentId;
            DoctorId = doctorId;
            SetGender(Gender);
            SetAge(age);
            SetDescription(description);
        }
       
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetGender(Gender gender)
        {
            Gender = gender;
        }
        public void SetAge(int age)
        {
            Age = age;
        }


    }
}
