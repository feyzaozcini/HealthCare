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

namespace Pusula.Training.HealthCare.PainDetails
{
    public class EfCorePainDetailRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, PainDetail, Guid>(dbContextProvider), IPainDetailRepository
    {
        public async Task<PainDetail> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var painDetail = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            //HealthCareException.ThrowIf(painDetail == null);
            return painDetail!;
        }

        public async Task<PainDetail> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var painDetail = await query.FirstOrDefaultAsync(x => x.ProtocolId == protocolId, cancellationToken);
            //HealthCareException.ThrowIf(painDetail == null);
            return painDetail!;
        }

        protected virtual async Task<IQueryable<PainDetail>> GetQueryForNavigationPropertiesAsync()
        {
            var dbSet = await GetDbSetAsync();
            return dbSet
                .Include(tp => tp.Protocol)
                .Include(tp => tp.PainType);

        }
    }
}
