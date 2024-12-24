using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public virtual Guid Id { get; set; }
        [Required]
        public BlackListStatus BlackListStatus { get; set; }

        [Required]
        public string Note { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

        public BlackListUpdateDto()
        {
        }
    }
}
