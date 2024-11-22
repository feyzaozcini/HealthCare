using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class EfCoreDiagnosisGroupRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, DiagnosisGroup, Guid>(dbContextProvider), IDiagnosisGroupRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, string? name = null, string? code = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyFilter(query, filterText, name, code);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string? filterText = null, string? name = null, string? code = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name,code);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<DiagnosisGroup>> GetListAsync(string? filterText = null, string? name = null, string? code = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DiagnosisGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<DiagnosisGroup> ApplyFilter(
           IQueryable<DiagnosisGroup> query,
           string? filterText = null,
           string? name = null,
           string? code = null) =>
               query
                   .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                   .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                   .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code!));



    }
}
