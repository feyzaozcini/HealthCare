using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.LabRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class EfCoreAppointmentRuleRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, AppointmentRule, Guid>(dbContextProvider), IAppointmentRuleRepository
    {
        public async Task<List<AppointmentRule>> GetRulesForDepartmentAsync(Guid departmentId)
        {
            return await (await GetQueryableAsync())
                .Where(x => x.DepartmentId == departmentId)
                .ToListAsync();
        }
        public async Task<List<AppointmentRule>> GetRulesForDoctorAsync(Guid doctorId)
        {
            return await (await GetQueryableAsync())
                .Where(x => x.DoctorId == doctorId)
                .ToListAsync();
        }
        public virtual async Task DeleteAllAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,doctorId, departmentId,gender,age,minAge,maxAge,description);
            var ids = query.Select(x => x.AppointmentRule.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, doctorId, departmentId, gender,age,minAge,maxAge,description);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<AppointmentRule>> GetListAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText,  doctorId, departmentId,gender,age,minAge,maxAge,description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentRuleConst.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<AppointmentRuleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,doctorId, departmentId, gender,age,minAge,maxAge,description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentRuleConst.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<AppointmentRuleWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var appointmentRule = await query.FirstOrDefaultAsync(lr => lr.AppointmentRule.Id == id, cancellationToken);
            HealthCareException.ThrowIf(appointmentRule == null);
            return appointmentRule!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<AppointmentRule> ApplyFilter(
            IQueryable<AppointmentRule> query,
             string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId);



        protected virtual async Task<IQueryable<AppointmentRuleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();
            var appointmentRules = await GetDbSetAsync();
            var query = appointmentRules
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.User)
                .Include(ar => ar.Doctor)
                    .ThenInclude(d => d.Title)
                .Include(ar => ar.Department)
                .Select(ar => new AppointmentRuleWithNavigationProperties
                {
                    AppointmentRule = ar,
                    Doctor = ar.Doctor,
                    Department = ar.Department
                });
            return query;
        }

        protected virtual IQueryable<AppointmentRuleWithNavigationProperties> ApplyFilter(
            IQueryable<AppointmentRuleWithNavigationProperties> query,
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null) =>
                query
                     .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AppointmentRule.Description!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.Department.Id == departmentId);
        #endregion
    }
}
