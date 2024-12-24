using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public class EfCoreFamilyHistoryRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, FamilyHistory, Guid>(dbContextProvider), IFamilyHistoryRepository
    {
        public async Task<FamilyHistory> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();
            return await queryable.FirstOrDefaultAsync(sc => sc.PatientId == patientId, cancellationToken);
        }
    }
}
