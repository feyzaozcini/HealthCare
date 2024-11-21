using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisCreateDto
    {

        public string Complaint { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public string Story { get; set; } = null!;

        public Guid ProtocolId { get; set; }
    }
}
