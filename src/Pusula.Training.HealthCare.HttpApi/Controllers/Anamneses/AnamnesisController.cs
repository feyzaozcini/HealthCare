using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.Anamneses
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Anamnesis")]
    [Route("api/app/anamnesis")]
    public class AnamnesisController(IAnamnesisAppService anamnesisAppService)
        : HealthCareController, IAnamnesisAppService
    {

        [HttpPost]
        public Task<AnamnesisDto> CreateAsync(AnamnesisCreateDto input) => anamnesisAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => anamnesisAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<AnamnesisDto> GetAsync(Guid id) => anamnesisAppService.GetAsync(id);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<AnamnesisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) =>
            anamnesisAppService.GetWithNavigationPropertiesAsync(id);


        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<AnamnesisDto> GetWithProtocolIdAsync(Guid protocolId) => anamnesisAppService.GetWithProtocolIdAsync(protocolId);

        [HttpPut]
        public Task<AnamnesisDto> UpdateAsync(AnamnesisUpdateDto input) => anamnesisAppService.UpdateAsync(input);
        
    }
}
