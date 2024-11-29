using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestValueRanges;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.TestValueRanges;

[RemoteService]
[Area("app")]
[ControllerName("TestValueRange")]
[Route("api/app/test-value-range")]
public class TestValueRangeController(ITestValueRangesAppService testValueRangesAppService)
    : HealthCareController, ITestValueRangesAppService
{
    [HttpPost]
    public Task<TestValueRangeDto> CreateAsync(TestValueRangesCreateDto input) => testValueRangesAppService.CreateAsync(input);

    [HttpDelete]
    [Route("{id}")]
    public Task<TestValueRangesDeletedDto> DeleteAsync(Guid id) => testValueRangesAppService.DeleteAsync(id);

    [HttpGet]
    [Route("{id}")]
    public Task<TestValueRangeDto> GetAsync(Guid id) => testValueRangesAppService.GetAsync(id);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => testValueRangesAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<TestValueRangeDto>> GetListAsync(GetTestValueRangesInput input) => testValueRangesAppService.GetListAsync(input);

    [HttpGet]
    [Route("get-list-with-navigation")]
    public Task<PagedResultDto<TestValueRangeDto>> GetListWithNavigationPropertiesAsync(GetTestValueRangesInput input) => testValueRangesAppService.GetListWithNavigationPropertiesAsync(input);

    [HttpGet]
    [Route("get-with-navigation")]
    public Task<TestValueRangeDto> GetWithNavigationPropertiesAsync(Guid id) => testValueRangesAppService.GetWithNavigationPropertiesAsync(id);

    [HttpPut]
    public Task<TestValueRangeDto> UpdateAsync(TestValueRangesUpdateDto input) => testValueRangesAppService.UpdateAsync(input);
}
