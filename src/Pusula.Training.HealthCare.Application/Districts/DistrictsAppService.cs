using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Cities;
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

namespace Pusula.Training.HealthCare.Districts
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Districts.Default)]
    public class DistrictsAppService(
    IDistrictRepository districtRepository,
    DistrictManager districtManager,
    IDistributedCache<DistrictDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, IDistrictsAppService

    {
        public virtual async Task<PagedResultDto<DistrictDto>> GetListAsync(GetDistrictsInput input)
        {
            var totalCount = await districtRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await districtRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DistrictDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<District>, List<DistrictDto>>(items)
            };
        }

        public virtual async Task<DistrictDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<District, DistrictDto>(await districtRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.Districts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var district = await districtRepository.FindAsync(id);
            if (district == null)
            {
                throw new EntityNotFoundException(typeof(District), id);
            }

            await districtRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.Districts.Create)]
        public virtual async Task<DistrictDto> CreateAsync(DistrictCreateDto input)
        {

            var district = await districtManager.CreateAsync(
                input.CityId,
                input.Name
                );

            return ObjectMapper.Map<District, DistrictDto>(district);
        }

        [Authorize(HealthCarePermissions.Districts.Edit)]
        public virtual async Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input)
        {
            var district = await districtManager.UpdateAsync(
                id,
                input.Name
            );

            return ObjectMapper.Map<District, DistrictDto>(district);
        }

        [Authorize(HealthCarePermissions.Districts.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> districtIds)
        {
            await districtRepository.DeleteManyAsync(districtIds);
        }

        [Authorize(HealthCarePermissions.Districts.Delete)]
        public virtual async Task DeleteAllAsync(GetDistrictsInput input)
        {
            var districts = await districtRepository.GetListAsync(
                input.FilterText,
                input.Name,
                input.Sorting,
                int.MaxValue,
                0
            );

            var idsToDelete = districts.Select(c => c.Id).ToList();
            await districtRepository.DeleteManyAsync(idsToDelete);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DistrictExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await districtRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<District>, List<DistrictExcelDownloadDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Districts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new DistrictDownloadTokenCacheItem { Token = token },
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