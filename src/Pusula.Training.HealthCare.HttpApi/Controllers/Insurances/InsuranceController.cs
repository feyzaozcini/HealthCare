using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Pusula.Training.HealthCare.Insurances;

namespace Pusula.Training.HealthCare.Controllers.Insurances
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Insurance")]
    [Route("api/app/insurances")]
    public class InsuranceController(IInsurancesAppService insurancesAppService)
    : HealthCareController, IInsurancesAppService
    {
        [HttpGet]
        public virtual Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input) => insurancesAppService.GetListAsync(input);


        [HttpGet]
        [Route("{id}")]
        public virtual Task<InsuranceDto> GetAsync(Guid id) => insurancesAppService.GetAsync(id);


        [HttpPost]
        public virtual Task<InsuranceDto> CreateAsync(InsuranceCreateDto input) => insurancesAppService.CreateAsync(input);


        [HttpPut]
        [Route("{id}")]
        public virtual Task<InsuranceDto> UpdateAsync(InsuranceUpdateDto input) => insurancesAppService.UpdateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => insurancesAppService.DeleteAsync(id);


        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => insurancesAppService.GetDownloadTokenAsync();


        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> insuranceIds) => insurancesAppService.DeleteByIdsAsync(insuranceIds);


        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetInsurancesInput input) => insurancesAppService.DeleteAllAsync(input);
    }
}
