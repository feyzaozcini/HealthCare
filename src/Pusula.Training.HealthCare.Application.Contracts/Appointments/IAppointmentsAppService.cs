using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Appointments
{
    public interface IAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>> GetListAsync(GetAppointmentsInput input);

        Task<AppointmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AppointmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentTypeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<AppointmentDto> CreateAsync(AppointmentCreateDto input);

        Task<AppointmentDto> UpdateAsync(AppointmentUpdateDto input);

        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
