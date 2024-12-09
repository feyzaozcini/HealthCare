using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.PainDetails
{
    public class PainDetailDto : AuditedEntityDto<Guid>
    {
        public string Area { get; set; } = null!;

        public Guid ProtocolId { get; set; }
        public int Score { get; set; }
        public Guid PainTypeId { get; set; }

        public string PainTypeName { get; set; } = null!;

        public string? Description { get; set; }

        public PainRhythm PainRhythm { get; set; }
        public string? OtherPain { get; set; }
        public DateTime StartDate { get; set; }
    }
}
