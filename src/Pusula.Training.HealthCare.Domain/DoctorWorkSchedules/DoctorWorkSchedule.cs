using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;


namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkSchedule : Entity<Guid>
    {
        [NotNull]
        public virtual Guid DoctorId { get; private set; }

        //Schedule İçin Tanımlama Türleri Bu Şekilde Yapıldı
        public virtual int[] WorkingDays { get; private set; } = new int[7]; // Çalışma günleri (Pazar = 0, Pazartesi = 1, vb.)
        public virtual string StartHour { get; private set; } //
        public virtual string EndHour { get; private set; }


        protected DoctorWorkSchedule()
        {

        }

        public DoctorWorkSchedule(Guid id, Guid doctorId, int[] workingDays, string startHour, string endHour)
        {
            Id = id;
            SetDoctorId(doctorId);
            SetWorkingDays(workingDays);
            SetStartHour(startHour);
            SetEndHour(endHour);
        }


        public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotNull(doctorId, nameof(doctorId));
        public void SetWorkingDays(int[] workingDays) => WorkingDays = Check.NotNull(workingDays, nameof(workingDays));
        public void SetStartHour(string startHour) => StartHour = Check.NotNullOrWhiteSpace(startHour, nameof(startHour));
        public void SetEndHour(string endHour) => EndHour = Check.NotNullOrWhiteSpace(endHour, nameof(endHour));
    }
}
