using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.LabRequests;

public class EfCoreLabRequestRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, LabRequest, Guid>(dbContextProvider), ILabRequestRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null, 
        Guid? protocolId = null, 
        Guid? doctorId = null, 
        Guid? testGroupItemId = null, 
        string? name = null, 
        DateTime? date = null, 
        RequestStatusEnum? status = null, 
        CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, name, protocolId, doctorId, testGroupItemId, date, status);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        Guid? protocolId = null, 
        Guid? doctorId = null, 
        Guid? testGroupItemId = null, 
        string? name = null, 
        DateTime? date = null, 
        RequestStatusEnum? status = null, 
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, protocolId, doctorId, testGroupItemId, date, status);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<LabRequest>> GetListAsync(
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
        )
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, protocolId, doctorId, testGroupItemId, date, status);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LabRequestConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual IQueryable<LabRequest> ApplyFilter(
    IQueryable<LabRequest> query,
    string? filterText = null,
    string? name = null,
    Guid? protocolId = null,
    Guid? doctorId = null,
    Guid? testGroupItemId = null,
    DateTime? date = null,
    RequestStatusEnum? status = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                (e.Name != null && e.Name.Contains(filterText!)) ||
                (e.ProtocolId != Guid.Empty && e.ProtocolId.ToString().Contains(filterText!)) ||
                (e.DoctorId != Guid.Empty && e.DoctorId.ToString().Contains(filterText!)) ||
                (e.TestGroupItemId != Guid.Empty && e.TestGroupItemId.ToString().Contains(filterText!)) ||
                (e.Date.ToString("yyyy-MM-dd").Contains(filterText!)) ||
                (e.Status.ToString().Contains(filterText!))
            )
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name != null && e.Name.Contains(name!))
            .WhereIf(protocolId.HasValue && protocolId != Guid.Empty, e => e.ProtocolId == protocolId!.Value)
            .WhereIf(doctorId.HasValue && doctorId != Guid.Empty, e => e.DoctorId == doctorId!.Value)
            .WhereIf(testGroupItemId.HasValue && testGroupItemId != Guid.Empty, e => e.TestGroupItemId == testGroupItemId!.Value)
            .WhereIf(date.HasValue, e => e.Date.Date == date!.Value.Date)
            .WhereIf(status.HasValue, e => e.Status == status!.Value);
    }


}