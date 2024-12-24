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

namespace Pusula.Training.HealthCare.PatientHistories
{
    public class EfCorePatientHistoryRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, PatientHistory, Guid>(dbContextProvider), IPatientHistoryRepository
    {
        public async Task<PatientHistory> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.PatientId == patientId, cancellationToken);
        }
    }
}
