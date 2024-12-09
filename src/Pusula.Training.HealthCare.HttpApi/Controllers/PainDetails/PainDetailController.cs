using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.PainDetails
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PainDetails")]
    [Route("api/app/pain-details")]

    public class PainDetailController(IPainDetailsAppService painDetailsAppService)
    : HealthCareController, IPainDetailsAppService
    {

        [HttpPost]
        public Task<PainDetailDto> CreateAsync(PainDetailCreateDto input) => painDetailsAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => painDetailsAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<PainDetailDto> GetAsync(Guid id) => painDetailsAppService.GetAsync(id);

        [HttpGet]
        [Route("painType-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPainTypeLookupAsync(LookupRequestDto input) => painDetailsAppService.GetPainTypeLookupAsync(input);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PainDetailDto> GetWithNavigationPropertiesAsync(Guid id) =>
            painDetailsAppService.GetWithNavigationPropertiesAsync(id);

        [HttpGet]
        [Route("with-navigation-properties-by-protocol/{protocolId}")]
        public Task<PainDetailDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId) =>
            painDetailsAppService.GetWithNavigationPropertiesByProtocolIdAsync(protocolId);


        [HttpPut]
        public Task<PainDetailDto> UpdateAsync(PainDetailUpdateDto input) => painDetailsAppService.UpdateAsync(input);


    }
}
