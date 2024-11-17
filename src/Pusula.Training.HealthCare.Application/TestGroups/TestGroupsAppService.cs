using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.TestGroups;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.TestGroups.Default)]
public class TestGroupsAppService(
    ITestGroupRepository testGroupRepository,
    TestGroupManager testGroupManager,
    IDistributedCache<TestGroupDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ITestGroupsAppService
{
    [Authorize(HealthCarePermissions.TestGroups.Create)]
    public virtual async Task<TestGroupDto> CreateAsync(TestGroupsCreateDto input)
    {
        var testGroup = await testGroupManager.CreateAsync(
            input.Name
            );

        return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
    }

    [Authorize(HealthCarePermissions.TestGroups.Delete)]
    public virtual async Task<TestGroupsDeletedDto> DeleteAsync(Guid id)
    {
        TestGroup? testGroup = await testGroupRepository.GetAsync(
            predicate: t => t.Id == id
            );

        await testGroupRepository.DeleteAsync(id);

        TestGroupsDeletedDto response = ObjectMapper.Map<TestGroup, TestGroupsDeletedDto>(testGroup);
        response.Message = TestGroupConsts.TestGroupDeletedMessage;

        return response;
    }

    public virtual async Task<TestGroupDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<TestGroup, TestGroupDto>(await testGroupRepository.GetAsync(id));
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new TestGroupDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }

    public virtual async Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input)
    {
        var totalCount = await testGroupRepository.GetCountAsync(input.FilterText, input.Name);
        var items = await testGroupRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<TestGroupDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestGroup>, List<TestGroupDto>>(items)
        };
    }

    [Authorize(HealthCarePermissions.TestGroups.Edit)]
    public virtual async Task<TestGroupDto> UpdateAsync(TestGroupsUpdateDto input)
    {
        var testGroup = await testGroupManager.UpdateAsync(
                    input.Id,
                    input.Name
                    );

        return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
    }
}
