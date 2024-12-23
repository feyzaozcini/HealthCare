using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Addresses
{
    public class EfCoreAddressRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Address, Guid>(dbContextProvider), IAddressRepository
    {
        public virtual async Task DeleteAllAsync(
            string? filterText = null,
            Guid? patientId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? districtId = null,
            Guid? villageId = null,
            string? addressDescription = null,
            bool? isPrimary = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, patientId, countryId, cityId, districtId, villageId, addressDescription, isPrimary);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }


        public virtual async Task<long> GetCountAsync(
           string? filterText = null,
           Guid? patientId = null,
           Guid? countryId = null,
           Guid? cityId = null,
           Guid? districtId = null,
           Guid? villageId = null,
           string? addressDescription = null,
           bool? isPrimary = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetDbSetAsync(), filterText, patientId, countryId, cityId, districtId, villageId, addressDescription, isPrimary);

            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        public virtual async Task<List<Address>> GetListAsync(
           string? filterText = null,
           Guid? patientId = null,
           Guid? countryId = null,
           Guid? cityId = null,
           Guid? districtId = null,
           Guid? villageId = null,
           string? addressDescription = null,
           bool? isPrimary = null,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {

            var query = ApplyFilter(await GetQueryForNavigationPropertiesAsync(), filterText, patientId, countryId, cityId, districtId, villageId, addressDescription, isPrimary);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }


        public virtual async Task<List<Address>> GetListWithNavigationPropertiesAsync(
           string? filterText = null,
           Guid? patientId = null,
           Guid? countryId = null,
           Guid? cityId = null,
           Guid? districtId = null,
           Guid? villageId = null,
           string? addressDescription = null,
           bool? isPrimary = null,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, patientId, countryId, cityId, districtId, villageId, addressDescription, isPrimary);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }


        public virtual async Task<Address> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var address = await query.FirstOrDefaultAsync(pr => pr.Id == id, cancellationToken);
            return address!;
        }


        protected virtual async Task<IQueryable<Address>> GetQueryForNavigationPropertiesAsync()
        {
            var dbSet = await GetDbSetAsync();
            return dbSet
                .Include(pr => pr.Patient)
                .Include(pr => pr.Country)
                .Include(pr => pr.City)
                .Include(pr => pr.District)
                .Include(pr => pr.Village);
        }


        protected virtual IQueryable<Address> ApplyFilter(
           IQueryable<Address> query,
           string? filterText = null,
           Guid? patientId = null,
           Guid? countryId = null,
           Guid? cityId = null,
           Guid? districtId = null,
           Guid? villageId = null,
           string? addressDescription = null,
           bool? isPrimary = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e =>
                    e.Patient.No!.ToString().Contains(filterText!) ||
                    e.Country.Name.Contains(filterText!) ||
                    e.City.Name.Contains(filterText!) ||
                    e.District.Name.Contains(filterText!) ||
                    e.Village.Name.Contains(filterText!) ||
                    e.AddressDescription.Contains(filterText!))

                    .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value)
                    .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value)
                    .WhereIf(cityId.HasValue, e => e.CityId == cityId!.Value)
                    .WhereIf(districtId.HasValue, e => e.DistrictId == districtId!.Value)
                    .WhereIf(villageId.HasValue, e => e.VillageId == villageId!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(addressDescription), e => e.AddressDescription != null && e.AddressDescription.Contains(addressDescription!))
                    .WhereIf(isPrimary.HasValue, e => e.IsPrimary == isPrimary!.Value);
        }
    }
}
