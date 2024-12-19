using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Core.Rules.TestProcesses;
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
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.TestProcesses;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.TestProcesses.Default)]
public class TestProcessesAppService(
    ITestProcessRepository testProcessRepository,
    TestProcessManager testProcessManager,
    ITestProcessBusinessRules testProcessBusinessRules,
    IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, ITestProcessesAppService
{

    [Authorize(HealthCarePermissions.TestProcesses.Create)]
    public virtual async Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input)
    {
        await testProcessBusinessRules.ValidateRecentTestsAsync(input.LabRequestId, input.TestGroupItemId);

        var testProcess = await testProcessManager.CreateAsync(
            input.LabRequestId,
            input.TestGroupItemId,
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

    //LabRequestID'ye göre TestProcess Listeleme.
    public virtual async Task<List<TestProcessWithNavigationPropertiesDto>> GetByLabRequestIdAsync(Guid labRequestId)
    {
        var testProcessesWithNavigation = await testProcessRepository.GetByLabRequestIdAsync(labRequestId);

        return ObjectMapper.Map<List<TestProcessWithNavigationProperties>, List<TestProcessWithNavigationPropertiesDto>>(testProcessesWithNavigation);
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

    public virtual async Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input)
    {
        var totalCount = await testProcessRepository.GetCountAsync(
            input.FilterText,
            input.LabRequestId,
            input.TestGroupItemId,
            input.Status,
            input.Result,
            input.ResultDate,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo
            );

        var items = await testProcessRepository.GetListAsync(
            input.FilterText,
            input.LabRequestId,
            input.TestGroupItemId,
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

    public async Task<PagedResultDto<TestProcessWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(GetTestProcessesInput input)
    {
        var totalCount = await testProcessRepository.GetCountAsync(
            input.FilterText,
            input.LabRequestId,
            input.TestGroupItemId,
            input.Status,
            input.Result,
            input.ResultDate,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo);

        var items = await testProcessRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText,
            input.Status,
            input.Result,
            input.ResultDate,
            input.DoctorName,
            input.DoctorSurname,
            input.PatientName,
            input.PatientSurname,
            input.ProtocolNo,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount);

        return new PagedResultDto<TestProcessWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<TestProcessWithNavigationProperties>, List<TestProcessWithNavigationPropertiesDto>>(items)
        };
    }


    //Tetkik İstem Test Sayısı Raporlama
    public async Task<List<TestCountDto>> GetTestCountsAsync()
    {
        var testProcesses = await testProcessRepository
        .GetListWithNavigationPropertiesAsync();

        var testCounts = testProcesses
            .Where(tp => tp.TestGroupItem != null) 
            .GroupBy(tp => tp.TestGroupItem!.Name)
            .Select(group => new TestCountDto
            {
                TestName = group.Key,
                Count = group.Count()
            })
            .ToList();
            
        return testCounts;
    }

    //Tetkik İstem Test Grup Sayısı Raporlama
    public async Task<List<TestGroupCountDto>> GetTestGroupCountsAsync()
    {
        var testProcesses = await testProcessRepository
        .GetListWithNavigationPropertiesAsync();

        var testCounts = testProcesses
            .Where(tp => tp.TestGroupItem.TestGroup != null)
            .GroupBy(tp => tp.TestGroupItem.TestGroup.Name)
            .Select(group => new TestGroupCountDto
            {
                TestName = group.Key,
                Count = group.Count()
            })
            .ToList();

        return testCounts;
    }

    public async Task<TestProcessWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        var testProcess = await testProcessRepository.GetWithNavigationPropertiesAsync(id);
        return ObjectMapper.Map<TestProcessWithNavigationProperties, TestProcessWithNavigationPropertiesDto>(testProcess);
    }

    [Authorize(HealthCarePermissions.TestProcesses.Edit)]
    public async Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input)
    {
        var testProcess = await testProcessManager.UpdateAsync(
            input.Id,
            input.LabRequestId,
            input.TestGroupItemId,
            input.Status,
            input.Result,
            input.ResultDate
            );

        return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
    }
}
