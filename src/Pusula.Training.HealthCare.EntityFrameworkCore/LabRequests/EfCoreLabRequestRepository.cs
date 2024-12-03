using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestProcesses;
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
        string? doctorName = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null,
        CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, protocolId, doctorId, doctorName, date, status, description);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        string? doctorName = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null,
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, protocolId, doctorId, doctorName, date, status, description);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<LabRequest>> GetListAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
                string? doctorName = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, protocolId, doctorId, doctorName, date, status, description);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LabRequestConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<List<LabRequest>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        string? doctorName = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, protocolId, doctorId, doctorName, date, status, description);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LabRequestConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<LabRequest> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        var labRequest = await query.FirstOrDefaultAsync(lr => lr.Id == id, cancellationToken);
        HealthCareException.ThrowIf(labRequest == null);
        return labRequest!;
    }

    protected virtual async Task<IQueryable<LabRequest>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(lr => lr.Protocol)
            .Include(lr => lr.Doctor)
                .ThenInclude(lr => lr.User);
    }

    protected virtual IQueryable<LabRequest> ApplyFilter(
    IQueryable<LabRequest> query,
        string? filterText = null,
        Guid? protocolId = null,
        Guid? doctorId = null,
        string? doctorName = null,
        DateTime? date = null,
        RequestStatusEnum? status = null,
        string? description = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                (e.ProtocolId != Guid.Empty && e.ProtocolId.ToString().Contains(filterText!)) ||
                (e.DoctorId != Guid.Empty && e.DoctorId.ToString().Contains(filterText!)) ||
                (e.Description!.Contains(filterText!)) ||
                (e.Doctor.User.Name!.Contains(filterText!))
            )
            .WhereIf(protocolId.HasValue && protocolId != Guid.Empty, e => e.ProtocolId == protocolId!.Value)
            .WhereIf(doctorId.HasValue && doctorId != Guid.Empty, e => e.DoctorId == doctorId!.Value)
            .WhereIf(date.HasValue, e => e.Date.Date == date!.Value.Date)
            .WhereIf(status.HasValue, e => e.Status == status!.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description != null && e.Description.Contains(description!))
            .WhereIf(!string.IsNullOrWhiteSpace(doctorName), e => e.Doctor.User.Name != null && e.Doctor.User.Name.Contains(doctorName!));
    }


}