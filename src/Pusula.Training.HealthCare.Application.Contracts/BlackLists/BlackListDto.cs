using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListDto : AuditedEntity<Guid>
    {
        [Required]
        public BlackListStatus BlackListStatus { get; set; }

        [Required]
        public string Note { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
    }
}