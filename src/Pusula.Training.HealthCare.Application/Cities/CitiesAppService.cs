using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using Volo.Abp.ObjectMapping;
using MiniExcelLibs;

namespace Pusula.Training.HealthCare.Cities
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Cities.Default)]
    public class CitiesAppService(
    ICityRepository cityRepository,
    CityManager cityManager,
    IDistributedCache<CityDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ICitiesAppService

    {
        public virtual async Task<PagedResultDto<CityDto>> GetListAsync(GetCitiesInput input)
        {
            var totalCount = await cityRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await cityRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CityDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<City>, List<CityDto>>(items)
            };
        }

        public virtual async Task<CityDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<City, CityDto>(await cityRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.Cities.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var city = await cityRepository.FindAsync(id);
            if (city == null)
            {
                throw new EntityNotFoundException(typeof(City), id);
            }

            await cityRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.Cities.Create)]
        public virtual async Task<CityDto> CreateAsync(CityCreateDto input)
        {

            var city = await cityManager.CreateAsync(
                input.CountryId,
                input.Name
                );

            return ObjectMapper.Map<City, CityDto>(city);
        }

        [Authorize(HealthCarePermissions.Cities.Edit)]
        public virtual async Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input)
        {
            var city = await cityManager.UpdateAsync(
                id,
                input.Name
            );

            return ObjectMapper.Map<City, CityDto>(city);
        }

        [Authorize(HealthCarePermissions.Cities.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> cityIds)
        {
            await cityRepository.DeleteManyAsync(cityIds);
        }


        [Authorize(HealthCarePermissions.Cities.Delete)]
        public virtual async Task DeleteAllAsync(GetCitiesInput input)
        {
            var cities = await cityRepository.GetListAsync(
                input.FilterText,
                input.Name,
                input.Sorting,
                int.MaxValue,
                0
            );

            var idsToDelete = cities.Select(c => c.Id).ToList();
            await cityRepository.DeleteManyAsync(idsToDelete);
        }



        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CityExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await cityRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<City>, List<CityExcelDownloadDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Cities.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new CityDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}
