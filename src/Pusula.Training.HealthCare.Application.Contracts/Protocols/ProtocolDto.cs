using Pusula.Training.HealthCare.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Protocols
{
    public class ProtocolDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        [Required]
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        [Required]
        public virtual int No { get; set; }
        public ProtocolStatus ProtocolStatus { get; set; }
        [Required]
        public virtual Guid ProtocolTypeId { get; set; }
        public virtual Guid ProtocolNoteId { get; set; }
        public string NoteText { get; set; }

        [Required]
        public virtual Guid ProtocolInsuranceId { get; set; }
        [Required]
        public virtual Guid PatientId { get; set; }

        public  string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }

        public string PatientNo { get; set; }

        public Gender PatientGender { get; set; }

        public DateTime PatientBirthDate { get; set; }



        [Required]
        public virtual Guid DepartmentId { get; set; }
        [Required]
        public virtual Guid DoctorId { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
        public ProtocolDto()
        {
           

        }
    }
}