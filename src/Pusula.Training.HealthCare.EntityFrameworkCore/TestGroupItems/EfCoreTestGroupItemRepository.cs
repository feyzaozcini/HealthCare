using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class EfCoreTestGroupItemRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, TestGroupItem, Guid>(dbContextProvider), ITestGroupItemRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        Guid? testGroupId = null,
        string? name = null,
        string? code = null,
        string? testType = null,
        string? description = null,
        int? turnaroundTime = null,
        CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, name, code, testType, description, turnaroundTime, testGroupId);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        Guid? testGroupId = null,
        string? name = null,
        string? code = null,
        string? testType = null,
        string? description = null,
        int? turnaroundTime = null,
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, code, testType, description, turnaroundTime, testGroupId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<TestGroupItem>> GetListAsync(
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
        )
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, code, testType, description, turnaroundTime, testGroupId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestGroupItemConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<TestGroupItem> GetWithNavigationPropertiesAsync(
     Guid id,
     CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        var testGroupItem = await query.FirstOrDefaultAsync(tg => tg.Id == id, cancellationToken);
        HealthCareException.ThrowIf(testGroupItem == null);
        return testGroupItem!;
    }

    protected virtual async Task<IQueryable<TestGroupItem>> GetQueryForNavigationPropertiesAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet
            .Include(tp => tp.TestGroup)
            .Include(tp => tp.TestValueRange);
    }

    public async Task<List<TestGroupItem>> GetListWithNavigationPropertiesAsync(
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
    CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, name, code, testType, description, turnaroundTime, testGroupId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestGroupItemConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual IQueryable<TestGroupItem> ApplyFilter(
    IQueryable<TestGroupItem> query,
    string? filterText = null,
    string? name = null,
    string? code = null,
    string? testType = null,
    string? description = null,
    int? turnaroundTime = null,
    Guid? testGroupId = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                (e.Name != null && e.Name.Contains(filterText!)) ||
                (e.Code != null && e.Code.Contains(filterText!)) ||
                (e.TestType != null && e.TestType.Contains(filterText!)) ||
                (e.Description != null && e.Description.Contains(filterText!))
            )
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name != null && e.Name.Contains(name!))
            .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code != null && e.Code.Contains(code!))
            .WhereIf(!string.IsNullOrWhiteSpace(testType), e => e.TestType != null && e.TestType.Contains(testType!))
            .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description != null && e.Description.Contains(description!))
            .WhereIf(turnaroundTime.HasValue, e => e.TurnaroundTime == turnaroundTime!.Value)
            .WhereIf(testGroupId.HasValue && testGroupId != Guid.Empty, e => e.TestGroupId == testGroupId!.Value);
    }
}
