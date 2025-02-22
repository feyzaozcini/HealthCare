using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Patients;

[RemoteService]
[Area("app")]
[ControllerName("Patient")]
[Route("api/app/patients")]

public class PatientController : HealthCareController, IPatientsAppService
{
    protected IPatientsAppService _patientsAppService;

    public PatientController(IPatientsAppService patientsAppService)
    {
        _patientsAppService = patientsAppService;
    }

    [HttpGet]
    public virtual Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetListAsync(GetPatientsInput input)
    {
        return _patientsAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<PatientDto> GetAsync(Guid id)
    {
        return _patientsAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("with-navigation-properties/{id}")]
    public Task<PatientWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => _patientsAppService.GetWithNavigationPropertiesAsync(id);

    [HttpGet]
    [Route("company-lookup")]
    public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input) => _patientsAppService.GetCompanyLookupAsync(input);

    [HttpGet]
    [Route("country-lookup")]
    public Task<PagedResultDto<GetCountryLookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input) => _patientsAppService.GetCountryLookupAsync(input);

    [HttpPost]
    public virtual Task<PatientDto> CreateAsync(PatientCreateDto input)
    {
        return _patientsAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input)
    {
        return _patientsAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public async virtual Task<PatientDeletedDto> DeleteAsync(Guid id)
    {
        PatientDeletedDto input = new PatientDeletedDto { Id = id };
        return await _patientsAppService.DeleteAsync(input.Id);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input)
    {
        return _patientsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return _patientsAppService.GetDownloadTokenAsync();
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> patientIds)
    {
        return _patientsAppService.DeleteByIdsAsync(patientIds);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetPatientsInput input)
    {
        return _patientsAppService.DeleteAllAsync(input);
    }

    

}