using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackList : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual BlackListStatus BlackListStatus { get; private set; }

        [CanBeNull]
        public virtual string Note { get; private set; } = null!;

        public virtual Guid PatientId { get; private set; }

        public virtual Guid DoctorId { get; private set; }

        public virtual Patient Patient { get; private set; } = null!;
        public virtual Doctor Doctor { get; private set; } = null!;
        protected BlackList()
        {
        }

        public BlackList(Guid id, BlackListStatus blackListStatus, string note, Guid patientId, Guid doctorId)
        {
            Id = id;
            SetBlackListStatus(blackListStatus);
            SetNote(note);
            SetPatientId(patientId);
            SetDoctorId(doctorId);
        }

        public void SetBlackListStatus(BlackListStatus blackListStatus)
        {
            Check.NotNull(blackListStatus, nameof(blackListStatus));
            BlackListStatus = blackListStatus;
        }

        public void SetNote(string note)
        {
            Note = note;
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

    }
}
