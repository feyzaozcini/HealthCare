using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Addresses
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Addresses.Default)]
    public class AddressesAppService(
        IAddressRepository addressRepository,
        AddressManager addressManager,
        IDistributedCache<AddressDownloadTokenCacheItem, string> downloadTokenCache) : HealthCareAppService, IAddressesAppService

    {
        [Authorize(HealthCarePermissions.Addresses.Create)]
        public virtual async Task<AddressDto> CreateAsync(AddressCreateDto input)
        {
            var address = await addressManager.CreateAsync(
            input.PatientId, input.CountryId, input.CityId, input.DistrictId,
            input.VillageId, input.AddressDescription, input.IsPrimary
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }


        [Authorize(HealthCarePermissions.Addresses.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await addressRepository.DeleteAsync(id);

        public virtual async Task<AddressDto> GetAsync(Guid id) => ObjectMapper.Map<Address, AddressDto>(await addressRepository.GetAsync(id));


        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new AddressDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }


        public virtual async Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressesInput input)
        {
            var totalCount = await addressRepository.GetCountAsync(
                input.FilterText,
                input.PatientId,
                input.CountryId,
                input.CityId,
                input.DistrictId,
                input.VillageId,
                input.AddressDescription,
                input.IsPrimary
                );

            var items = await addressRepository.GetListAsync(
                input.FilterText,
                input.PatientId,
                input.CountryId,
                input.CityId,
                input.DistrictId,
                input.VillageId,
                input.AddressDescription,
                input.IsPrimary,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount
                );


            return new PagedResultDto<AddressDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Address>, List<AddressDto>>(items)
            };
        }


        public virtual async Task<PagedResultDto<AddressDto>> GetListWithNavigationPropertiesAsync(GetAddressesInput input)
        {
            var totalCount = await addressRepository.GetCountAsync(
                input.FilterText,
                input.PatientId,
                input.CountryId,
                input.CityId,
                input.DistrictId,
                input.VillageId,
                input.AddressDescription,
                input.IsPrimary
                );
            var items = await addressRepository.GetListWithNavigationPropertiesAsync(
                input.FilterText,
                input.PatientId,
                input.CountryId,
                input.CityId,
                input.DistrictId,
                input.VillageId,
                input.AddressDescription,
                input.IsPrimary,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount
                );

            return new PagedResultDto<AddressDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Address>, List<AddressDto>>(items)
            };
        }


        public virtual async Task<AddressDto> GetWithNavigationPropertiesAsync(Guid id) => ObjectMapper.Map<Address, AddressDto>(await addressRepository.GetWithNavigationPropertiesAsync(id));


        [Authorize(HealthCarePermissions.Addresses.Edit)]
        public virtual async Task<AddressDto> UpdateAsync(Guid id, AddressUpdateDto input)
        {
            var address = await addressManager.UpdateAsync(
                id,
                input.PatientId,
                input.CountryId,
                input.CityId,
                input.DistrictId,
                input.VillageId,
                input.AddressDescription,
                input.IsPrimary,
                input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Address, AddressDto>(address);
        }


        [Authorize(HealthCarePermissions.Addresses.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> addressIds) => await addressRepository.DeleteManyAsync(addressIds);


        [Authorize(HealthCarePermissions.Addresses.Delete)]
        public virtual async Task DeleteAllAsync(GetAddressesInput input) => await addressRepository.DeleteAllAsync(
            input.FilterText,
            input.PatientId,
            input.CountryId,
            input.CityId,
            input.DistrictId,
            input.VillageId,
            input.AddressDescription,
            input.IsPrimary);
    }
}
