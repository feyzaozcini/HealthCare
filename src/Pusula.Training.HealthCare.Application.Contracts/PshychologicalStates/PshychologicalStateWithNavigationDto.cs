using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateWithNavigationDto
    {
        public PshychologicalStateDto PshychologicalState { get; set; } = null!;

        public ProtocolDto Protocol { get; set; } = null!;

    }
}
