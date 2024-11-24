using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.DoctorDepartments;

namespace Pusula.Training.HealthCare.Departments;

public class EfCoreDepartmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Department, Guid>(dbContextProvider), IDepartmentRepository
{
    public async Task<Department?> GetWithDoctorsAsync(Guid departmentId)
    {
        var dbContext = await GetDbContextAsync();

        var department = await dbContext.Departments
            .Include(d => d.DoctorDepartments)
            .ThenInclude(dd => dd.Doctor)
            .FirstOrDefaultAsync(d => d.Id == departmentId);

        if (department != null)
        {
            foreach (var doctorDepartment in department.DoctorDepartments)
            {
                var doctor = await dbContext.Doctors
                    .Where(doc => doc.Id == doctorDepartment.DoctorId)
                    .Select(doc => new DoctorWithNavigationProperties
                    {
                        Doctor = doc,
                        User = dbContext.Users.FirstOrDefault(u => u.Id == doc.UserId) ,
                        Title = dbContext.Titles.FirstOrDefault(t => t.Id == doc.TitleId),
                        DoctorDepartments = dbContext.DoctorDepartments
                            .Where(dd => dd.DoctorId == doc.Id)
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (doctor != null)
                {
                    doctorDepartment.Doctor = doctor.Doctor;
                }
            }
        }

        return department;
    }

    public async Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(Guid departmentId)
    {
        var dbContextProvider = await GetDbContextAsync();
        return await dbContextProvider.DoctorDepartments
            .Where(dd => dd.DepartmentId == departmentId)
            .Select(dd => dd.Doctor)
            .ToListAsync();
    }

    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, name);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Department>> GetListAsync(
        string? filterText = null,
        string? name = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Department> ApplyFilter(
        IQueryable<Department> query,
        string? filterText = null,
        string? name = null)
    {
        return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
    }
}
