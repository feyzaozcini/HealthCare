using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisWithNavigationPropertiesDto
    {
        public AnamnesisDto Anamnesis { get; set; } = null!;

        public ProtocolDto Protocol { get; set; } = null!;
    }
}
