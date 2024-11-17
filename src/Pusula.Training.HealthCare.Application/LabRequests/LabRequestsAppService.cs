using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.LabRequests;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.LabRequests.Default)]
public class LabRequestsAppService(
    ILabRequestRepository labRequestRepository,
    LabRequestManager labRequestManager,
    IDistributedCache<LabRequestDownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, ILabRequestsAppService
{
    [Authorize(HealthCarePermissions.LabRequests.Create)]
    public virtual async Task<LabRequestDto> CreateAsync(LabRequestCreateDto input)
    {
        var labRequests = await labRequestManager.CreateAsync(
           input.ProtocolId,
           input.DoctorId,
           input.TestGroupItemId,
           input.Name,
           input.Date,
           input.Status
           );

        return ObjectMapper.Map<LabRequest, LabRequestDto>(labRequests);
    }

    [Authorize(HealthCarePermissions.LabRequests.Delete)]
    public virtual async Task<LabRequestDeletedDto> DeleteAsync(Guid id)
    {
        LabRequest? labRequest = await labRequestRepository.GetAsync(
            predicate: l => l.Id == id
            );

        await labRequestRepository.DeleteAsync(id);

        LabRequestDeletedDto response = ObjectMapper.Map<LabRequest, LabRequestDeletedDto>(labRequest);
        response.Message = LabRequestConsts.LabRequestDeletedMessage;

        return response;
    }

    public virtual async Task<LabRequestDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<LabRequest, LabRequestDto>(await labRequestRepository.GetAsync(id));
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new LabRequestDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }

    public virtual async Task<PagedResultDto<LabRequestDto>> GetListAsync(GetLabRequestsInput input)
    {
        var totalCount = await labRequestRepository.GetCountAsync(
            input.FilterText,
            input.ProtocolId,
            input.DoctorId,
            input.TestGroupItemId,
            input.Name,
            input.Date,
            input.Status
            );
        var items = await labRequestRepository.GetListAsync(
            input.FilterText,
            input.ProtocolId,
            input.DoctorId,
            input.TestGroupItemId,
            input.Name,
            input.Date,
            input.Status,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
            );

        return new PagedResultDto<LabRequestDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<LabRequest>, List<LabRequestDto>>(items)
        };
    }

    [Authorize(HealthCarePermissions.LabRequests.Edit)]
    public virtual async Task<LabRequestDto> UpdateAsync(LabRequestUpdateDto input)
    {
        var labRequest = await labRequestManager.UpdateAsync(
                    input.Id,
                    input.ProtocolId,
                    input.DoctorId,
                    input.TestGroupItemId,
                    input.Name,
                    input.Date,
                    input.Status
                    );

        return ObjectMapper.Map<LabRequest, LabRequestDto>(labRequest);
    }
}
