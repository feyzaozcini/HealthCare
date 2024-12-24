using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.PatientHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.PatientHistories
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PatientHistory")]
    [Route("api/app/patient-histories")]
    public class PatientHistoryController(IPatientHistoriesAppService patientHistoriesAppService)
        : HealthCareController, IPatientHistoriesAppService
    {
        [HttpPost]

        public Task<PatientHistoryDto> CreateAsync(PatientHistoryCreateDto input) => patientHistoriesAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => patientHistoriesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("{id}")]
        public Task<PatientHistoryDto> GetAsync(Guid id) => patientHistoriesAppService.GetAsync(id);

        [HttpGet]
        [Route("with-patientId/{patientId}")]
        public Task<PatientHistoryDto> GetByPatientIdAsync(Guid patientId) => patientHistoriesAppService.GetByPatientIdAsync(patientId);

        [HttpPut]
        public Task<PatientHistoryDto> UpdateAsync(PatientHistoryUpdateDto input) => patientHistoriesAppService.UpdateAsync(input);
    }
}
