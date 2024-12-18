using Pusula.Training.HealthCare.SystemChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public interface IFollowUpPlansAppService : IApplicationService
    {
        Task<FollowUpPlanDto> GetAsync(Guid id);

        Task<FollowUpPlanDto> GetByProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task<FollowUpPlanDto> CreateAsync(FollowUpPlanCreateDto input);
        Task<FollowUpPlanDto> UpdateAsync(FollowUpPlanUpdateDto input);

    }
}
