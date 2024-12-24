using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.Appointments
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Appointment")]
    [Route("api/app/appointment")]
    public class AppointmentController : HealthCareController, IAppointmentsAppService
    {
        protected IAppointmentsAppService _appointmentsAppService;

        public AppointmentController(IAppointmentsAppService appointmentsAppService)
        {
            _appointmentsAppService = appointmentsAppService;
        }
        [HttpGet]
        [Route("id")]
        public Task<AppointmentDto> GetAsync(Guid id) => _appointmentsAppService.GetAsync(id);

        [HttpGet]
        public Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>> GetListAsync(GetAppointmentsInput input) => _appointmentsAppService.GetListAsync(input);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<AppointmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => _appointmentsAppService.GetWithNavigationPropertiesAsync(id);

        [HttpGet]
        [Route("appointmentType-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentTypeLookupAsync(LookupRequestDto input) => _appointmentsAppService.GetAppointmentTypeLookupAsync(input);

        [HttpGet]
        [Route("department-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input) => _appointmentsAppService.GetDepartmentLookupAsync(input);

        [HttpGet]
        [Route("doctor-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input) => _appointmentsAppService.GetDoctorLookupAsync(input);
        
        [HttpGet]
        [Route("patient-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input) => _appointmentsAppService.GetPatientLookupAsync(input);

        [HttpPost]
        public Task<AppointmentDto> CreateAsync(AppointmentCreateDto input) => _appointmentsAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id) => _appointmentsAppService.DeleteAsync(id);

        [HttpPut]
        public Task<AppointmentDto> UpdateAsync(AppointmentUpdateDto input) => _appointmentsAppService.UpdateAsync(input);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => _appointmentsAppService.GetDownloadTokenAsync();
    }
}
