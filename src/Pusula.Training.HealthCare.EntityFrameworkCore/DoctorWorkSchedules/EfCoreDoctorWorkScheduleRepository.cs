using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class EfCoreDoctorWorkScheduleRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, DoctorWorkSchedule, Guid>(dbContextProvider), IDoctorWorkScheduleRepository
    {

        public async Task<List<DoctorWorkSchedule>> GetWorkScheduleForDoctorAsync(Guid doctorId)
        {
            return await (await GetQueryableAsync())
                .Where(x => x.DoctorId == doctorId)
                .ToListAsync();
        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            int[]? workingDays = null, 
            string? startHour = null, 
            string? endHour = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, doctorId, workingDays,startHour,endHour);
            var ids = query.Select(x => x.DoctorWorkSchedule.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            int[]? workingDays = null, 
            string? startHour = null, 
            string? endHour = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, doctorId,workingDays,startHour,endHour);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<DoctorWorkSchedule>> GetListAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            int[]? workingDays = null, 
            string? startHour = null, 
            string? endHour = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, doctorId, workingDays,startHour,endHour);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorWorkScheduleConsts.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<DoctorWorkScheduleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            int[]? workingDays = null, 
            string? startHour = null, 
            string? endHour = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, doctorId, workingDays,startHour,endHour);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorWorkScheduleConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<DoctorWorkScheduleWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(doctorWorkSchedule => new DoctorWorkScheduleWithNavigationProperties
                {
                    DoctorWorkSchedule = doctorWorkSchedule,
                    Doctor = dbContext.Set<Doctor>().FirstOrDefault(c => c.Id == doctorWorkSchedule.DoctorId)!,
                })
                .FirstOrDefault()!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<DoctorWorkSchedule> ApplyFilter(
            IQueryable<DoctorWorkSchedule> query,
            string? filterText = null,
            Guid? doctorId = null,
            int[]? workingDays = null,
            string? startHour = null,
            string? endHour = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.StartHour!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId);



        protected virtual async Task<IQueryable<DoctorWorkScheduleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
            from doctorWorkSchedule in (await GetDbSetAsync())
            join doctor in (await GetDbContextAsync()).Set<Doctor>()
            .Include(d => d.User)
            .Include(d => d.Title)
            on doctorWorkSchedule.DoctorId equals doctor.Id into doctors
            from doctor in doctors.DefaultIfEmpty()
            select new DoctorWorkScheduleWithNavigationProperties
            {
                DoctorWorkSchedule = doctorWorkSchedule,
                Doctor = doctor
            };



        protected virtual IQueryable<DoctorWorkScheduleWithNavigationProperties> ApplyFilter(
            IQueryable<DoctorWorkScheduleWithNavigationProperties> query,
            string? filterText = null,
            Guid? doctorId = null,
            int[]? workingDays = null,
            string? startHour = null,
            string? endHour = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DoctorWorkSchedule.StartHour!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId);

        
        #endregion
    }
}
