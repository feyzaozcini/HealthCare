using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateManager(IPshychologicalStateRepository pshychologicalStateRepository) : DomainService
    {
        public virtual async Task<PshychologicalState> CreateAsync(MentalState mentalState, Guid protocolId, string description)
        {

            var pshychologicalState = new PshychologicalState(Guid.NewGuid(), mentalState, protocolId, description);
            return await pshychologicalStateRepository.InsertAsync(pshychologicalState);
        }

        public virtual async Task<PshychologicalState> UpdateAsync(Guid id, MentalState mentalState, Guid protocolId, string description)
        {

            var pshychologicalState = await pshychologicalStateRepository.GetAsync(id);

            pshychologicalState.SetMentalState(mentalState);
            pshychologicalState.SetProtocolId(protocolId);
            pshychologicalState.SetDescription(description);

            return await pshychologicalStateRepository.UpdateAsync(pshychologicalState);
        }
    }
}
