using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.TestGroups;

[RemoteService]
[Area("app")]
[ControllerName("TestGroup")]
[Route("api/app/test-groups")]
public class TestGroupController(ITestGroupsAppService testGroupsAppService)
    : HealthCareController, ITestGroupsAppService
{
    [HttpPost]
    public Task<TestGroupDto> CreateAsync(TestGroupsCreateDto input) => testGroupsAppService.CreateAsync(input);

    [HttpDelete]
    [Route("{id}")]
    public Task<TestGroupsDeletedDto> DeleteAsync(Guid id) => testGroupsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("{id}")]
    public Task<TestGroupDto> GetAsync(Guid id) => testGroupsAppService.GetAsync(id);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => testGroupsAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input) => testGroupsAppService.GetListAsync(input);

    [HttpGet]
    [Route("test-group-name-lookup")]
    public Task<PagedResultDto<LookupDto<Guid>>> GetGroupNameLookupAsync(LookupRequestDto input) => testGroupsAppService.GetGroupNameLookupAsync(input);

    [HttpPut]
    public Task<TestGroupDto> UpdateAsync(TestGroupsUpdateDto input) => testGroupsAppService.UpdateAsync(input);
}
