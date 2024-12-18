using Pusula.Training.HealthCare.SystemChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public interface IFollowUpPlanRepository : IRepository<FollowUpPlan, Guid>
    {
        Task<FollowUpPlan> GetWithNavigationPropertiesAsync(Guid id,CancellationToken cancellationToken = default);

        Task<FollowUpPlan> GetByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default);
    }
}
