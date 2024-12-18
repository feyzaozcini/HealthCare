using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.FollowUpPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.FollowUpPlans
{
    [RemoteService]
    [Area("app")]
    [ControllerName("FollowUpPlan")]
    [Route("api/app/followUp-plans")]
    public class FollowUpPlanController(IFollowUpPlansAppService followUpPlansAppService)
        : HealthCareController, IFollowUpPlansAppService
    {
        [HttpPost]
        public Task<FollowUpPlanDto> CreateAsync(FollowUpPlanCreateDto input) => followUpPlansAppService.CreateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => followUpPlansAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<FollowUpPlanDto> GetAsync(Guid id) => followUpPlansAppService.GetAsync(id);

        [HttpGet]
        [Route("with-protocolId/{protocolId}")]
        public Task<FollowUpPlanDto> GetByProtocolIdAsync(Guid protocolId) => followUpPlansAppService.GetByProtocolIdAsync(protocolId);


        [HttpPut]
        public Task<FollowUpPlanDto> UpdateAsync(FollowUpPlanUpdateDto input) => followUpPlansAppService.UpdateAsync(input);
    }
}
