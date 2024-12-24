using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRule : Entity<Guid>
    {
        [CanBeNull]
        public virtual Guid? DoctorId { get; private set; }
        [CanBeNull]
        public virtual Guid? DepartmentId { get;  private set; }

        public virtual Gender? Gender { get; private set; } // Kuralın cinsiyet seçeneği

        public virtual int? Age { get; private set; } // Kullanılmıyor

        public virtual int? MinAge { get; private set; } 

        public virtual int? MaxAge { get; private set; }

        public string? Description { get; private set; } // Kuralın açıklaması

        public virtual Doctor Doctor { get; private set; } = null!;
        public virtual Department Department { get; private set; } = null!;


        protected AppointmentRule()
        {
            
        }

        public AppointmentRule(Guid id, Guid? doctorId, Guid? departmentId,Gender? gender,int? age,int? minAge,int? maxAge, string? description)
        {
            Id = id;
            SetDepartmentId(departmentId);
            SetDoctorId(doctorId);
            SetGender(gender);
            SetAge(age);
            SetMinAge(minAge);
            SetMaxAge(maxAge);
            SetDescription(description);
        }
       
        public void SetDescription(string? description) => Description = description;
        public void SetGender(Gender? gender) => Gender = gender;
        public void SetAge(int? age) => Age = age;
        public void SetMinAge(int? minAge) => MinAge = minAge;
        public void SetMaxAge(int? maxAge) => MaxAge = maxAge;
        public void SetDoctorId(Guid? doctorId) => DoctorId = doctorId;
        public void SetDepartmentId(Guid? departmentId) => DepartmentId = departmentId;
    }
}
