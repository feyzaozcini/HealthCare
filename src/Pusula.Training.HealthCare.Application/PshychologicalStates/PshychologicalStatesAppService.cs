using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class PshychologicalStatesAppService(IPshychologicalStateRepository pshychologicalStateRepository,
        PshychologicalStateManager pshychologicalStateManager) : HealthCareAppService, IPshychologicalStatesAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<PshychologicalStateDto> CreateAsync(PshychologicalStateCreateDto input)
        {
            var pshychologicalState = await pshychologicalStateManager.CreateAsync(input.MentalState, input.ProtocolId, input.Description);

            return ObjectMapper.Map<PshychologicalState, PshychologicalStateDto>(pshychologicalState);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]

        public async Task DeleteAsync(Guid id) => await pshychologicalStateRepository.DeleteAsync(id);


        public async Task<PshychologicalStateDto> GetAsync(Guid id) => ObjectMapper.Map<PshychologicalState, PshychologicalStateDto>(
                await pshychologicalStateRepository.GetAsync(id));
        public async Task<PshychologicalStateWithNavigationDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var pshychologicalState = await pshychologicalStateRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<PshychologicalStateWithNavigationProperties, PshychologicalStateWithNavigationDto>(pshychologicalState);
        }

        public async Task<PshychologicalStateDto> GetWithProtocolIdAsync(Guid protocolId)
        {
            var pshychologicalState = await pshychologicalStateRepository.GetAsync(a => a.ProtocolId == protocolId);


            var pshychologicalStateDto = ObjectMapper.Map<PshychologicalState, PshychologicalStateDto>(pshychologicalState);

            return pshychologicalStateDto;
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<PshychologicalStateDto> UpdateAsync(PshychologicalStateUpdateDto input) => ObjectMapper.Map<PshychologicalState, PshychologicalStateDto>(
                await pshychologicalStateManager.UpdateAsync(input.Id, input.MentalState, input.ProtocolId, input.Description));
    }
}
