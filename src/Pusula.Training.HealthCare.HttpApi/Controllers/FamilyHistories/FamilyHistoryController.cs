using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.FamilyHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.FamilyHistories
{
    [RemoteService]
    [Area("app")]
    [ControllerName("FamilyHistory")]
    [Route("api/app/family-histories")]
    public class FamilyHistoryController(IFamilyHistoriesAppService familyHistoriesAppService)
        : HealthCareController, IFamilyHistoriesAppService
    {

        [HttpPost]
        public Task<FamilyHistoryDto> CreateAsync(FamilyHistoryCreateDto input) => familyHistoriesAppService.CreateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => familyHistoriesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<FamilyHistoryDto> GetAsync(Guid id) => familyHistoriesAppService.GetAsync(id);


        [HttpGet]
        [Route("with-patientId/{patientId}")]
        public Task<FamilyHistoryDto> GetByPatientIdAsync(Guid patientId) => familyHistoriesAppService.GetByPatientIdAsync(patientId);

        [HttpPut]
        public Task<FamilyHistoryDto> UpdateAsync(FamilyHistoryUpdateDto input) => familyHistoriesAppService.UpdateAsync(input);
    }
}
