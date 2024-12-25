using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalState : Entity<Guid>, ISoftDelete
    {
        public string Description { get; private set; } = string.Empty;
        public MentalState MentalState { get; private set; }

        public Guid ProtocolId { get; private set; }
        public bool IsDeleted { get; set; }

        protected PshychologicalState()
        {
            Description = string.Empty;
        }

        public PshychologicalState(Guid id, MentalState mentalState, Guid protocolId, string description)
        {
            Id = id;
            SetMentalState(mentalState);
            SetProtocolId(protocolId);
            SetDescription(description);
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetMentalState(MentalState mentalState)
        {
            MentalState = mentalState;
        }

        public void SetProtocolId(Guid protocolId)
        {
            Check.NotNull(protocolId, nameof(protocolId));
            ProtocolId = protocolId;
        }
    }
}
