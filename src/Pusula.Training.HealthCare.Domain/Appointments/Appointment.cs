using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Appointments
{
    public class Appointment : FullAuditedAggregateRoot<Guid>
    {

        [NotNull]
        public DateTime StartDate { get; private set; }
        [NotNull]
        public DateTime EndDate { get; private set; }
        [CanBeNull]
        public virtual string Note { get; private set; } = null!;
        [CanBeNull]
        public virtual AppointmentStatus AppointmentStatus { get; private set; }

        public virtual bool IsBlock { get; private set; }

        public virtual Guid PatientId { get; private set; }

        public virtual Guid DoctorId { get; private set; }

        public virtual Guid DepartmentId { get; private set; }

        public virtual Guid AppointmentTypeId { get; private set; }

        protected Appointment()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public Appointment(Guid id, DateTime startDate, DateTime endDate, string note, AppointmentStatus appointmentStatus, bool isBlok,Guid patientId, Guid doctorId, Guid departmentId, Guid appointmentTypeId)
        {
            Id = id;
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetNote(note);
            SetAppointmentStatus(appointmentStatus);
            SetBlock(isBlok);
            SetPatientId(patientId);
            SetDoctorId(doctorId);
            SetDepartmentId(departmentId);
            SetAppointmentType(appointmentTypeId);
        }


        public void SetStartDate(DateTime startDate)
        {
            Check.NotNull(startDate, nameof(startDate));
            StartDate = startDate;
        }
        public void SetEndDate(DateTime endDate)
        {
            Check.NotNull(endDate, nameof(endDate));
            EndDate = endDate;
        }
        public void SetNote(string note)
        {
            Check.Length(Note, nameof(Note), AppointmentConst.NoteMaxLength);
            Note = note;
        }
        public void SetAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            //Check.Range((int)appointmentStatus, nameof(appointmentStatus), 1, 10);
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
        public void SetBlock(bool isBlock)
        {
            IsBlock = isBlock;
        }

        
    }
}
