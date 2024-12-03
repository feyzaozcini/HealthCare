using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.TestProcesses;

[RemoteService]
[Area("app")]
[ControllerName("TestProcess")]
[Route("api/app/test-processes")]
public class TestProcessController(ITestProcessesAppService testProcessesAppService)
    : HealthCareController, ITestProcessesAppService
{

    [HttpPost]
    public Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input) => testProcessesAppService.CreateAsync(input);

    [HttpDelete]
    [Route("{id}")]
    public Task<TestProcessesDeletedDto> DeleteAsync(Guid id) => testProcessesAppService.DeleteAsync(id);

    [HttpGet]
    [Route("{id}")]
    public Task<TestProcessDto> GetAsync(Guid id) => testProcessesAppService.GetAsync(id);

    [HttpGet]
    [Route("by-lab-request/{labRequestId}")]
    public Task<List<TestProcessDto>> GetByLabRequestIdAsync(Guid labRequestId) => testProcessesAppService.GetByLabRequestIdAsync(labRequestId);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => testProcessesAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input) => testProcessesAppService.GetListAsync(input);

    [HttpGet]
    [Route("get-list-with-navigation-properties")]
    public Task<PagedResultDto<TestProcessDto>> GetListWithNavigationPropertiesAsync(GetTestProcessesInput input) => testProcessesAppService.GetListWithNavigationPropertiesAsync(input);

    [HttpGet]
    [Route("get-with-navigation-properties")]
    public Task<TestProcessDto> GetWithNavigationPropertiesAsync(Guid id) => testProcessesAppService.GetWithNavigationPropertiesAsync(id);

    [HttpPut]
    public Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input) => testProcessesAppService.UpdateAsync(input);
}
