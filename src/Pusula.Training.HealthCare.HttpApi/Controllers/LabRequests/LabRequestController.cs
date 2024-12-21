using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.LabRequests;

[RemoteService]
[Area("app")]
[ControllerName("LabRequest")]
[Route("api/app/lab-requests")]
public class LabRequestController(ILabRequestsAppService labRequestsAppService)
    : HealthCareController, ILabRequestsAppService
{

    [HttpPost]
    public Task<LabRequestDto> CreateAsync(LabRequestCreateDto input) => labRequestsAppService.CreateAsync(input);

    [HttpDelete]
    [Route("{id}")]
    public Task<LabRequestDeletedDto> DeleteAsync(Guid id) => labRequestsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("{id}")]
    public Task<LabRequestDto> GetAsync(Guid id) => labRequestsAppService.GetAsync(id);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => labRequestsAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<LabRequestDto>> GetListAsync(GetLabRequestsInput input) => labRequestsAppService.GetListAsync(input);

    [HttpGet]
    [Route("get-list-with-navigation")]
    public Task<PagedResultDto<LabRequestDto>> GetListWithNavigationPropertiesAsync(GetLabRequestsInput input) => labRequestsAppService.GetListWithNavigationPropertiesAsync(input);

    [HttpGet]
    [Route("get-with-navigation")]
    public Task<LabRequestDto> GetWithNavigationPropertiesAsync(Guid id) => labRequestsAppService.GetWithNavigationPropertiesAsync(id);

    [HttpGet]
    [Route("get-notify-test-results")]
    public Task NotifyTestResultsAsync(LabRequestDto labRequest) => labRequestsAppService.NotifyTestResultsAsync(labRequest);

    [HttpPut]
    public Task<LabRequestDto> UpdateAsync(LabRequestUpdateDto input) => labRequestsAppService.UpdateAsync(input);
}
