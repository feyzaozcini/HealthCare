﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestGroups;

public class EfCoreTestGroupRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, TestGroup, Guid>(dbContextProvider), ITestGroupRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null, 
        string? name = null, 
        CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, name);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        string? name = null, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<TestGroup>> GetListAsync(
        string? filterText = null, 
        string? name = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestGroupConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual IQueryable<TestGroup> ApplyFilter(
    IQueryable<TestGroup> query,
    string? filterText = null,
    string? name = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
               (e.Name != null && e.Name.Contains(filterText!))
            )
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
    }
}
