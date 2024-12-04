using System;
using System.Collections.Generic;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateCreateDto
    {
        public string Description { get; set; } = null!;
        public MentalState MentalState { get; set; }
        public Guid ProtocolId { get; set; }
    }
}
