using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
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

    public virtual async Task<TestGroupItemWithNavigationProperties> GetWithNavigationPropertiesAsync(
     Guid id,
     CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync()).Where(b => b.Id == id)
            .Select(testGroupItem => new TestGroupItemWithNavigationProperties
            {
                TestGroupItem = testGroupItem,
                TestGroup = dbContext.Set<TestGroup>().FirstOrDefault(c => c.Id == testGroupItem.TestGroupId)!,
            })
            .FirstOrDefault()!;
    }

    public async Task<List<TestGroupItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
        query = ApplyFilter(query, filterText, name, testGroupId, code, testType, description, turnaroundTime);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestGroupItemConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }


    protected virtual async Task<IQueryable<TestGroupItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
    from testGroupItem in (await GetDbSetAsync())
    join testGroup in (await GetDbContextAsync()).Set<TestGroup>() on testGroupItem.TestGroupId equals testGroup.Id into testGroups
    from testGroup in testGroups.DefaultIfEmpty()
    select new TestGroupItemWithNavigationProperties
    {
        TestGroupItem = testGroupItem,
        TestGroup = testGroup
    };

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

    private IQueryable<TestGroupItemWithNavigationProperties> ApplyFilter(
    IQueryable<TestGroupItemWithNavigationProperties> query,
    string? filterText,
    string? name,
    Guid? testGroupId,
    string? code,
    string? testType,
    string? description,
    int? turnaroundTime)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                x => x.TestGroup.Name.Contains(filterText!) ||
                     x.TestGroupItem.Name.Contains(filterText!) ||
                     x.TestGroupItem.Code.Contains(filterText!) ||
                     x.TestGroupItem.Description.Contains(filterText!) ||
                     x.TestGroupItem.TestType.Contains(filterText!))
            .WhereIf(testGroupId.HasValue, x => x.TestGroup.Id == testGroupId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.TestGroupItem.Name.Contains(name!))
            .WhereIf(!string.IsNullOrWhiteSpace(code), x => x.TestGroupItem.Code.Contains(code!))
            .WhereIf(!string.IsNullOrWhiteSpace(testType), x => x.TestGroupItem.TestType.Contains(testType!))
            .WhereIf(!string.IsNullOrWhiteSpace(description), x => x.TestGroupItem.Description.Contains(description!))
            .WhereIf(turnaroundTime.HasValue, x => x.TestGroupItem.TurnaroundTime == turnaroundTime);
    }



}
