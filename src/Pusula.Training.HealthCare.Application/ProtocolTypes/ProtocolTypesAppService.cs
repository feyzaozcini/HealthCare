using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.DepartmentServices;
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
using Volo.Abp;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.ProtocolTypes.Default)]
    public class ProtocolTypesAppService(IProtocolTypeRepository protocolTypeRepository,
        ProtocolTypeManager protocolTypeManager, IDistributedCache<ProtocolTypeDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IProtocolTypesAppService
    {
        public virtual async Task<PagedResultDto<ProtocolTypeDto>> GetListAsync(GetProtocolTypesInput input)
        {
            var totalCount = await protocolTypeRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await protocolTypeRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProtocolTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProtocolType>, List<ProtocolTypeDto>>(items)
            };
        }

        public virtual async Task<ProtocolTypeDto> GetAsync(Guid id) => ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(await protocolTypeRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.ProtocolTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await protocolTypeRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.ProtocolTypes.Create)]
        public virtual async Task<ProtocolTypeDto> CreateAsync(ProtocolTypeCreateDto input) => ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(await protocolTypeManager.CreateAsync(input.Name));


        [Authorize(HealthCarePermissions.ProtocolTypes.Edit)]
        public virtual async Task<ProtocolTypeDto> UpdateAsync(ProtocolTypeUpdateDto input) => ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(await protocolTypeManager.UpdateAsync(input.Id, input.Name, input.ConcurrencyStamp));


        [Authorize(HealthCarePermissions.ProtocolTypes.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> protocolTypeIds) => await protocolTypeRepository.DeleteManyAsync(protocolTypeIds);


        [Authorize(HealthCarePermissions.ProtocolTypes.Delete)]
        public virtual async Task DeleteAllAsync(GetProtocolTypesInput input) => await protocolTypeRepository.DeleteAllAsync(input.FilterText, input.Name);


        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new ProtocolTypeDownloadTokenCacheItem { Token = token },
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
