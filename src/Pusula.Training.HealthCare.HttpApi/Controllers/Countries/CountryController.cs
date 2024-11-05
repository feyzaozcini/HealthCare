using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.Countries;


[RemoteService]
[Area("app")]
[ControllerName("Country")]
[Route("api/app/countries")]
public class CountryController(ICountriesAppService countriesAppService)
            : HealthCareController, ICountriesAppService
{
    [HttpPost]
    public virtual Task<CountryDto> CreateAsync(CountryCreateDto input) => countriesAppService.CreateAsync(input);

    [HttpDelete]
    [Route("all")]
    public Task DeleteAllAsync(GetCountriesInput input) => countriesAppService.DeleteAllAsync(input);
    

    [HttpDelete]
    [Route("{id}")]
    public Task DeleteAsync(Guid id) => countriesAppService.DeleteAsync(id);


    [HttpDelete]
    [Route("")]
    public Task DeleteByIdsAsync(List<Guid> countryIds) => countriesAppService.DeleteByIdsAsync(countryIds);

    [HttpGet]
    [Route("{id}")]
    public Task<CountryDto> GetAsync(Guid id) => countriesAppService.GetAsync(id);

    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => countriesAppService.GetDownloadTokenAsync();

    [HttpGet]
    public Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input) => countriesAppService.GetListAsync(input);

    [HttpPut]
    public Task<CountryDto> UpdateAsync(CountryUpdateDto input) => countriesAppService.UpdateAsync(input);
}
