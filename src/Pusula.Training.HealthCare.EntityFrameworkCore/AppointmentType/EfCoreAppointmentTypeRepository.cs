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

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    internal class EfCoreAppointmentTypeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, AppointmentType, Guid>(dbContextProvider), IAppointmentTypeRepository
    {
        public async Task<List<AppointmentType>> GetAppointmentTypesForDoctorAsync(Guid doctorId)
        {
            var dbContext = await GetDbContextAsync();

            // AppointmentType'ları ve ilişkili bilgileri getir
            var appointmentTypes = await dbContext.DoctorAppoinmentTypes
                .Where(da => da.DoctorId == doctorId) // Doktora göre filtrele
                .Select(da => da.AppoinmentType)      // AppointmentType'ı seç
                .Distinct()                           // Aynı AppointmentType'ı tekrarlamamak için
                .ToListAsync();

            return appointmentTypes;
        }
        public async Task<List<DoctorWithNavigationProperties>> GetDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId)
        {
            var dbContext = await GetDbContextAsync();

            // Doktorları ve ilişkili bilgilerini getir
            var doctors = await dbContext.DoctorAppoinmentTypes
                .Where(dd => dd.AppoinmentTypeId == appointmentTypeId) // Muayene türlerine göre filtrele
                .Select(dd => new
                {
                    Doctor = dd.Doctor,
                    Title = dbContext.Titles.FirstOrDefault(t => t.Id == dd.Doctor.TitleId), // Title'ı getir
                    User = dbContext.Users.FirstOrDefault(u => u.Id == dd.Doctor.UserId)    // User'ı getir
                })
                .ToListAsync();

            // Sonucu DoctorWithNavigationProperties'e dönüştür
            return doctors.Select(d => new DoctorWithNavigationProperties
            {
                Doctor = d.Doctor,
                Title = d.Title!,
                User = d.User!
            }).ToList();
        }

        public async Task<List<AppointmentType>> GetAppointmentTypesByDoctorIdsAsync(List<Guid> doctorIds)
        {
            var dbContext = await GetDbContextAsync();

            // Verilen doktor ID'lerine sahip AppointmentType'ları 
            var appointmentTypes = await dbContext.DoctorAppoinmentTypes
                .Where(dd => doctorIds.Contains(dd.DoctorId)) // DoctorId'leri filtrele
                .Select(dd => dd.AppoinmentType) // İlgili AppointmentType'ları seç
                .Distinct() // Aynı AppointmentType'ları bir kez al
                .ToListAsync();

            return appointmentTypes; // AppointmentType listesini döndür
        }

        //doktorları önce silmek için
        public async Task RemoveAllDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId)
        {
            var dbContext = await GetDbContextAsync();

            // İlgili AppointmentTypeId için ara tablo kayıtlarını al
            var doctorAppointmentTypes = await dbContext.DoctorAppoinmentTypes
                .Where(dd => dd.AppoinmentTypeId == appointmentTypeId)
                .ToListAsync();

            // Ara tablodaki bu ilişkileri sil
            dbContext.DoctorAppoinmentTypes.RemoveRange(doctorAppointmentTypes);

            // Veritabanına değişiklikleri kaydet
            await dbContext.SaveChangesAsync();
        }




        public virtual async Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        int? durationInMinutes = null,
        CancellationToken cancellationToken = default
        )
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name, durationInMinutes);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? durationInMinutes = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name,durationInMinutes);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<AppointmentType>> GetListAsync(
            string? filterText = null,
            string? name = null,
            int? durationInMinutes = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
            )
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name,durationInMinutes);
            query = query.Include(d => d.DoctorAppointmentTypes).ThenInclude(dd => dd.Doctor);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentTypeConst.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<AppointmentType> ApplyFilter(
        IQueryable<AppointmentType> query,
        string? filterText = null,
        string? name = null,
        int? durationInMinutes = null)
        {
            return query
                .Include(d => d.DoctorAppointmentTypes)
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                   (e.Name != null && e.Name.Contains(filterText!))
                )
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
        }

        
    }
}
