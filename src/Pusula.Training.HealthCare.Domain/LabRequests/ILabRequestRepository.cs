using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.LabRequests;

public interface ILabRequestRepository : IRepository<LabRequest, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        Guid? testGroupItemId = null,
        string? name = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        CancellationToken cancellationToken = default
        );

    Task<List<LabRequest>> GetListAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        Guid? testGroupItemId = null,
        string? name = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        );

    Task<long> GetCountAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        Guid? testGroupItemId = null,
        string? name = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        CancellationToken cancellationToken = default
        );
}
