using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.FallRisks
{
    public class EfCoreFallRiskRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, FallRisk, Guid>(dbContextProvider), IFallRiskRepository
    {
        public async Task<FallRiskWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(fallRisk => new FallRiskWithNavigationProperties
                {
                    FallRisk = fallRisk,
                    Protocol = dbContext.Set<Protocol>().FirstOrDefault(c => c.Id == fallRisk.ProtocolId)!
                })
                .FirstOrDefault()!;
        }

        protected virtual async Task<IQueryable<FallRiskWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
          from fallRisk in (await GetDbSetAsync())
          join protocol in (await GetDbContextAsync()).Set<Protocol>() on fallRisk.ProtocolId equals protocol.Id into protocols
          from protocol in protocols.DefaultIfEmpty()
          select new FallRiskWithNavigationProperties
          {
              FallRisk = fallRisk,
              Protocol = protocol
          };
    }
}
