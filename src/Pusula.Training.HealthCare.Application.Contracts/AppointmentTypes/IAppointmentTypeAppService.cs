using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public interface IAppointmentTypeAppService : IApplicationService
    {
        Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input);
        Task<AppointmentTypeDto> GetAsync(Guid id);
        Task<AppointmentTypeDeleteDto> DeleteAsync(Guid id);
        Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input);
        Task<AppointmentTypeDto> UpdateAsync(AppointmentTypeUpdateDto input);
        Task<List<DoctorWithNavigationPropertiesDto>> GetDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
