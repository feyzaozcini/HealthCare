using Pusula.Training.HealthCare.Anamneses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateManager(IPshychologicalStateRepository pshychologicalStateRepository) : DomainService
    {
        public virtual async Task<PshychologicalState> CreateAsync(State state, Guid protocolId, string description)
        {

            var pshychologicalState = new PshychologicalState(Guid.NewGuid(),state,protocolId,description);
            return await pshychologicalStateRepository.InsertAsync(pshychologicalState);
        }

        public virtual async Task<PshychologicalState> UpdateAsync(Guid id, State state, Guid protocolId, string description)
        {

            var pshychologicalState = await pshychologicalStateRepository.GetAsync(id);

            pshychologicalState.State = state;
            pshychologicalState.ProtocolId = protocolId;
            pshychologicalState.Description = description;

            return await pshychologicalStateRepository.UpdateAsync(pshychologicalState);
        }
    }
}
