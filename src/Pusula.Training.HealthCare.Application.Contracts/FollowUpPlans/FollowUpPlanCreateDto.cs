using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlanCreateDto
    {
        [Required]
        public Guid ProtocolId { get; set; }

        public string? Note { get; set; }

        [Required(ErrorMessage = "Takip tipi alanı zorunludur.")]
        public FollowUpType FollowUpType { get; set; }

        [Required(ErrorMessage = "Ameliyat planlama durumu secimi yapiniz")]
        public bool IsSurgeryScheduled { get; set; }
    }
}
