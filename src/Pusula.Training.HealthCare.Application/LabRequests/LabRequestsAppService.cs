using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.EmailServices;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.LabRequests;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.LabRequests.Default)]
public class LabRequestsAppService(
    ILabRequestRepository labRequestRepository,
    LabRequestManager labRequestManager,
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache,
    IDistributedEventBus distributedEventBus
    ) : HealthCareAppService, ILabRequestsAppService
{
    [Authorize(HealthCarePermissions.LabRequests.Create)]
    public virtual async Task<LabRequestDto> CreateAsync(LabRequestCreateDto input)
    {
        var labRequests = await labRequestManager.CreateAsync(
           input.ProtocolId,
           input.DoctorId,
           input.Date,
           input.Status,
           input.Description
           );

        return ObjectMapper.Map<LabRequest, LabRequestDto>(labRequests);
    }

    [Authorize(HealthCarePermissions.LabRequests.Delete)]
    public virtual async Task<LabRequestDeletedDto> DeleteAsync(Guid id)
    {
        await labRequestRepository.DeleteAsync(id);

        return new LabRequestDeletedDto
        {
            Id = id,
            Message = LabRequestConsts.LabRequestDeletedMessage
        };
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
            new DownloadTokenCacheItem { Token = token },
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
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo,
            input.Date,
            input.Status,
            input.Description
            );
        var items = await labRequestRepository.GetListAsync(
            input.FilterText,
            input.ProtocolId,
            input.DoctorId,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo,
            input.Date,
            input.Status,
            input.Description,
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

    public virtual async Task<PagedResultDto<LabRequestDto>> GetListWithNavigationPropertiesAsync(GetLabRequestsInput input)
    {
        var totalCount = await labRequestRepository.GetCountAsync(
            input.FilterText,
            input.ProtocolId,
            input.DoctorId,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo,
            input.Date,
            input.Status,
            input.Description
            );
        var items = await labRequestRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText,
            input.ProtocolId,
            input.DoctorId,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo,
            input.Date,
            input.Status,
            input.Description,
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

    public async Task<LabRequestDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        var labRequest = await labRequestRepository.GetWithNavigationPropertiesAsync(id);
        return ObjectMapper.Map<LabRequest, LabRequestDto>(labRequest);
    }

    public async Task NotifyTestResultsAsync(LabRequestDto labRequestDto)
    {
        await distributedEventBus.PublishAsync(new TestProcessSendMailEto
        {
            Email = labRequestDto.PatientMail,
            PatientName = $"{labRequestDto.PatientName} {labRequestDto.PatientSurname}"
        });
    }

    [Authorize(HealthCarePermissions.LabRequests.Edit)]
    public virtual async Task<LabRequestDto> UpdateAsync(LabRequestUpdateDto input)
    {
        var labRequest = await labRequestManager.UpdateAsync(
                    input.Id,
                    input.ProtocolId,
                    input.DoctorId,
                    input.Date,
                    input.Status,
                    input.Description
                    );

        return ObjectMapper.Map<LabRequest, LabRequestDto>(labRequest);
    }
}
