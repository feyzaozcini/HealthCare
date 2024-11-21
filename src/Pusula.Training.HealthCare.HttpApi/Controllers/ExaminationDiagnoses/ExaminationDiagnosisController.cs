using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.ExaminationDiagnoses
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ExaminationDiagnosis")]
    [Route("api/app/examinationDiagnosis")]
    public class ExaminationDiagnosisController(IExaminationDiagnosisAppService examinationDiagnosisAppService)
        : HealthCareController, IExaminationDiagnosisAppService
    {

        [HttpPost]
        public Task<ExaminationDiagnosisDto> CreateAsync(ExaminationDiagnosisCreateDto input)
            =>examinationDiagnosisAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => examinationDiagnosisAppService.DeleteAsync(id);
      

        [HttpDelete]
        [Route("with-diagnosisId/{diagnosisId}")]
        public Task DeleteByDiagnosisIdAsync(Guid diagnosisId) => 
            examinationDiagnosisAppService.DeleteByDiagnosisIdAsync(diagnosisId);

        [HttpGet]
        [Route("{id}")]
        public Task<ExaminationDiagnosisDto> GetAsync(Guid id) => examinationDiagnosisAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListAsync(GetExaminationDiagnosisInput input) 
            => examinationDiagnosisAppService.GetListAsync(input);


        [HttpGet]
        [Route("/not-paged")]
        public Task<List<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListNotPagedAsync(GetExaminationDiagnosisInput input)
            => examinationDiagnosisAppService.GetListNotPagedAsync(input);


        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ExaminationDiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
            => examinationDiagnosisAppService.GetWithNavigationPropertiesAsync(id);

        [HttpPut]
        public Task<ExaminationDiagnosisDto> UpdateAsync(ExaminationDiagnosisUpdateDto input)
            => examinationDiagnosisAppService.UpdateAsync(input);
    }
}
