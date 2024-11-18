using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors
{
    public class EfCoreDoctorRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Doctor, Guid>(dbContextProvider), IDoctorRepository
    {
       
        public async Task DeleteAllAsync(string? filterText = null, 
            Guid? userId = null, 
            Guid? titleId = null, 
            string? identityNumber = null, 
            DateTime? birthDateMin = null, 
            DateTime? birthDateMax = null, 
            Gender? gender = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, birthDateMin, birthDateMax, gender, identityNumber, userId, titleId);

            var ids = query.Select(x => x.Doctor.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }
       

       
        public async Task<DoctorWithNavigationProperties> GetWithNavigationProperties(Guid id, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(doctor => new DoctorWithNavigationProperties
                {
                    Doctor = doctor,
                    User = dbContext.Users.FirstOrDefault(c => c.Id == doctor.UserId)!,
                    Title = dbContext.Set<Title>().FirstOrDefault(c => c.Id == doctor.TitleId)!
                })
                .FirstOrDefault()!;
        }
      
        public async Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, 
            Guid? userId = null, 
            Guid? titleId = null, 
            string? identityNumber = null, 
            DateTime? birthDateMin = null, 
            DateTime? birthDateMax = null, 
            Gender? gender = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, birthDateMin, birthDateMax, gender, identityNumber, userId, titleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<List<Doctor>> GetListAsync(string? filterText = null, 
            Guid? userId = null, 
            Guid? titleId = null, 
            string? identityNumber = null, 
            DateTime? birthDateMin = null, 
            DateTime? birthDateMax = null, 
            Gender? gender = null, 
            string? sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, birthDateMin, birthDateMax, gender, identityNumber, userId, titleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(true) : sorting);
            return await query.Page(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(string? filterText = null, 
            Guid? userId = null, 
            Guid? titleId = null, 
            string? identityNumber = null, 
            DateTime? birthDateMin = null, 
            DateTime? birthDateMax = null, 
            Gender? gender = null, 
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, birthDateMin, birthDateMax, gender, identityNumber, userId, titleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        protected virtual IQueryable<Doctor> ApplyFilter(
            IQueryable<Doctor> query,
            string? filterText = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            Gender? gender = null,
            string? identityNumber = null,
            Guid? userId = null,
            Guid? titleId = null)
        {
            return query
           .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.IdentityNumber.Contains(filterText!))
           .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
           .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
           .WhereIf(gender.HasValue, e => e.Gender == gender)
           .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber == identityNumber)
           .WhereIf(userId.HasValue, e => e.UserId == userId!.Value)
           .WhereIf(titleId.HasValue, e => e.TitleId == titleId!.Value);
        }


        protected virtual async Task<IQueryable<DoctorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync() =>
         from doctor in (await GetDbSetAsync())
         join title in (await GetDbContextAsync()).Set<Title>() on doctor.TitleId equals title.Id into titles
         from title in titles.DefaultIfEmpty()
         join user in (await GetDbContextAsync()).Users on doctor.UserId equals user.Id into users
         from user in users.DefaultIfEmpty()
         select new DoctorWithNavigationProperties
         {
             Doctor = doctor,
             Title = title,
             User = user
         };

        protected virtual IQueryable<DoctorWithNavigationProperties> ApplyFilter(
            IQueryable<DoctorWithNavigationProperties> query,
            string? filterText = null,
            DateTime? birthDateMin = null,
            DateTime? birthDateMax = null,
            Gender? gender = null,
            string? identityNumber = null,
            Guid? userId = null,
            Guid? titleId = null)

        {
            return query
               .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                   e =>
                       (e.Doctor.IdentityNumber.Contains(filterText!) ||    
                        e.User.Name.Contains(filterText!) ||                
                        e.User.Surname.Contains(filterText!)) &&           
                        e.Doctor.UserId == e.User.Id)                      
               .WhereIf(birthDateMin.HasValue, e => e.Doctor.BirthDate >= birthDateMin!.Value)
               .WhereIf(birthDateMax.HasValue, e => e.Doctor.BirthDate <= birthDateMax!.Value)
               .WhereIf(gender.HasValue, e => e.Doctor.Gender == gender)
               .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.Doctor.IdentityNumber == identityNumber)
               .WhereIf(userId.HasValue, e => e.Doctor.UserId == userId!.Value)
               .WhereIf(titleId.HasValue, e => e.Title.Id == titleId!.Value);
        }


    }
}
