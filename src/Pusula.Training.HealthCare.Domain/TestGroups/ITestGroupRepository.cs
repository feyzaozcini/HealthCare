using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestGroups;

public interface ITestGroupRepository : IRepository<TestGroup, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        CancellationToken cancellationToken = default);

    Task<List<TestGroup>> GetListAsync(
                string? filterText = null,
                string? name = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        CancellationToken cancellationToken = default);
}