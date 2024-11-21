using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.PshychologicalStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.FallRisks
{
    [RemoteService]
    [Area("app")]
    [ControllerName("FallRisks")]
    [Route("api/app/fallRisks")]
    public class FallRiskController(IFallRisksAppService fallRisksAppService)
        : HealthCareController, IFallRisksAppService
    {

        [HttpPost]
        public Task<FallRiskDto> CreateAsync(FallRiskCreateDto input) => fallRisksAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => fallRisksAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<FallRiskDto> GetAsync(Guid id) => fallRisksAppService.GetAsync(id);


        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<FallRiskWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) =>
            fallRisksAppService.GetWithNavigationPropertiesAsync(id);

        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<FallRiskDto> GetWithProtocolIdAsync(Guid protocolId) => fallRisksAppService.GetWithProtocolIdAsync(protocolId);

        [HttpPut]
        public Task<FallRiskDto> UpdateAsync(FallRiskUpdateDto input) =>
            fallRisksAppService.UpdateAsync(input);
    }
}
