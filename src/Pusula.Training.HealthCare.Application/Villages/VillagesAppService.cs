using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Districts;
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
using Pusula.Training.HealthCare.Exceptions;

namespace Pusula.Training.HealthCare.Villages
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Villages.Default)]
    public class VillagesAppService(
    IVillageRepository villageRepository,
    VillageManager villageManager,
    IDistributedCache<VillageDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, IVillagesAppService

    {
        public virtual async Task<PagedResultDto<VillageDto>> GetListAsync(GetVillagesInput input)
        {
            var totalCount = await villageRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await villageRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VillageDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Village>, List<VillageDto>>(items)
            };
        }

        public virtual async Task<VillageDto> GetAsync(Guid id) => ObjectMapper.Map<Village, VillageDto>(await villageRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.Villages.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            HealthCareException.ThrowIfNull(await villageRepository.FindAsync(id), HealthCareException.VILLAGE_NOT_FOUND_MESSAGE);
            await villageRepository.DeleteAsync(id);
        }


        [Authorize(HealthCarePermissions.Villages.Create)]
        public virtual async Task<VillageDto> CreateAsync(VillageCreateDto input) => ObjectMapper.Map<Village, VillageDto>(await villageManager.CreateAsync(input.DistrictId, input.Name));


        [Authorize(HealthCarePermissions.Villages.Edit)]
        public virtual async Task<VillageDto> UpdateAsync(Guid id, VillageUpdateDto input) => ObjectMapper.Map<Village, VillageDto>(await villageManager.UpdateAsync(id, input.Name));


        [Authorize(HealthCarePermissions.Villages.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> villageIds) => await villageRepository.DeleteManyAsync(villageIds);


        [Authorize(HealthCarePermissions.Villages.Delete)]
        public virtual async Task DeleteAllAsync(GetVillagesInput input)
        {
            var villages = await villageRepository.GetListAsync(
                input.FilterText,
                input.Name,
                input.Sorting,
                int.MaxValue,
                0
            );

            var idsToDelete = villages.Select(c => c.Id).ToList();
            await villageRepository.DeleteManyAsync(idsToDelete);
        }


        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VillageExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await villageRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Village>, List<VillageExcelDownloadDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Villages.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new VillageDownloadTokenCacheItem { Token = token },
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
