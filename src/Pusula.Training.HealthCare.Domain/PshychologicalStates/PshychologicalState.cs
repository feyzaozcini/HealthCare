using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalState : Entity<Guid>, ISoftDelete
    {
        public string Description { get; set; }
        public State State { get; set; }

        public Guid ProtocolId { get; set; }
        public bool IsDeleted { get; set; }

        protected PshychologicalState()
        {
            Description = string.Empty;
        }

        public PshychologicalState(Guid id,State state,Guid protocolId,string description)
        {
            Id = id;
            State = state;
            ProtocolId = protocolId;
            Description = description;
        }
    }
}
