using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Appointments
{
    public class Appointment : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public DateTime AppointmentDate { get; private set; } 
        [NotNull]
        public TimeSpan StartTime { get; private set; } 
        [NotNull]
        public TimeSpan EndTime { get; private set; }
        [CanBeNull]
        public virtual string Note { get; private set; } = null!; 
        [CanBeNull]
        public virtual AppointmentStatus AppointmentStatus { get; private set; }
        
        public virtual Guid PatientId { get; private set; }
       
        public virtual Guid DoctorId { get; private set; }
        
        public virtual Guid DepartmentId { get; private set; }
       
        public virtual Guid AppointmentTypeId { get; private set; }

        protected Appointment()
        {
            AppointmentDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            EndTime = DateTime.Now.TimeOfDay;
        }

        public Appointment(Guid id, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime, string note, AppointmentStatus appointmentStatus, Guid patientId, Guid doctorId, Guid departmentId, Guid appointmentTypeId)
        {
            Id = id;
            SetAppointmentDate(appointmentDate);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetNote(note);
            SetAppointmentStatus(appointmentStatus);
            SetPatientId(patientId);
            SetDoctorId(doctorId);
            SetDepartmentId(departmentId);
            SetAppointmentType(appointmentTypeId);
        }

        public void SetAppointmentDate(DateTime appointmentDate)
        {
            Check.NotNull(appointmentDate, nameof(appointmentDate));
            AppointmentDate = appointmentDate;
        }
        public void SetStartTime(TimeSpan startTime)
        {
            Check.NotNull(startTime, nameof(startTime));
            StartTime = startTime;
        }
        public void SetEndTime(TimeSpan endTime)
        {
            Check.NotNull(endTime, nameof(endTime));
            EndTime = endTime;
        }
        public void SetNote(string note)
        {
            Check.Length(Note, nameof(Note), AppointmentConst.NoteMaxLength);
            Note = note;
        }
        public void SetAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            Check.Range((int)appointmentStatus, nameof(appointmentStatus), 1, 10);
            AppointmentStatus = appointmentStatus;
        }
        public void SetPatientId(Guid patientId)
        {
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            PatientId = patientId;
        }
        public void SetDoctorId(Guid doctorId)
        {
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            DoctorId = doctorId;
        }
        public void SetDepartmentId(Guid departmentId)
        {
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            DepartmentId = departmentId;
        }
        public void SetAppointmentType(Guid appointmentTypeId)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            AppointmentTypeId = appointmentTypeId;
        }

    }
}
