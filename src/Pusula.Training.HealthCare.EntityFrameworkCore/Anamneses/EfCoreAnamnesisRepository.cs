using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class EfCoreAnamnesisRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Anamnesis, Guid>(dbContextProvider), IAnamnesisRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, string? complaint = null, DateTime? startDate = null, string? story = null, Guid? protocolId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, story, complaint, protocolId);

            var ids = query.Select(x => x.Anamnesis.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string? filterText = null, string? complaint = null, DateTime? startDate = null, string? story = null,  Guid? protocolId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, story, complaint, protocolId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Anamnesis>> GetListAsync(string? filterText = null, string? complaint = null, DateTime? startDate = null, string? story = null, Guid? protocolId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, story, complaint, protocolId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DiagnosisConsts.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }


        public async Task<AnamnesisWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(anamnesis => new AnamnesisWithNavigationProperties
                {
                    Anamnesis = anamnesis,
                    Protocol = dbContext.Set<Protocol>().FirstOrDefault(c => c.Id == anamnesis.ProtocolId)!
                })
                .FirstOrDefault()!;
        }

        protected virtual async Task<IQueryable<AnamnesisWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
            from anamnesis in (await GetDbSetAsync())
            join protocol in (await GetDbContextAsync()).Set<Protocol>() on anamnesis.ProtocolId equals protocol.Id into protocols
            from protocol in protocols.DefaultIfEmpty()
            select new AnamnesisWithNavigationProperties
            {
                Anamnesis = anamnesis,
                Protocol = protocol
            };

        protected virtual IQueryable<Anamnesis> ApplyFilter(
           IQueryable<Anamnesis> query,
           string? filterText = null,
           string? story = null,
           string? complaint = null,
           Guid? protocolId = null) =>
               query
                   .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Story.StartsWith(filterText!) || e.Complaint.StartsWith(filterText!))
                   .WhereIf(!string.IsNullOrWhiteSpace(complaint), e => e.Complaint.StartsWith(complaint!))
                   .WhereIf(!string.IsNullOrWhiteSpace(story), e => e.Story.StartsWith(story!))
                   .WhereIf(protocolId.HasValue, e => e.ProtocolId == protocolId);


        protected virtual IQueryable<AnamnesisWithNavigationProperties> ApplyFilter(
         IQueryable<AnamnesisWithNavigationProperties> query,
         string? filterText = null,
         string? story = null,
         string? complaint = null,
         Guid? protocolId = null) =>
             query
                 .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Anamnesis.Story.StartsWith(filterText!) || e.Anamnesis.Complaint.StartsWith(filterText!))
                 .WhereIf(!string.IsNullOrWhiteSpace(story), e => e.Anamnesis.Story.StartsWith(story!))
                 .WhereIf(!string.IsNullOrWhiteSpace(complaint), e => e.Anamnesis.Complaint.StartsWith(complaint!))
                 .WhereIf(protocolId.HasValue, e => e.Anamnesis.ProtocolId == protocolId);

    }
}
