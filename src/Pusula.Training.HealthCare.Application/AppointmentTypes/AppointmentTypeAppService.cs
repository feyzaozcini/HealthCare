using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.AppointmentTypes.Default)]
    public class AppointmentTypeAppService(IAppointmentTypeRepository appointmentTypeRepository,
    AppointmentTypeManager appointmentTypeManager,
    IDistributedCache<AppointmentTypeDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, IAppointmentTypeAppService
    {

        [Authorize(HealthCarePermissions.AppointmentTypes.Create)]
        public virtual async Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input)
        {
            var appointmentType = await appointmentTypeManager.CreateAsync(
            input.Name,
            input.DurationInMinutes,
            input.DoctorIds
            );

            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(appointmentType);
        }

        [Authorize(HealthCarePermissions.AppointmentTypes.Delete)]
        public virtual async Task<AppointmentTypeDeleteDto> DeleteAsync(Guid id)
        {
            AppointmentType? appointmentType = await appointmentTypeRepository.GetAsync(
            predicate: t => t.Id == id
            );

            await appointmentTypeRepository.DeleteAsync(id);

            AppointmentTypeDeleteDto response = ObjectMapper.Map<AppointmentType, AppointmentTypeDeleteDto>(appointmentType);
            response.Message = HealthCareDomainErrorCodes.DeleteMessage;

            return response;
        }

        public virtual async Task<AppointmentTypeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(await appointmentTypeRepository.GetAsync(id));
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new AppointmentTypeDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new Shared.DownloadTokenResultDto
            {
                Token = token
            };

        }

        public virtual async Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input)
        {
            var totalCount = await appointmentTypeRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await appointmentTypeRepository.GetListAsync(input.FilterText, input.Name,input.DurationInMinutes ,input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentType>, List<AppointmentTypeDto>>(items)
            };
        }

        public virtual async Task<List<DoctorWithNavigationPropertiesDto>> GetDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId)
        {
            var doctors = await appointmentTypeRepository.GetDoctorsByAppointmentTypeIdAsync(appointmentTypeId);

            var doctorDtos = ObjectMapper.Map<List<DoctorWithNavigationProperties>, List<DoctorWithNavigationPropertiesDto>>(doctors);

            return doctorDtos;
        }

        [Authorize(HealthCarePermissions.AppointmentTypes.Edit)]
        public virtual async Task<AppointmentTypeDto> UpdateAsync(AppointmentTypeUpdateDto input)
        {
            await appointmentTypeRepository.RemoveAllDoctorsByAppointmentTypeIdAsync(input.Id);
            var appointmentType = await appointmentTypeManager.UpdateAsync(
                    input.Id,
                    input.Name,
                    input.DurationInMinutes,
                    input.DoctorIds
                    );

            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(appointmentType);
        }
    }
}
