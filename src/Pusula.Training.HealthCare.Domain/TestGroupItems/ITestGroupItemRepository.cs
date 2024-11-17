using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestGroupItems;

public interface ITestGroupItemRepository : IRepository<TestGroupItem, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        Guid? testGroupId = null,
        string? name = null,
        string? code = null,
        string? testType = null,
        string? description = null,
        int? turnaroundTime = null,
        CancellationToken cancellationToken = default);

    Task<List<TestGroupItem>> GetListAsync(
        string? filterText = null,
        Guid? testGroupId = null,
        string? name = null,
        string? code = null,
        string? testType = null,
        string? description = null,
        int? turnaroundTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
            );

    Task<long> GetCountAsync(
        string? filterText = null,
        Guid? testGroupId = null,
        string? name = null,
        string? code = null,
        string? testType = null,
        string? description = null,
        int? turnaroundTime = null,
        CancellationToken cancellationToken = default);
}