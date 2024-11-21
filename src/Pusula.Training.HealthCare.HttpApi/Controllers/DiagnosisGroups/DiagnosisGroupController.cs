using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Controllers.DiagnosisGroups
{
    [RemoteService]
    [Area("app")]
    [ControllerName("DiagnosisGroups")]
    [Route("api/app/diagnosisGroups")]
    public class DiagnosisGroupController(IDiagnosisGroupsAppService diagnosisGroupsAppService) : HealthCareController, IDiagnosisGroupsAppService
    {
        [HttpGet]
        public Task<PagedResultDto<DiagnosisGroupDto>> GetListAsync(GetDiagnosisGroupsInput input) => diagnosisGroupsAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public Task<DiagnosisGroupDto> GetAsync(Guid id) => diagnosisGroupsAppService.GetAsync(id);


        [HttpPost]
        public Task<DiagnosisGroupDto> CreateAsync(DiagnosisGroupCreateDto input) => diagnosisGroupsAppService.CreateAsync(input);

        [HttpPut]
        public Task<DiagnosisGroupDto> UpdateAsync(DiagnosisGroupUpdateDto input) => diagnosisGroupsAppService.UpdateAsync(input);


        [HttpDelete]
        [Route("{id}")]
        public void DeleteAsync(Guid id) => diagnosisGroupsAppService.DeleteAsync(id);

        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetDiagnosisGroupsInput input) => diagnosisGroupsAppService.DeleteAllAsync(input);


        [HttpDelete]
        [Route("")]
        public Task DeleteByIdsAsync(List<Guid> diagnosisGroupIds) => diagnosisGroupsAppService.DeleteByIdsAsync(diagnosisGroupIds);


    }
}
