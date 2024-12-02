using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.ProtocolTypes;

namespace Pusula.Training.HealthCare.Controllers.ProtocolTypes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ProtocolType")]
    [Route("api/app/protocolTypes")]
    public class ProtocolTypeController(IProtocolTypesAppService protocolTypesAppService)
    : HealthCareController, IProtocolTypesAppService
    {
        [HttpGet]
        public virtual Task<PagedResultDto<ProtocolTypeDto>> GetListAsync(GetProtocolTypesInput input) => protocolTypesAppService.GetListAsync(input);


        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProtocolTypeDto> GetAsync(Guid id) => protocolTypesAppService.GetAsync(id);


        [HttpPost]
        public virtual Task<ProtocolTypeDto> CreateAsync(ProtocolTypeCreateDto input) => protocolTypesAppService.CreateAsync(input);


        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProtocolTypeDto> UpdateAsync(ProtocolTypeUpdateDto input) => protocolTypesAppService.UpdateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => protocolTypesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => protocolTypesAppService.GetDownloadTokenAsync();


        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> protocolTypeIds) => protocolTypesAppService.DeleteByIdsAsync(protocolTypeIds);


        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetProtocolTypesInput input) => protocolTypesAppService.DeleteAllAsync(input);
    }
}
