using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class EfCoreBlackListRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, BlackList, Guid>(dbContextProvider), IBlackListRepository
    {
        public virtual async Task DeleteAllAsync(
            string? filterText = null, 
            BlackListStatus? blackListStatus = null, 
            string? note = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, blackListStatus, note, patientId, doctorId);

            var ids = query.Select(x => x.BlackList.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            BlackListStatus? blackListStatus = null, 
            string? note = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, blackListStatus, note, patientId, doctorId);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<BlackList>> GetListAsync(
            string? filterText = null, 
            BlackListStatus? blackListStatus = null, 
            string? note = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, blackListStatus, note, patientId, doctorId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BlackListConst.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<BlackListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            BlackListStatus? blackListStatus = null, 
            string? note = null, 
            Guid? patientId = null, 
            Guid? doctorId = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, blackListStatus, note, patientId, doctorId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BlackListConst.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<BlackListWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var blackList = await query.FirstOrDefaultAsync(lr => lr.BlackList.Id == id, cancellationToken);
            HealthCareException.ThrowIf(blackList == null);
            return blackList!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<BlackList> ApplyFilter(
            IQueryable<BlackList> query,
            string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PatientId.ToString()!.Contains(filterText!) || e.DoctorId.ToString()!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note!.Contains(note!))
                    .WhereIf(blackListStatus.HasValue, e => e.BlackListStatus == blackListStatus)
                    .WhereIf(patientId.HasValue, e => e.PatientId == patientId)
                    .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId);



        protected virtual async Task<IQueryable<BlackListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();
            var blackList = await GetDbSetAsync();
            var query = blackList
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.User)
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.Title)
                .Include(ar => ar.Patient)
                .Select(ar => new BlackListWithNavigationProperties
                {
                    BlackList = ar,
                    Doctor = ar.Doctor,
                    Patient = ar.Patient
                });
            return query;
        }

        protected virtual IQueryable<BlackListWithNavigationProperties> ApplyFilter(
            IQueryable<BlackListWithNavigationProperties> query,
           string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Patient.Id.ToString()!.Contains(filterText!) || e.Doctor.Id.ToString()!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.BlackList.Note!.Contains(note!))
                    .WhereIf(blackListStatus.HasValue, e => e.BlackList.BlackListStatus == blackListStatus)
                    .WhereIf(patientId.HasValue, e => e.Patient.Id == patientId)
                    .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId);
        #endregion
    }
}
