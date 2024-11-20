using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Appointments
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Appointments.Default)]
    public class AppointmentsAppService(
        IAppointmentRepository appointmentRepository,
        AppointmentManager appointmentManager,
        IDistributedCache<AppointmentDownloadTokenCacheItem, string> downloadTokenCache,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IDepartmentRepository departmentRepository,
        IAppointmentTypeRepository appointmentTypeRepository
        ) : HealthCareAppService, IAppointmentsAppService
    {

        public virtual async Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>> GetListAsync(GetAppointmentsInput input)
        {
            var totalCount = await appointmentRepository.GetCountAsync(input.FilterText, input.StartDate, input.EndDate, input.Note, input.AppointmentStatus, input.PatientId, input.DoctorId, input.DepartmentId, input.AppointmentTypeId);
            var items = await appointmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartDate, input.EndDate, input.Note, input.AppointmentStatus, input.PatientId, input.DoctorId, input.DepartmentId, input.AppointmentTypeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentWithNavigationProperties>, List<AppointmentWithNavigationPropertiesDto>>(items)
            };
        }
        public virtual async Task<AppointmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var appointment = await appointmentRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>(appointment);
        }

        public virtual async Task<AppointmentDto> GetAsync(Guid id) => ObjectMapper.Map<Appointment, AppointmentDto>(
                await appointmentRepository.GetAsync(id));


        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input)
        {
            var query = (await patientRepository.GetQueryableAsync())
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                               x => x.FirstName != null && x.FirstName.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Patient>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Patient>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input)
        {
            var query = (await doctorRepository.GetQueryableAsync())
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                               x => x.IdentityNumber != null && x.IdentityNumber.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Doctor>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Doctor>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await departmentRepository.GetQueryableAsync())
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                               x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentTypeLookupAsync(LookupRequestDto input)
        {
            var query = (await appointmentTypeRepository.GetQueryableAsync())
                           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                               x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<AppointmentType>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentType>, List<LookupDto<Guid>>>(lookupData)
            };
        }


        [Authorize(HealthCarePermissions.Appointments.Create)]
        public virtual async Task<AppointmentDto> CreateAsync(AppointmentCreateDto input) => ObjectMapper.Map<Appointment, AppointmentDto>(
            await appointmentManager.CreateAsync(
                input.PatientId,
                input.DoctorId,
                input.DepartmentId,
                input.AppointmentTypeId,
                input.StartDate,
                input.EndDate,
                input.Note,
                input.AppointmentStatus
            ));




        [Authorize(HealthCarePermissions.Appointments.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await appointmentRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Appointments.Edit)]
        public virtual async Task<AppointmentDto> UpdateAsync(AppointmentUpdateDto input) => ObjectMapper.Map<Appointment,AppointmentDto>(
            await appointmentManager.UpdateAsync(
                input.Id,
                input.PatientId,
                input.DoctorId,
                input.DepartmentId,
                input.AppointmentTypeId,
                input.StartDate,
                input.EndDate,
                input.Note,
                input.AppointmentStatus
            ));

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
             token,
             new AppointmentDownloadTokenCacheItem { Token = token },
             new DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
             });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

    }
}
