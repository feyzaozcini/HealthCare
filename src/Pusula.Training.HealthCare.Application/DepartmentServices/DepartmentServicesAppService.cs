using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Departments;
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
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;
using MiniExcelLibs;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.DepartmentServices.Default)]
    public class DepartmentServicesAppService(IDepartmentServiceRepository departmentServiceRepository,
        DepartmentServiceManager departmentServiceManager, IDistributedCache<DepartmentServiceDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IDepartmentServicesAppService
    {
        public virtual async Task<PagedResultDto<DepartmentServiceDto>> GetListAsync(GetDepartmentServicesInput input)
        {
            var totalCount = await departmentServiceRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await departmentServiceRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DepartmentServiceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DepartmentService>, List<DepartmentServiceDto>>(items)
            };
        }

        public virtual async Task<DepartmentServiceDto> GetAsync(Guid id) => ObjectMapper.Map<DepartmentService, DepartmentServiceDto>(await departmentServiceRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.DepartmentServices.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await departmentServiceRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.DepartmentServices.Create)]
        public virtual async Task<DepartmentServiceDto> CreateAsync(DepartmentServiceCreateDto input) => ObjectMapper.Map<DepartmentService, DepartmentServiceDto>(await departmentServiceManager.CreateAsync(input.Name));


        [Authorize(HealthCarePermissions.DepartmentServices.Edit)]
        public virtual async Task<DepartmentServiceDto> UpdateAsync(Guid id, DepartmentServiceUpdateDto input) => ObjectMapper.Map<DepartmentService, DepartmentServiceDto>(await departmentServiceManager.UpdateAsync(id, input.Name, input.ConcurrencyStamp));


        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DepartmentServiceExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await departmentServiceRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DepartmentService>, List<DepartmentServiceExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "DepartmentServices.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        [Authorize(HealthCarePermissions.DepartmentServices.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> departmentServiceIds) => await departmentServiceRepository.DeleteManyAsync(departmentServiceIds);

        
        [Authorize(HealthCarePermissions.DepartmentServices.Delete)]
        public virtual async Task DeleteAllAsync(GetDepartmentServicesInput input) => await departmentServiceRepository.DeleteAllAsync(input.FilterText, input.Name);

        
        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new DepartmentServiceDownloadTokenCacheItem { Token = token },
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
