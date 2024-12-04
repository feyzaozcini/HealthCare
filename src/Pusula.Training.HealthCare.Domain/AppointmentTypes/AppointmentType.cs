using JetBrains.Annotations;
using Pusula.Training.HealthCare.DoctorAppoinmentTypes;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentType : AuditedEntity<Guid>
    {
        [NotNull]
        public string Name { get; private set; } = null!;

        [NotNull]
        public int DurationInMinutes { get; private set; } //Doktorın randevu sürelerini tutmak için dakika olarak ekleniyor

        public virtual ICollection<DoctorAppoinmentType> DoctorAppointmentTypes { get; set; } 



        protected AppointmentType()
        {
            Name = string.Empty;
            DurationInMinutes = 0;
            DoctorAppointmentTypes = new Collection<DoctorAppoinmentType>();
        }

        public AppointmentType(Guid id, string name,int durationInMinutes)
        {
            Id = id;
            SetName(name);
            DurationInMinutes = durationInMinutes;
            SetDurationInMinutes(durationInMinutes);
            DoctorAppointmentTypes = new Collection<DoctorAppoinmentType>();
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), AppointmentTypeConst.NameMaxLength, AppointmentTypeConst.NameMinLength);
            Name = name;
        }

        public void SetDurationInMinutes(int durationInMinutes) 
        {
            Check.Range(durationInMinutes, nameof(durationInMinutes), 0, 60);
            DurationInMinutes = durationInMinutes;
        }

        public void AddDoctor(Guid doctorId)
        {
            if (DoctorAppointmentTypes.Any(dd => dd.DoctorId == doctorId))
            {
                return;
            }

            DoctorAppointmentTypes.Add(new DoctorAppoinmentType(doctorId, Id));
        }

        private bool IsInDoctor(Guid doctorId)
        {
            return DoctorAppointmentTypes.Any(x => x.DoctorId == doctorId);
        }
    }
}
