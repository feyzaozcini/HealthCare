using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.AppointmentRules
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AppointmentRule")]
    [Route("api/app/appointmentRule")]
    public class AppointmentRuleController(IAppointmentRulesAppService appointmentRulesAppService) : HealthCareController, IAppointmentRulesAppService
    {
        [HttpPost]  
        public Task<AppointmentRuleDto> CreateAsync(AppointmentRuleCreateDto input) => appointmentRulesAppService.CreateAsync(input);

        [HttpDelete]
        public Task DeleteAsync(Guid id) => appointmentRulesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("{id}")]
        public Task<AppointmentRuleDto> GetAsync(Guid id) => appointmentRulesAppService.GetAsync(id);

        [HttpGet]
        [Route("department-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input) => appointmentRulesAppService.GetDepartmentLookupAsync(input);

        [HttpGet]
        [Route("doctor-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input) => appointmentRulesAppService.GetDoctorLookupAsync(input);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => appointmentRulesAppService.GetDownloadTokenAsync();

        [HttpGet]
        public Task<PagedResultDto<AppointmentRuleWithNavigationPropertiesDto>> GetListAsync(GetAppointmentRulesInput input) => appointmentRulesAppService.GetListAsync(input);

        [HttpGet]
        [Route("rules-for-department/{departmentId}")]
        public Task<List<AppointmentRuleDto>> GetRulesForDepartmentAsync(Guid departmentId) => appointmentRulesAppService.GetRulesForDepartmentAsync(departmentId);

        [HttpGet]
        [Route("rules-for-doctor/{doctorId}")]
        public Task<List<AppointmentRuleDto>> GetRulesForDoctorAsync(Guid doctorId) => appointmentRulesAppService.GetRulesForDoctorAsync(doctorId);

        [HttpGet]
        [Route("{id}/with-navigation-properties")]
        public Task<AppointmentRuleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => appointmentRulesAppService.GetWithNavigationPropertiesAsync(id);

        [HttpPut]
        public Task<AppointmentRuleDto> UpdateAsync(AppointmentRuleUpdateDto input) => appointmentRulesAppService.UpdateAsync(input);
    }
}
