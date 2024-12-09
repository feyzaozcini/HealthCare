using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PainDetails
{
    public class PainDetailUpdateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Area { get; set; } = null!;

        [Required]
        public Guid ProtocolId { get; set; }

        [Required]
        public int Score { get; set; }
        public Guid PainTypeId { get; set; }

        public string? Description { get; set; }

        public PainRhythm PainRhythm { get; set; }
        public string? OtherPain { get; set; }
        public DateTime StartDate { get; set; }
    }
}
