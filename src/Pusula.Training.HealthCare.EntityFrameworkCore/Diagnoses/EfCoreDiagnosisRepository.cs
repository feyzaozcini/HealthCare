using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class EfCoreDiagnosisRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Diagnosis, Guid>(dbContextProvider), IDiagnosisRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, string? name = null, string? code = null, Guid? groupId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, groupId);

            var ids = query.Select(x => x.Diagnosis.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async  Task<long> GetCountAsync(string? filterText = null, string? name = null, string? code = null, Guid? groupId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, groupId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Diagnosis>> GetListAsync(string? filterText = null, string? name = null, string? code = null, Guid? groupId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, code, name, groupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DiagnosisConsts.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<List<DiagnosisWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? code = null, string? name = null, Guid? groupId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, groupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DiagnosisConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<DiagnosisWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(diagnosis => new DiagnosisWithNavigationProperties
                {
                    Diagnosis = diagnosis,
                    DiagnosisGroup = dbContext.Set<DiagnosisGroup>().FirstOrDefault(c => c.Id == diagnosis.GroupId)!
                })
                .FirstOrDefault()!;
        }

        protected virtual IQueryable<Diagnosis> ApplyFilter(
            IQueryable<Diagnosis> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            Guid? groupId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.StartsWith(filterText!) || e.Code.StartsWith(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.StartsWith(code!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.StartsWith(name!))
                    .WhereIf(groupId.HasValue, e => e.GroupId == groupId);

        protected virtual IQueryable<DiagnosisWithNavigationProperties> ApplyFilter(
            IQueryable<DiagnosisWithNavigationProperties> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            Guid? groupId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Diagnosis.Name.StartsWith(filterText!) || e.Diagnosis.Code.StartsWith(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Diagnosis.Code.StartsWith(code!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Diagnosis.Name.StartsWith(name!))
                    .WhereIf(groupId.HasValue, e => e.Diagnosis.GroupId == groupId);



        protected virtual async Task<IQueryable<DiagnosisWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
             from diagnosis in (await GetDbSetAsync())
             join diagnosisGroup in (await GetDbContextAsync()).Set<DiagnosisGroup>() on diagnosis.GroupId equals diagnosisGroup.Id into diagnosisGroups
             from diagnosisGroup in diagnosisGroups.DefaultIfEmpty()
             select new DiagnosisWithNavigationProperties
             {
                 Diagnosis = diagnosis,
                 DiagnosisGroup = diagnosisGroup
             };
    }
}
