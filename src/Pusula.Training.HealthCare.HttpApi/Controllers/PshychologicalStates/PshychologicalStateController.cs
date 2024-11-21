using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.PshychologicalStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.PshychologicalStates
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PshychologicalState")]
    [Route("api/app/pshychologicalState")]
    public class PshychologicalStateController(IPshychologicalStatesAppService pshychologicalStatesAppService)
        : HealthCareController, IPshychologicalStatesAppService
    {
        [HttpPost]
        public Task<PshychologicalStateDto> CreateAsync(PshychologicalStateCreateDto input) =>
            pshychologicalStatesAppService.CreateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => pshychologicalStatesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<PshychologicalStateDto> GetAsync(Guid id) => pshychologicalStatesAppService.GetAsync(id);


        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PshychologicalStateWithNavigationDto> GetWithNavigationPropertiesAsync(Guid id) =>
            pshychologicalStatesAppService.GetWithNavigationPropertiesAsync(id);


        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<PshychologicalStateDto> GetWithProtocolIdAsync(Guid protocolId) =>
            pshychologicalStatesAppService.GetWithProtocolIdAsync(protocolId);

        [HttpPut]
        public Task<PshychologicalStateDto> UpdateAsync(PshychologicalStateUpdateDto input) => pshychologicalStatesAppService.UpdateAsync(input);
    }
}
