using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Core.Rules.Patients;
using Pusula.Training.HealthCare.Core.Rules.TestGroups;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
    ITestGroupBusinessRules testGroupBusinessRules,
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ITestGroupsAppService
{
    [Authorize(HealthCarePermissions.TestGroups.Create)]
    public virtual async Task<TestGroupDto> CreateAsync(TestGroupsCreateDto input)
    {
        await testGroupBusinessRules.TestGroupNameDuplicatedAsync(input.Name);

        var testGroup = await testGroupManager.CreateAsync(
            input.Name
            );

        return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
    }

    [Authorize(HealthCarePermissions.TestGroups.Delete)]
    public virtual async Task<TestGroupsDeletedDto> DeleteAsync(Guid id)
    {
        await testGroupBusinessRules.ValidateTestGroupDeletableAsync(id);

        await testGroupRepository.DeleteAsync(id);

        return new TestGroupsDeletedDto
        {
            Id = id,
            Message = TestGroupConsts.TestGroupDeletedMessage
        };
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

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGroupNameLookupAsync(LookupRequestDto input)
    {
        var query = (await testGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null && x.Name.Contains(input.Filter!));

        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TestGroup>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestGroup>, List<LookupDto<Guid>>>(lookupData)
        };
    }

    [Authorize(HealthCarePermissions.TestGroups.Edit)]
    public virtual async Task<TestGroupDto> UpdateAsync(TestGroupsUpdateDto input)
    {
        await testGroupBusinessRules.TestGroupNameDuplicatedAsync(input.Name);

        var testGroup = await testGroupManager.UpdateAsync(
                    input.Id,
                    input.Name
                    );

        return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
    }
}
