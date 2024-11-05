using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Countries;

public class EfCoreCountryRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Country, Guid>(dbContextProvider), ICountryRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null, 
        string? name = null, 
        string? code = null, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, name, code);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null, 
        string? name = null, 
        string? code = null, 
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, code);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Country>> GetListAsync(
        string? filterText = null, 
        string? name = null, 
        string? code = null, 
        string? sorting = null, 
        int maxResultCount = int.MaxValue, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, code);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CountryConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }


    protected virtual IQueryable<Country> ApplyFilter(
        IQueryable<Country> query,
        string? filterText = null,
        string? name = null,
        string? code = null)
    {
        return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Code!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code!));
    }
}
