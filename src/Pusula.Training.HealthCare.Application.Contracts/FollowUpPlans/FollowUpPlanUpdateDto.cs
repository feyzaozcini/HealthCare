using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlanUpdateDto
    {   
        public Guid Id { get; set; }

        [Required]
        public Guid ProtocolId { get; set; }

        public string? Note { get; set; }

        [Required]
        public FollowUpType FollowUpType { get; set; }

        [Required]
        public bool IsSurgeryScheduled { get; set; }
    }
}
