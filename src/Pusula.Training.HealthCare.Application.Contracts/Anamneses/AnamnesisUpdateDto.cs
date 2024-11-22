using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisUpdateDto
    {
        public Guid Id { get; set; } = default!;

        public string Complaint { get; set; } = null!;
        public DateTime StartDate { get; set; } 

        public string Story { get; set; } = null!;

        public Guid ProtocolId { get; set; }
    }
}
