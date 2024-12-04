using System;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateUpdateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public MentalState MentalState { get; set; }
        public Guid ProtocolId { get; set; }
    }
}
