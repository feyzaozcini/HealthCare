using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestValueRanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.TestProcesses;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.TestProcesses.Default)]
public class TestProcessesAppService(
    ITestProcessRepository testProcessRepository,
    TestProcessManager testProcessManager,
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, ITestProcessesAppService
{

    [Authorize(HealthCarePermissions.TestProcesses.Create)]
    public virtual async Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input)
    {
        var testProcess = await testProcessManager.CreateAsync(
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate
            );

        return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
    }
    
    [Authorize(HealthCarePermissions.TestProcesses.Delete)]
    public virtual async Task<TestProcessesDeletedDto> DeleteAsync(Guid id)
    {
        await testProcessRepository.DeleteAsync(id);

        return new TestProcessesDeletedDto
        {
            Id = id,
            Message = TestProcessConsts.TestProcessDeletedMessage
        };
    }

    public virtual async Task<TestProcessDto> GetAsync(Guid id) => ObjectMapper.Map<TestProcess, TestProcessDto>(await testProcessRepository.GetAsync(id));

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

    public virtual async Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input)
    {
        var totalCount = await testProcessRepository.GetCountAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate
            );

        var items = await testProcessRepository.GetListAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
            );

        return new PagedResultDto<TestProcessDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestProcess>, List<TestProcessDto>>(items)
        };
    }

    public async Task<PagedResultDto<TestProcessDto>> GetListWithNavigationPropertiesAsync(GetTestProcessesInput input)
    {
        var totalCount = await testProcessRepository.GetCountAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate
            );

        var items = await testProcessRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
            );

        return new PagedResultDto<TestProcessDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestProcess>, List<TestProcessDto>>(items)
        };
    }

    public async Task<TestProcessDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        var testProcess = await testProcessRepository.GetWithNavigationPropertiesAsync(id);
        return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
    }

    [Authorize(HealthCarePermissions.TestProcesses.Edit)]
    public async Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input)
    {
        var testProcess = await testProcessManager.UpdateAsync(
            input.Id,
            input.TestGroupItemId,
            input.LabRequestId,
            input.Status,
            input.Result,
            input.ResultDate
            );

        return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
    }
}
