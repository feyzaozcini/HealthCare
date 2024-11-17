﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.TestGroupItems;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.TestGroupItems.Default)]
public class TestGroupItemsAppService(
    ITestGroupItemRepository testGroupItemRepository,
    TestGroupItemManager testGroupItemManager,
    IDistributedCache<TestGroupItemDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ITestGroupItemsAppService
{
    [Authorize(HealthCarePermissions.TestGroupItems.Create)]
    public virtual async Task<TestGroupItemDto> CreateAsync(TestGroupItemsCreateDto input)
    {
        var testGroupItem = await testGroupItemManager.CreateAsync(
           input.TestGroupId,
           input.Name,
           input.Code,
           input.TestType,
           input.Description,
           input.TurnaroundTime
           );

        return ObjectMapper.Map<TestGroupItem, TestGroupItemDto>(testGroupItem);
    }
    
    [Authorize(HealthCarePermissions.TestGroupItems.Delete)]
    public virtual async Task<TestGroupItemsDeletedDto> DeleteAsync(Guid id)
    {
        TestGroupItem? testGroupItem = await testGroupItemRepository.GetAsync(
            predicate: t => t.Id == id
            );

        await testGroupItemRepository.DeleteAsync(id);

        TestGroupItemsDeletedDto response = ObjectMapper.Map<TestGroupItem, TestGroupItemsDeletedDto>(testGroupItem);
        response.Message = TestGroupItemConsts.TestGroupItemDeletedMessage;

        return response;
    }

    public virtual async Task<TestGroupItemDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<TestGroupItem, TestGroupItemDto>(await testGroupItemRepository.GetAsync(id));
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new TestGroupItemDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }

    public virtual async Task<PagedResultDto<TestGroupItemDto>> GetListAsync(GetTestGroupItemsInput input)
    {
        var totalCount = await testGroupItemRepository.GetCountAsync(
            input.FilterText, 
            input.TestGroupId,
            input.Name,
            input.Code,
            input.TestType,
            input.Description,
            input.TurnaroundTime
            );
        var items = await testGroupItemRepository.GetListAsync(
            input.FilterText,
            input.TestGroupId,
            input.Name,
            input.Code,
            input.TestType,
            input.Description,
            input.TurnaroundTime, 
            input.Sorting, 
            input.MaxResultCount, 
            input.SkipCount
            );

        return new PagedResultDto<TestGroupItemDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestGroupItem>, List<TestGroupItemDto>>(items)
        };
    }
    
    [Authorize(HealthCarePermissions.TestGroupItems.Edit)]
    public virtual async Task<TestGroupItemDto> UpdateAsync(TestGroupItemsUpdateDto input)
    {
        var testGroupItem = await testGroupItemManager.UpdateAsync(
                    input.Id,
                    input.TestGroupId,
                    input.Name,
                    input.Code,
                    input.TestType,
                    input.Description,
                    input.TurnaroundTime
                    );

        return ObjectMapper.Map<TestGroupItem, TestGroupItemDto>(testGroupItem);
    }
}
