using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.PainTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.PainTypes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("PainType")]
    [Route("api/app/painTypes")]
    public class PainTypeController(IPainTypesAppService painTypesAppService)
    : HealthCareController, IPainTypesAppService
    {

        [HttpPost]
        public Task<PainTypeDto> CreateAsync(PainTypeCreateDto input) => painTypesAppService.CreateAsync(input);


        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetPainTypesInput input) => painTypesAppService.DeleteAllAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => painTypesAppService.DeleteAsync(id);

        [HttpDelete]
        [Route("")]
        public Task DeleteByIdsAsync(List<Guid> painTypeIds)=> painTypesAppService.DeleteByIdsAsync(painTypeIds);

        [HttpGet]
        [Route("{id}")]
        public Task<PainTypeDto> GetAsync(Guid id) => painTypesAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<PainTypeDto>> GetListAsync(GetPainTypesInput input) => painTypesAppService.GetListAsync(input);

        [HttpPut]
        public Task<PainTypeDto> UpdateAsync(PainTypeUpdateDto input) => painTypesAppService.UpdateAsync(input);
    }
}
