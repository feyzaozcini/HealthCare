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

namespace Pusula.Training.HealthCare.Departments;

public class EfCoreDepartmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Department, Guid>(dbContextProvider), IDepartmentRepository
{
    public async Task<List<DoctorWithNavigationProperties>> GetDoctorsByDepartmentIdAsync(Guid departmentId)
    {
        var dbContext = await GetDbContextAsync(); 

        // Doktorlarý ve iliþkili bilgilerini getir
        var doctors = await dbContext.DoctorDepartments
            .Where(dd => dd.DepartmentId == departmentId) // Departmana göre filtrele
            .Select(dd => new
            {
                Doctor = dd.Doctor,
                Title = dbContext.Titles.FirstOrDefault(t => t.Id == dd.Doctor.TitleId), // Title'ý getir
                User = dbContext.Users.FirstOrDefault(u => u.Id == dd.Doctor.UserId)    // User'ý getir
            })
            .ToListAsync();

        // Sonucu DoctorWithNavigationProperties'e dönüþtür
        return doctors.Select(d => new DoctorWithNavigationProperties
        {
            Doctor = d.Doctor,
            Title = d.Title!,
            User = d.User!
        }).ToList();
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
        query = query.Include(d => d.Doctors).ThenInclude(dd => dd.Doctor);
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
                .Include(d => d.Doctors) // Include Doctors
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
    }
}
