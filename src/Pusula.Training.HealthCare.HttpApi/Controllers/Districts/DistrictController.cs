using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Controllers.Districts
{

    [RemoteService]
    [Area("app")]
    [ControllerName("District")]
    [Route("api/app/districts")]
    public class DistrictController(IDistrictsAppService districtsAppService)
            : HealthCareController, IDistrictsAppService
    {
        [HttpGet]
        public Task<PagedResultDto<DistrictDto>> GetListAsync(GetDistrictsInput input) => districtsAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DistrictDto> GetAsync(Guid id) => districtsAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<DistrictDto> CreateAsync(DistrictCreateDto input) => districtsAppService.CreateAsync(input);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input) => districtsAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => districtsAppService.DeleteAsync(id);

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DistrictExcelDownloadDto input) => districtsAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => districtsAppService.GetDownloadTokenAsync();

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> districtIds) => districtsAppService.DeleteByIdsAsync(districtIds);

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetDistrictsInput input) => districtsAppService.DeleteAllAsync(input);
    }
}
