using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.SystemChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.SystemChecks
{
    [RemoteService]
    [Area("app")]
    [ControllerName("SystemCheck")]
    [Route("api/app/system-checks")]
    public class SystemCheckController(ISystemChecksAppService systemChecksAppService)
        : HealthCareController, ISystemChecksAppService
    {
        [HttpPost]
        public Task<SystemCheckDto> CreateAsync(SystemCheckCreateDto input) => systemChecksAppService.CreateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => systemChecksAppService.DeleteAsync(id);

        [HttpGet]
        [Route("{id}")]
        public Task<SystemCheckDto> GetAsync(Guid id) => systemChecksAppService.GetAsync(id);

        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<SystemCheckDto> GetByProtocolIdAsync(Guid protocolId) => systemChecksAppService.GetByProtocolIdAsync(protocolId);

        [HttpPut]
        public Task<SystemCheckDto> UpdateAsync(SystemCheckUpdateDto input) => systemChecksAppService.UpdateAsync(input);
    }
}
