using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestProcesses;

public interface ITestProcessRepository : IRepository<TestProcess, Guid>
{
    Task<List<TestProcess>> GetListAsync(
               string? filterText = null,
               Guid? labRequestId = null,
               Guid? testGroupItemId = null,
               TestProcessStates? status = null,
               decimal? result = null,
               DateTime? resultDate = null,
               string? sorting = null,
               int maxResultCount = int.MaxValue,
               int skipCount = 0,
               CancellationToken cancellationToken = default
           );

    Task<long> GetCountAsync(
        string? filterText = null,
        Guid? labRequestId = null,
        Guid? testGroupItemId = null,
        TestProcessStates? status = null,
        decimal? result = null,
        DateTime? resultDate = null,
        CancellationToken cancellationToken = default);
}
