using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Countries;


[RemoteService]
[Area("app")]
[ControllerName("Country")]
[Route("api/app/countries")]
public class CountryController(ICountriesAppService countriesAppService)
            : HealthCareController, ICountriesAppService
{
    [HttpGet]
    public Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input) => countriesAppService.GetListAsync(input);

    [HttpGet]
    [Route("{id}")]
    public virtual Task<CountryDto> GetAsync(Guid id) => countriesAppService.GetAsync(id);

    [HttpPost]
    public virtual Task<CountryDto> CreateAsync(CountryCreateDto input) => countriesAppService.CreateAsync(input);

    [HttpPut]
    [Route("{id}")]
    public virtual Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input) => countriesAppService.UpdateAsync(id, input);

    [HttpDelete]
    [Route("{id}")]
    public Task DeleteAsync(Guid id) => countriesAppService.DeleteAsync(id);

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CountryExcelDownloadDto input) => countriesAppService.GetListAsExcelFileAsync(input);

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => countriesAppService.GetDownloadTokenAsync();

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> countryIds) => countriesAppService.DeleteByIdsAsync(countryIds);

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetCountriesInput input) => countriesAppService.DeleteAllAsync(input);
}
