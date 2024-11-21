using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class EfCorePshychologicalStateRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, PshychologicalState, Guid>(dbContextProvider), IPshychologicalStateRepository
    {
        public async Task<PshychologicalStateWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(phychologicalState => new PshychologicalStateWithNavigationProperties
                {
                    PhychologicalState = phychologicalState,
                    Protocol = dbContext.Set<Protocol>().FirstOrDefault(c => c.Id == phychologicalState.ProtocolId)!
                })
                .FirstOrDefault()!;
        }

        protected virtual async Task<IQueryable<PshychologicalStateWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
           from pshychologicalState in (await GetDbSetAsync())
           join protocol in (await GetDbContextAsync()).Set<Protocol>() on pshychologicalState.ProtocolId equals protocol.Id into protocols
           from protocol in protocols.DefaultIfEmpty()
           select new PshychologicalStateWithNavigationProperties
           {
               PhychologicalState = pshychologicalState,
               Protocol = protocol
           };
    }
}
