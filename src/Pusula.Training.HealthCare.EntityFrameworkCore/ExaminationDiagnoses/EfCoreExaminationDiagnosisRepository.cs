using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.EntityFrameworkCore;

using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class EfCoreExaminationDiagnosisRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, ExaminationDiagnosis, Guid>(dbContextProvider), IExaminationDiagnosisRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, DateTime? initialDate = null, string? note = null, Guid? protocolId = null, Guid? diagnosisId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, initialDate,note,protocolId,diagnosisId);
            var ids = query.Select(x => x.ExaminationDiagnosis.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }
        public async Task<long> GetCountAsync(string? filterText = null, DateTime? initialDate = null, string? note = null, Guid? protocolId = null, Guid? diagnosisId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, initialDate, note, protocolId, diagnosisId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<ExaminationDiagnosis>> GetListAsync(string? filterText = null, DateTime? initialDate = null, string? note = null, Guid? protocolId = null, Guid? diagnosisId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText,initialDate,note,protocolId,diagnosisId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExaminationDiagnosisConsts.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<List<ExaminationDiagnosisWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, DateTime? initialDate = null, string? note = null, Guid? protocolId = null, Guid? diagnosisId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,initialDate,note,protocolId,diagnosisId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExaminationDiagnosisConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<ExaminationDiagnosisWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(examinationDiagnosis => new ExaminationDiagnosisWithNavigationProperties
                {
                    ExaminationDiagnosis = examinationDiagnosis,
                    Protocol = dbContext.Set<Protocol>().FirstOrDefault(c => c.Id == examinationDiagnosis.ProtocolId)!,
                    Diagnosis = dbContext.Set<Diagnosis>().FirstOrDefault(c => c.Id == examinationDiagnosis.DiagnosisId)!
                })
                .FirstOrDefault()!;
        }


        protected virtual IQueryable<ExaminationDiagnosis> ApplyFilter(
           IQueryable<ExaminationDiagnosis> query,
           string? filterText = null,
           DateTime? initialDate = null,
           string? note = null,
           Guid? protocolId = null,
           Guid? diagnosisId = null) =>
               query
                   .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Note.StartsWith(filterText!))
                   .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.StartsWith(note!))
                   .WhereIf(initialDate.HasValue, e => e.InitialDate == initialDate)
                   .WhereIf(protocolId.HasValue, e => e.ProtocolId == protocolId)
                   .WhereIf(diagnosisId.HasValue, e => e.DiagnosisId == diagnosisId);


        protected virtual async Task<IQueryable<ExaminationDiagnosisWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
           from examinationDiagnosis in (await GetDbSetAsync())
           join diagnosis in (await GetDbContextAsync()).Set<Diagnosis>() on examinationDiagnosis.DiagnosisId equals diagnosis.Id into diagnoses
           from diagnosis in diagnoses.DefaultIfEmpty()
           join protocol in (await GetDbContextAsync()).Set<Protocol>() on examinationDiagnosis.ProtocolId equals protocol.Id into protocols
           from protocol in protocols.DefaultIfEmpty()
           select new ExaminationDiagnosisWithNavigationProperties
           {
               ExaminationDiagnosis = examinationDiagnosis,
               Protocol = protocol,
               Diagnosis = diagnosis,
              
           };


        protected virtual IQueryable<ExaminationDiagnosisWithNavigationProperties> ApplyFilter(
          IQueryable<ExaminationDiagnosisWithNavigationProperties> query,
          string? filterText = null,
          DateTime? initialDate = null,
          string? note = null,
          Guid? protocolId = null,
          Guid? diagnosisId = null) =>
              query
                  .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ExaminationDiagnosis.Note.StartsWith(filterText!))
                  .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.ExaminationDiagnosis.Note.StartsWith(note!))
                  .WhereIf(initialDate.HasValue, e => e.ExaminationDiagnosis.InitialDate == initialDate)
                  .WhereIf(protocolId.HasValue, e => e.ExaminationDiagnosis.ProtocolId == protocolId)
                  .WhereIf(diagnosisId.HasValue, e => e.ExaminationDiagnosis.DiagnosisId == diagnosisId);


        public async Task<List<(string DiagnosisName, int Count)>> GetDiagnosisCountsAsync(int skipCount,int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var query = from ed in dbContext.Set<ExaminationDiagnosis>()
                        join d in dbContext.Set<Diagnosis>() on ed.DiagnosisId equals d.Id
                        where !ed.IsDeleted // Soft delete olanları dışla
                        group ed by d.Name into g
                        orderby g.Key // İsteğe bağlı sıralama
                        select new
                        {
                            DiagnosisName = g.Key,
                            Count = g.Count()
                        };

            var result = await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(cancellationToken);

            return result.Select(x => (x.DiagnosisName, x.Count)).ToList();
        }

    }
}
