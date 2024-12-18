using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlanDto : FullAuditedEntityDto<Guid>
    { 
        public Guid ProtocolId { get; set; }

        public string? Note { get; set; }
        public FollowUpType FollowUpType { get; set; }

        public bool IsSurgeryScheduled { get; set; }
    }
}
