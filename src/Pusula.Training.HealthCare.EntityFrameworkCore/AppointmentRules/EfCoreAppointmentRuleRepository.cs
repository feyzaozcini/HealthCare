using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Departments;
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
            string? description = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,doctorId, departmentId,gender,age,description);
            var ids = query.Select(x => x.AppointmentRule.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Gender? gender = null,
            int? age = null,
            string? description = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, doctorId, departmentId, gender,age,description);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<AppointmentRule>> GetListAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null, 
            Gender? gender = null,
            int? age = null,
            string? description = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText,  doctorId, departmentId,gender,age,description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentRuleConst.GetDefaultSorting(false) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<AppointmentRuleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, 
            Guid? doctorId = null, 
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            string? description = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,doctorId, departmentId, gender,age,description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentRuleConst.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<AppointmentRuleWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(appointmentRule => new AppointmentRuleWithNavigationProperties
                {
                    AppointmentRule = appointmentRule,
                    Doctor = dbContext.Set<Doctor>().FirstOrDefault(c => c.Id == appointmentRule.DoctorId)!,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == appointmentRule.DepartmentId)!,
                })
                .FirstOrDefault()!;
        }


        #region ApplyFilter and Queryable
        protected virtual IQueryable<AppointmentRule> ApplyFilter(
            IQueryable<AppointmentRule> query,
             string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            string? description = null) =>
                query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId);



        protected virtual async Task<IQueryable<AppointmentRuleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
            from appointmentRule in (await GetDbSetAsync())
            join doctor in (await GetDbContextAsync()).Set<Doctor>()
            .Include(d => d.User)
            .Include(d => d.Title)
            on appointmentRule.DoctorId equals doctor.Id into doctors
            from doctor in doctors.DefaultIfEmpty()
            join department in (await GetDbContextAsync()).Set<Department>() on appointmentRule.DepartmentId equals department.Id into departments
            from department in departments.DefaultIfEmpty()
            select new AppointmentRuleWithNavigationProperties
            {
                AppointmentRule = appointmentRule,
                Doctor = doctor,
                Department = department
            };
            


        protected virtual IQueryable<AppointmentRuleWithNavigationProperties> ApplyFilter(
            IQueryable<AppointmentRuleWithNavigationProperties> query,
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            string? description = null) =>
                query
                     .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AppointmentRule.Description!.Contains(filterText!))
                    .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId)
                    .WhereIf(departmentId.HasValue, e => e.Department.Id == departmentId);
        #endregion
    }
}
