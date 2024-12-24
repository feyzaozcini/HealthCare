using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class EfCoreControlNoteRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, ControlNote, Guid>(dbContextProvider), IControlNoteRepository
    {
        public async Task<long> GetCountAsync(string? filterText = null, Guid? protocolId = null, DateTime? noteDate = null,
            string? note = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, protocolId, noteDate, note);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<ControlNote>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? protocolId = null, 
            DateTime? noteDate = null, string? note = null, string? sorting = null, int maxResultCount = int.MaxValue, 
            int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, protocolId, noteDate, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ControlNoteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<ControlNote> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var controlNote = await query.FirstOrDefaultAsync(x => x.ProtocolId == protocolId, cancellationToken);
            return controlNote!;
        }

        protected virtual async Task<IQueryable<ControlNote>> GetQueryForNavigationPropertiesAsync()
        {
            var dbSet = await GetDbSetAsync();
            return dbSet
                .Include(x => x.User)
                .Include(x => x.Protocol)
                    .ThenInclude(x => x.Doctor)
                        .ThenInclude(x => x.Title)
                 .Where(controlNote =>controlNote.CreatorId == controlNote.Protocol.Doctor.UserId || controlNote.User != null);

        }

        protected virtual IQueryable<ControlNote> ApplyFilter(
        IQueryable<ControlNote> query,
        string? filterText = null,
        Guid? protocolId = null,
        DateTime? noteDate = null,
        string? note = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>(e.Note != null && e.Note.Contains(filterText!)))
                .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note != null && e.Note.Contains(note!))
                .WhereIf(noteDate.HasValue , e => e.NoteDate == noteDate!.Value)
                .WhereIf(protocolId.HasValue && protocolId != Guid.Empty, e => e.ProtocolId == protocolId!.Value);
              
        }

        public async Task<List<ControlNote>> GetListAsync(string? filterText = null, Guid? protocolId = null, 
            DateTime? noteDate = null, string? note = null, string? sorting = null, int maxResultCount = int.MaxValue, 
            int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, protocolId, noteDate, note);//GetQueryForNavigationPropertiesAsync ??
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ControlNoteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
    }
}
