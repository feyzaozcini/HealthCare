using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.BlackLists;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.BlackLists
{
    [RemoteService]
    [Area("app")]
    [ControllerName("BlackList")]
    [Route("api/app/blackList")]
    public class BlackListController(IBlackListsAppService blackListsAppService) : HealthCareController, IBlackListsAppService
    {
        [HttpGet]
        [Route("{id}")]
        public Task<BlackListDto> GetAsync(Guid id) => blackListsAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<BlackListWithNavigationPropertiesDto>> GetListAsync(GetBlackListInput input) => blackListsAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}/with-navigation-properties")]
        public Task<BlackListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => blackListsAppService.GetWithNavigationPropertiesAsync(id);

        [HttpPost]
        public Task<BlackListDto> CreateAsync(BlackListCreateDto input) => blackListsAppService.CreateAsync(input);

        [HttpPut]
        public Task<BlackListDto> UpdateAsync(BlackListUpdateDto input) => blackListsAppService.UpdateAsync(input);

        [HttpDelete]
        public Task DeleteAsync(Guid id) => blackListsAppService.DeleteAsync(id);

        [HttpGet]
        [Route("patient-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input) => blackListsAppService.GetPatientLookupAsync(input);

        [HttpGet]
        [Route("doctor-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input) => blackListsAppService.GetDoctorLookupAsync(input);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => blackListsAppService.GetDownloadTokenAsync();

    }
}
