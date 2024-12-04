using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalState : Entity<Guid>, ISoftDelete
    {
        public string Description { get; set; }
        public MentalState MentalState { get; set; }

        public Guid ProtocolId { get; set; }
        public bool IsDeleted { get; set; }

        protected PshychologicalState()
        {
            Description = string.Empty;
        }

        public PshychologicalState(Guid id, MentalState mentalState, Guid protocolId, string description)
        {
            Id = id;
            MentalState = mentalState;
            ProtocolId = protocolId;
            Description = description;
        }
    }
}
