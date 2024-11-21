using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.DiagnosisGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.Diagnoses
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Diagnosis")]
    [Route("api/app/diagnosis")]
    public class DiagnosisController(IDiagnosisAppService diagnosisAppService) : HealthCareController, IDiagnosisAppService
    {
        [HttpPost]
        public Task<DiagnosisDto> CreateAsync(DiagnosisCreateDto input) => diagnosisAppService.CreateAsync(input);

        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetDiagnosisInput input) => diagnosisAppService.DeleteAllAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => diagnosisAppService.DeleteAsync(id);


        [HttpDelete]
        [Route("")]
        public Task DeleteByIdsAsync(List<Guid> diagnosesIds) => diagnosisAppService.DeleteByIdsAsync(diagnosesIds);


        [HttpGet]
        [Route("{id}")]
        public Task<DiagnosisDto> GetAsync(Guid id) => diagnosisAppService.GetAsync(id);


        [HttpGet]
        public Task<PagedResultDto<DiagnosisWithNavigationPropertiesDto>> GetListAsync(GetDiagnosisInput input) =>
            diagnosisAppService.GetListAsync(input);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<DiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => diagnosisAppService.GetWithNavigationPropertiesAsync(id);


        [HttpPut]
        public Task<DiagnosisDto> UpdateAsync(DiagnosisUpdateDto input) => diagnosisAppService.UpdateAsync(input);
        
    }
}
