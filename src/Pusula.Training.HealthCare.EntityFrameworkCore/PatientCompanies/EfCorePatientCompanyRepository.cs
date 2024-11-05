using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public class EfCorePatientCompanyRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, PatientCompany, Guid>(dbContextProvider), IPatientCompanyRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, 
            string? name = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyFilter(query,filterText,name);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string? filterText = null, 
            string? name = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<PatientCompany>> GetListAsync(string? filterText = null, 
            string? name = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientCompanyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<PatientCompany> ApplyFilter(
            IQueryable<PatientCompany> query,
            string? filterText = null,
            string? name = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText),x=>x.Name!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name!.Contains(name!));

        }
    }
}
