using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Protocols
{
    public class ProtocolUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public virtual Guid Id { get; set; }
        [Required]
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
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

        public ProtocolUpdateDto()
        {
        }
    }
}