using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.PshychologicalStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.FallRisks
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class FallRiskAppService(IFallRiskRepository fallRiskRepository,
        FallRiskManager fallRiskManager) : HealthCareAppService, IFallRisksAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<FallRiskDto> CreateAsync(FallRiskCreateDto input)
        {
            var fallRisk = await fallRiskManager.CreateAsync(input.Score,input.ProtocolId,input.Description,input.HasFallHistory,input.UsesMedications,input.HasAddiction,input.HasBalanceDisorder,input.HasVisionImpairment,input.MentalState,input.GeneralHealthState);

            return ObjectMapper.Map<FallRisk, FallRiskDto>(fallRisk);
        }


        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await fallRiskRepository.DeleteAsync(id);

        public async Task<FallRiskDto> GetAsync(Guid id) => ObjectMapper.Map<FallRisk, FallRiskDto>(
                await fallRiskRepository.GetAsync(id));

        public async Task<FallRiskWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var fallRisk = await fallRiskRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<FallRiskWithNavigationProperties, FallRiskWithNavigationPropertiesDto>(fallRisk);
        }

        public async Task<FallRiskDto> GetWithProtocolIdAsync(Guid protocolId)
        {
            var fallRisk = await fallRiskRepository.GetAsync(a => a.ProtocolId == protocolId);

          
            var fallRiskDto = ObjectMapper.Map<FallRisk, FallRiskDto>(fallRisk);

            return fallRiskDto;
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<FallRiskDto> UpdateAsync(FallRiskUpdateDto input) => ObjectMapper.Map<FallRisk, FallRiskDto>(
                await fallRiskManager.UpdateAsync(input.Id, input.Score, input.ProtocolId, input.Description, input.HasFallHistory, input.UsesMedications, input.HasAddiction, input.HasBalanceDisorder, input.HasVisionImpairment, input.MentalState, input.GeneralHealthState));
    }
}
