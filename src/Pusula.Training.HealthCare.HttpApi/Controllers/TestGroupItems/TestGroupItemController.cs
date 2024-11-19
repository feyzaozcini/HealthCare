using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.TestGroupItems;

[RemoteService]
[Area("app")]
[ControllerName("TestGroupItem")]
[Route("api/app/test-group-items")]
public class TestGroupItemController(ITestGroupItemsAppService testGroupItemsAppService)
    : HealthCareController, ITestGroupItemsAppService
{
    [HttpPost]
    public Task<TestGroupItemDto> CreateAsync(TestGroupItemsCreateDto input) => testGroupItemsAppService.CreateAsync(input);

    [HttpDelete]
    [Route("{id}")]
    public Task<TestGroupItemsDeletedDto> DeleteAsync(Guid id) => testGroupItemsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("{id}")]
    public Task<TestGroupItemDto> GetAsync(Guid id) => testGroupItemsAppService.GetAsync(id);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => testGroupItemsAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<TestGroupItemDto>> GetListAsync(GetTestGroupItemsInput input) => testGroupItemsAppService.GetListAsync(input);
    [HttpGet]

    [Route("with-navigation-properties/{id}")]
    public Task<TestGroupItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => testGroupItemsAppService.GetWithNavigationPropertiesAsync(id);

    [HttpPut]
    public Task<TestGroupItemDto> UpdateAsync(TestGroupItemsUpdateDto input) => testGroupItemsAppService.UpdateAsync(input);
}
