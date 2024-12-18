using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.SystemChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class FollowUpPlansAppService(IFollowUpPlanRepository followUpPlanRepository,
        FollowUpPlanManager followUpPlanManager)
    : HealthCareAppService, IFollowUpPlansAppService
    {
        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<FollowUpPlanDto> CreateAsync(FollowUpPlanCreateDto input)
        {
            var followUpPlan = await followUpPlanManager.CreateAsync(input.ProtocolId, input.Note,
                input.FollowUpType, input.IsSurgeryScheduled);

            return ObjectMapper.Map<FollowUpPlan, FollowUpPlanDto>(followUpPlan);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await followUpPlanRepository.DeleteAsync(id);

        public async Task<FollowUpPlanDto> GetAsync(Guid id) =>
            ObjectMapper.Map<FollowUpPlan, FollowUpPlanDto>(await followUpPlanRepository.GetAsync(id));

        public async Task<FollowUpPlanDto> GetByProtocolIdAsync(Guid protocolId)
        {
            var followUpPlan = await followUpPlanRepository.GetByProtocolIdAsync(protocolId);

            // Eğer kayıt yoksa doğrudan null dönecek
            return followUpPlan != null
                ? ObjectMapper.Map<FollowUpPlan, FollowUpPlanDto>(followUpPlan)
                : null;
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<FollowUpPlanDto> UpdateAsync(FollowUpPlanUpdateDto input) => 
            ObjectMapper.Map<FollowUpPlan, FollowUpPlanDto>( await followUpPlanManager.UpdateAsync(input.Id, input.ProtocolId, 
                input.Note, input.FollowUpType, input.IsSurgeryScheduled));
    }
}
