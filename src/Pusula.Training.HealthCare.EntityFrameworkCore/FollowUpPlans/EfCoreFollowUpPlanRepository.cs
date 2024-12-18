using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class EfCoreFollowUpPlanRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, FollowUpPlan, Guid>(dbContextProvider), IFollowUpPlanRepository
    {
        public async Task<FollowUpPlan> GetByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync(); // Asenkron sorguyu bir değişkende sakla
            return await queryable.FirstOrDefaultAsync(sc => sc.ProtocolId == protocolId, cancellationToken);
        }

        public async Task<FollowUpPlan> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var followUpPlan = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
 
            return followUpPlan!;
        }

        protected virtual async Task<IQueryable<FollowUpPlan>> GetQueryForNavigationPropertiesAsync()
        {
            var dbSet = await GetDbSetAsync();
            return dbSet
                .Include(tp => tp.Protocol);
                
        }
    }
}
