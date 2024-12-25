using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
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

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public class EfCorePhysicalExaminationRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, PhysicalExamination, Guid>(dbContextProvider), IPhysicalExaminationRepository
    {

        public async Task<PhysicalExaminationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(physicalExamination => new PhysicalExaminationWithNavigationProperties
                {
                  PhysicalExamination = physicalExamination,
                    Protocol = dbContext.Set<Protocol>().FirstOrDefault(c => c.Id == physicalExamination.ProtocolId)!
                })
                .FirstOrDefault()!;
        }

        protected virtual async Task<IQueryable<PhysicalExaminationWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
       from physicalExamination in (await GetDbSetAsync())
       join protocol in (await GetDbContextAsync()).Set<Protocol>() on physicalExamination.ProtocolId equals protocol.Id into protocols
       from protocol in protocols.DefaultIfEmpty()
       select new PhysicalExaminationWithNavigationProperties
       {
          PhysicalExamination=physicalExamination,
          Protocol=protocol,
       };
    }
}
