﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestProcesses;

public class EfCoreTestProcessRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
: EfCoreRepository<HealthCareDbContext, TestProcess, Guid>(dbContextProvider), ITestProcessRepository
{
    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        Guid? labRequestId = null, 
        Guid? testGroupItemId = null, 
        TestProcessStates? status = null, 
        decimal? result = null, 
        DateTime? resultDate = null, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<TestProcess>> GetListAsync(
        string? filterText = null, 
        Guid? labRequestId = null, 
        Guid? testGroupItemId = null, 
        TestProcessStates? status = null, 
        decimal? result = null, 
        DateTime? resultDate = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, labRequestId, testGroupItemId, status, result, resultDate);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    //Bağlı olduğu entity'ler ile birlikte tüm verileri getirir.
    public async Task<List<TestProcess>> GetTestProcessesWithNavigationPropertiesAsync()
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        return await query.ToListAsync();
    }

    protected virtual async Task<IQueryable<TestProcess>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(tp => tp.LabRequest) 
            .Include(tp => tp.TestGroupItem);
    }

    protected virtual IQueryable<TestProcess> ApplyFilter(
    IQueryable<TestProcess> query,
    string? filterText = null,
    Guid? labRequestId = null,
    Guid? testGroupItemId = null,
    TestProcessStates? status = null,
    decimal? result = null,
    DateTime? resultDate = null
)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                (e.Status.ToString().Contains(filterText!)) ||
                (e.Result.HasValue && e.Result.ToString()!.Contains(filterText!)) ||
                (e.ResultDate.HasValue && e.ResultDate.ToString()!.Contains(filterText!))
            )
            .WhereIf(labRequestId.HasValue && labRequestId != Guid.Empty, e => e.LabRequestId == labRequestId.Value)
            .WhereIf(testGroupItemId.HasValue && testGroupItemId != Guid.Empty, e => e.TestGroupItemId == testGroupItemId.Value)
            .WhereIf(status.HasValue, e => e.Status == status!.Value)
            .WhereIf(result.HasValue, e => e.Result == result!.Value)
            .WhereIf(resultDate.HasValue, e => e.ResultDate == resultDate!.Value);
    }


}
