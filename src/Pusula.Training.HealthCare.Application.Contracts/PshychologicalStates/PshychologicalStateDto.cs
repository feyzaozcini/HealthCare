using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateDto : EntityDto<Guid>
    {
        public string Description { get; set; } = null!;
        public MentalState MentalState { get; set; }
        public Guid ProtocolId { get; set; }

    }
}
