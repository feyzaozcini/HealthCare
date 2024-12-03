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
        [Required]
        public virtual Guid ProtocolInsuranceId { get; set; }
        [Required]
        public virtual Guid PatientId { get; set; }
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