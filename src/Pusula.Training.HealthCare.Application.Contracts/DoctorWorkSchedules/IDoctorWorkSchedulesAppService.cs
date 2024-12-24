using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public interface IDoctorWorkSchedulesAppService : IApplicationService
    {
        Task<PagedResultDto<DoctorWorkScheduleWithNavigationPropertiesDto>> GetListAsync(GetDoctorWorkSchedulesInput input);

        Task<DoctorWorkScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DoctorWorkScheduleDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DoctorWorkScheduleDto> CreateAsync(DoctorWorkScheduleCreateDto input);

        Task<DoctorWorkScheduleDto> UpdateAsync(DoctorWorkScheduleUpdateDto input);

        //Doktora ait saatleri getirmek için kullanılır
        Task<List<DoctorWorkScheduleDto>> GetScheduleForDoctorAsync(Guid doctorId);

        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
