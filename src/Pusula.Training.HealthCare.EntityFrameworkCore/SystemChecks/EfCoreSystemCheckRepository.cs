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

namespace Pusula.Training.HealthCare.SystemChecks
{
    public class EfCoreSystemCheckRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, SystemCheck, Guid>(dbContextProvider), ISystemCheckRepository
    {
        public async Task<SystemCheck> GetByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync(); 
            return await queryable.FirstOrDefaultAsync(sc => sc.ProtocolId == protocolId, cancellationToken);
         
        }

        public async Task<SystemCheck> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var systemCheck = await query.FirstOrDefaultAsync(sc => sc.Id == id, cancellationToken);
           
            return systemCheck;
        }

        public async Task<SystemCheck> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var systemCheck = await query.FirstOrDefaultAsync(sc => sc.ProtocolId == protocolId, cancellationToken);

            return systemCheck;
        }

        protected virtual async Task<IQueryable<SystemCheck>> GetQueryForNavigationPropertiesAsync()
        {
            var dbSet = await GetDbSetAsync();
            return dbSet
                .Include(tp => tp.Protocol);
        }
    }
}
