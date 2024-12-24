using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.DoctorWorkSchedules
{
    [RemoteService]
    [Area("app")]
    [ControllerName("DoctorWorkSchedules")]
    [Route("api/app/doctorWorkSchedules")]

    public class DoctorWorkScheduleController(IDoctorWorkSchedulesAppService doctorWorkSchedulesAppService) : HealthCareController, IDoctorWorkSchedulesAppService
    {
        [HttpGet]
        [Route("{id}")]
        public Task<DoctorWorkScheduleDto> GetAsync(Guid id) => doctorWorkSchedulesAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<DoctorWorkScheduleWithNavigationPropertiesDto>> GetListAsync(GetDoctorWorkSchedulesInput input) => doctorWorkSchedulesAppService.GetListAsync(input);
        [HttpGet]
        [Route("{id}/with-navigation-properties")]
        public Task<DoctorWorkScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => doctorWorkSchedulesAppService.GetWithNavigationPropertiesAsync(id);

        [HttpPost]
        public Task<DoctorWorkScheduleDto> CreateAsync(DoctorWorkScheduleCreateDto input) => doctorWorkSchedulesAppService.CreateAsync(input);

        [HttpPut]
        public Task<DoctorWorkScheduleDto> UpdateAsync(DoctorWorkScheduleUpdateDto input) => doctorWorkSchedulesAppService.UpdateAsync(input);

        [HttpDelete]
        public Task DeleteAsync(Guid id) => doctorWorkSchedulesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("doctor-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input) => doctorWorkSchedulesAppService.GetDoctorLookupAsync(input);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => doctorWorkSchedulesAppService.GetDownloadTokenAsync();

        [HttpGet]
        [Route("schedule-for-doctor/{doctorId}")]
        public Task<List<DoctorWorkScheduleDto>> GetScheduleForDoctorAsync(Guid doctorId) => doctorWorkSchedulesAppService.GetScheduleForDoctorAsync(doctorId);

    }
}
