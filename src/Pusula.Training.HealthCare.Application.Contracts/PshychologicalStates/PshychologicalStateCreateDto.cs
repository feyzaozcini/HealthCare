using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateCreateDto
    {
        public string Description { get; set; } = null!;
        public State State { get; set; }
        public Guid ProtocolId { get; set; }
    }
}
