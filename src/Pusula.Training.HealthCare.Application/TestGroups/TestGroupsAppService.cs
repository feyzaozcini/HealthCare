using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Core.Helpers.Concretes;
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
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache,
    IDistributedCache distributedCache,
    ILogger<TestGroupsAppService> logger)    
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
        var cacheHelper = new CacheHelper<TestGroupDto, GetTestGroupsInput>(
        distributedCache,
        Logger
    );
        return await cacheHelper.GetOrAddAsync(
            async () =>
            {
                var totalCount = await testGroupRepository.GetCountAsync(input.FilterText, input.Name);
                var items = await testGroupRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

                return new PagedResultDto<TestGroupDto>
                {
                    TotalCount = totalCount,
                    Items = ObjectMapper.Map<List<TestGroup>, List<TestGroupDto>>(items)
                };
            },
            input,
            "TestGroups:GetList",
            TimeSpan.FromMinutes(60)
        );
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGroupNameLookupAsync(LookupRequestDto input)
    {
        var cacheHelper = new CacheHelper<LookupDto<Guid>, LookupRequestDto>(
        distributedCache,
        Logger
    );

        return await cacheHelper.GetOrAddLookupAsync<Guid>(
            async () =>
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
            },
            input,
            "TestGroups:Lookup",
            TimeSpan.FromMinutes(30)
        );
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

    //public virtual async Task<PagedResultDto<TestGroupDto>> GetCachedTestGroupsAsync(GetTestGroupsInput input)
    //{

    //    // her getlist buna gidebilmeli.
    //    //generictype ile yönetilecek 
    //    //testgroup ve getlist için reflection'ları araştır. içinde bulunduğu servis metot...
    //    var cacheKey = $"TestGroups:GetList:{input.FilterText}-{input.Name}:{input.MaxResultCount}:{input.SkipCount}"; //inputlar generic olacak 

    //    //Generic yap sadece içine cache data'yı ver.

    //    var cachedData = await distributedCache.GetAsync(cacheKey); //distributedcache'i kullanıcıdan al

    //    if (cachedData != null)
    //    {
    //        Logger.LogInformation($"Cache hit for key: {cacheKey}"); //Generic yapıda loglar olsun. Yoktu oluştu, vardı bu oldu gibi... Info. Tüm Lookup'lar bağlanamaz.
    //        return new PagedResultDto<TestGroupDto>
    //        {
    //            TotalCount = cachedData.Count,
    //            Items = cachedData
    //        };
    //    }

    //    var result = await GetListAsync(input); //GetTotalCount ve GetItems gelcek. Reflection'lara bak.

    //    await distributedCache.SetAsync(
    //        cacheKey,
    //        result.Items.ToList(),
    //        new DistributedCacheEntryOptions
    //        {
    //            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1 * 60)
    //        });

    //    return result;
    //}
}
