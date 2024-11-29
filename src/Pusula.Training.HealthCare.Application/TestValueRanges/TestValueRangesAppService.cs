using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.TestValueRanges;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.TestValueRanges.Default)]
public class TestValueRangesAppService(
    ITestValueRangeRepository testValueRangeRepository,
    TestValueRangeManager testValueRangeManager,
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, ITestValueRangesAppService
{

    [Authorize(HealthCarePermissions.TestValueRanges.Create)]
    public async Task<TestValueRangeDto> CreateAsync(TestValueRangesCreateDto input)
    {
        var testValueRange = await testValueRangeManager.CreateAsync(
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit
            );

        return ObjectMapper.Map<TestValueRange, TestValueRangeDto>(testValueRange);
    }
    
    [Authorize(HealthCarePermissions.TestValueRanges.Delete)]
    public async Task<TestValueRangesDeletedDto> DeleteAsync(Guid id)
    {
        await testValueRangeRepository.DeleteAsync(id);

        return new TestValueRangesDeletedDto
        {
            Id = id,
            Message = TestValueRangeConsts.TestValueRangeDeletedMessage
        };
    }

    public async Task<TestValueRangeDto> GetAsync(Guid id) => ObjectMapper.Map<TestValueRange, TestValueRangeDto>(await testValueRangeRepository.GetAsync(id));

    public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
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

    public async Task<PagedResultDto<TestValueRangeDto>> GetListAsync(GetTestValueRangesInput input)
    {
        var totalCount = await testValueRangeRepository.GetCountAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit
            );

        var items = await testValueRangeRepository.GetListAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
            );

        return new PagedResultDto<TestValueRangeDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestValueRange>, List<TestValueRangeDto>>(items)
        };
    }

    public async Task<PagedResultDto<TestValueRangeDto>> GetListWithNavigationPropertiesAsync(GetTestValueRangesInput input)
    {
        var totalCount = await testValueRangeRepository.GetCountAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit
            );

        var items = await testValueRangeRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText,
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
            );

        return new PagedResultDto<TestValueRangeDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestValueRange>, List<TestValueRangeDto>>(items)
        };
    }

    public async Task<TestValueRangeDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        var testValueRange = await testValueRangeRepository.GetWithNavigationPropertiesAsync(id);
        return ObjectMapper.Map<TestValueRange, TestValueRangeDto>(testValueRange);
    }

    [Authorize(HealthCarePermissions.TestValueRanges.Edit)]
    public async Task<TestValueRangeDto> UpdateAsync(TestValueRangesUpdateDto input)
    {
        var testValueRange = await testValueRangeManager.UpdateAsync(
            input.Id,
            input.TestGroupItemId,
            input.MinValue,
            input.MaxValue,
            input.Unit
            );

        return ObjectMapper.Map<TestValueRange, TestValueRangeDto>(testValueRange);
    }
}
