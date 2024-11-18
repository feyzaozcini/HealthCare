using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.AppointmentTypes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AppointmentType")]
    [Route("api/app/appointmentType")]
    public class AppointmentTypeController(IAppointmentTypeAppService appointmentTypeAppService)
            : HealthCareController, IAppointmentTypeAppService
    {
        [HttpPost]
        public Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input) => appointmentTypeAppService.CreateAsync(input);

        [HttpDelete]
        [Route("{id}")]
        public Task<AppointmentTypeDeleteDto> DeleteAsync(Guid id) => appointmentTypeAppService.DeleteAsync(id);


        [HttpGet]
        [Route("{id}")]
        public Task<AppointmentTypeDto> GetAsync(Guid id) => appointmentTypeAppService.GetAsync(id);

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync() => appointmentTypeAppService.GetDownloadTokenAsync();

        [HttpGet]
        public Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input) => appointmentTypeAppService.GetListAsync(input);

        [HttpPut]
        public Task<AppointmentTypeDto> UpdateAsync(AppointmentTypeUpdateDto input) => appointmentTypeAppService.UpdateAsync(input);
    }
}
