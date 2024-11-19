using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.Cities;

namespace Pusula.Training.HealthCare.Controllers.Cities
{
    [RemoteService]
    [Area("app")]
    [ControllerName("City")]
    [Route("api/app/cities")]
    public class CityController(ICitiesAppService citiesAppService)
            : HealthCareController, ICitiesAppService
    {
        [HttpGet]
        public Task<PagedResultDto<CityDto>> GetListAsync(GetCitiesInput input) => citiesAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CityDto> GetAsync(Guid id) => citiesAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<CityDto> CreateAsync(CityCreateDto input) => citiesAppService.CreateAsync(input);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input) => citiesAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => citiesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CityExcelDownloadDto input) => citiesAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => citiesAppService.GetDownloadTokenAsync();

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> cityIds) => citiesAppService.DeleteByIdsAsync(cityIds);

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetCitiesInput input) => citiesAppService.DeleteAllAsync(input);
    }
}
