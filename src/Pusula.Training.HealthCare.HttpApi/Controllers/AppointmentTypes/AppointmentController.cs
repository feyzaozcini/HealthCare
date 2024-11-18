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
    public class AppointmentController(IAppointmentTypeAppService appointmentTypeAppService)
            : HealthCareController, IAppointmentTypeAppService
    {
        public Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentTypeDeleteDto> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentTypeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentTypeDto> UpdateAsync(AppointmentTypeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
