using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlanManager(IFollowUpPlanRepository followUpPlanRepository) : DomainService
    {
        public virtual async Task<FollowUpPlan> CreateAsync(Guid protocolId,string? note,
            FollowUpType followUpType, bool isSurgeryScheduled)
        {

            var followUpPlan = new FollowUpPlan( GuidGenerator.Create(), protocolId,note,followUpType,isSurgeryScheduled);

            return await followUpPlanRepository.InsertAsync(followUpPlan);
        }
        public virtual async Task<FollowUpPlan> UpdateAsync(Guid id, Guid protocolId, string? note,
           FollowUpType followUpType, bool isSurgeryScheduled)
        {

            var followUpPlan = await followUpPlanRepository.GetAsync(id);
            followUpPlan.SetProtocolId(protocolId);
            followUpPlan.SetNote(note);
            followUpPlan.SetFollowUpType(followUpType);
            followUpPlan.SetIsSurgeryScheduled(isSurgeryScheduled);
         
            return await followUpPlanRepository.UpdateAsync(followUpPlan);

        }
    }

   
}
