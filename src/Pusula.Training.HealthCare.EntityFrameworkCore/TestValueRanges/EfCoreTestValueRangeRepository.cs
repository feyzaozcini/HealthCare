﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class EfCoreTestValueRangeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, TestValueRange, Guid>(dbContextProvider), ITestValueRangeRepository
{
    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        Guid? testGroupItemId = null, 
        decimal? minValue = null, 
        decimal? maxValue = null, 
        TestUnitTypes? unit = null, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, testGroupItemId, minValue, maxValue, unit);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));

    }

    public virtual async Task<List<TestValueRange>> GetListAsync(
        string? filterText = null, 
        Guid? testGroupItemId = null, 
        decimal? minValue = null, 
        decimal? maxValue = null, 
        TestUnitTypes? unit = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, testGroupItemId, minValue, maxValue, unit);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestValueRangeConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<List<TestValueRange>> GetTestValueRangesWithNavigationPropertiesAsync()
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        return await query.ToListAsync();
    }

    protected virtual async Task<IQueryable<TestValueRange>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(tvr => tvr.TestGroupItem); 
    }

    protected virtual IQueryable<TestValueRange> ApplyFilter(
        IQueryable<TestValueRange> query,
        string? filterText = null,
        Guid? testGroupItemId = null,
        decimal? minValue = null,
        decimal? maxValue = null,
        TestUnitTypes? unit = null
    )
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), tvr =>
                (tvr.MinValue.ToString().Contains(filterText!) ||
                tvr.MaxValue.ToString().Contains(filterText!) ||
                tvr.Unit.ToString().Contains(filterText!))
            )
            .WhereIf(testGroupItemId.HasValue && testGroupItemId != Guid.Empty, tvr => tvr.TestGroupItemId == testGroupItemId!.Value)
            .WhereIf(minValue.HasValue, tvr => tvr.MinValue == minValue!.Value)
            .WhereIf(maxValue.HasValue, tvr => tvr.MaxValue == maxValue!.Value)
            .WhereIf(unit.HasValue, tvr => tvr.Unit == unit!.Value);
    }

}
