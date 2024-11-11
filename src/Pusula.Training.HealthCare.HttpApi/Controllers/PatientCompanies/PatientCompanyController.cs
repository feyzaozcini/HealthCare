using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.PatientCompanies
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PatientCompanies")]
    [Route("api/app/patientCompanies")]
    public class PatientCompanyController(IPatientCompaniesAppService patientCompaniesAppService) : HealthCareController, IPatientCompaniesAppService
    {
        [HttpGet]
        public virtual Task<PagedResultDto<PatientCompanyDto>> GetListAsync(GetPatientCompaniesInput input) => patientCompaniesAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public Task<PatientCompanyDto> GetAsync(Guid id) => patientCompaniesAppService.GetAsync(id);

        [HttpPost]
        public Task<PatientCompanyDto> CreateAsync(PatientCompanyCreateDto input) => patientCompaniesAppService.CreateAsync(input);

        [HttpPut]
        public Task<PatientCompanyDto> UpdateAsync(PatientCompanyUpdateDto input) => patientCompaniesAppService.UpdateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public async Task<PatientCompanyDeleteDto> DeleteAsync(Guid id) 
        {

            PatientCompanyDeleteDto input = new PatientCompanyDeleteDto { Id = id };
            return await patientCompaniesAppService.DeleteAsync(input.Id);
        } 

        [HttpGet]
        [Route("as-excel-file")]
        public Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientCompanyExcelDownloadDto input) => patientCompaniesAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => patientCompaniesAppService.GetDownloadTokenAsync();


        [HttpDelete]
        [Route("")]
        public Task DeleteByIdAsync(List<Guid> patientCompanyIds) => patientCompaniesAppService.DeleteByIdAsync(patientCompanyIds);

        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetPatientCompaniesInput input) => patientCompaniesAppService.DeleteAllAsync(input);
       






     





    }
}
