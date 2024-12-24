using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Core.Helpers.Concretes;
using Pusula.Training.HealthCare.Core.Rules.Appointments;
using Pusula.Training.HealthCare.Core.Rules.BlackLists;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Distributed;

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
        IAppointmentTypeRepository appointmentTypeRepository,
        IAppointmentBusinessRules appointmentBusinessRules,
        IBlackListBusinessRules blackListBusinessRules,
        IAppointmentRuleRepository appointmentRuleRepository,
        IDistributedEventBus distributedEventBus,
        IDistributedCache distributedCache,
        IDistributedCache<PagedResultDto<LookupDto<Guid>>, string> departmentDistributedCache //department için cache
        ) : HealthCareAppService, IAppointmentsAppService
    {

        public virtual async Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>> GetListAsync(GetAppointmentsInput input)
        {
            var totalCount = await appointmentRepository.GetCountAsync(input.FilterText, input.StartDate, input.EndDate, input.Note, input.AppointmentStatus, input.IsBlock,input.PatientId, input.DoctorId, input.DepartmentId, input.AppointmentTypeId);
            var items = await appointmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartDate, input.EndDate, input.Note, input.AppointmentStatus,input.IsBlock,input.PatientId, input.DoctorId, input.DepartmentId, input.AppointmentTypeId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        //Departmanlar için cache işlemi yapıldı
        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var cacheHelper = new CacheHelper<LookupDto<Guid>, LookupRequestDto>(
            distributedCache,
            Logger
            );

            return await cacheHelper.GetOrAddLookupAsync<Guid>(
                async () =>
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
                },
                input,
                "Departments:Lookup",
                TimeSpan.FromMinutes(30)
            );
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
        public virtual async Task<AppointmentDto> CreateAsync(AppointmentCreateDto input)
        {
            await CheckRulesAsync(input);
            // AppointmentType üzerinden DoctorAppointmentTypes'a eriş
            var appointmentType = await appointmentTypeRepository.GetAsync(input.AppointmentTypeId);

            //Doktorun aynı zamanlarda randevusu olup olmadığı kontrol edilir
            await appointmentBusinessRules.AppointmentDatesCannotOverlapForDoctor(input.DoctorId,input.StartDate, input.EndDate);
            
            //Hastanın black listte olup olmadığı kontrol edilir
            await blackListBusinessRules.ValidateBlackList(input.PatientId, input.DoctorId);

            //Geçmiş zamanlı randevu alınamaz end date, datetime.now'dan büyük olsa bile start date geçmiş olabilir
            //await appointmentBusinessRules.AppointmentCannotCreatePastTime(input.StartDate);

            // Doktorun bu appointment type ile ilişkisini kontrol et
            var doctorAppointmentType = appointmentType.DoctorAppointmentTypes
                .FirstOrDefault(dat => dat.DoctorId == input.DoctorId);

            //Doktorların randevu süreleri değişebilir onları otomatil set edilmesi için düzenlendi(default 15 dk)
            var startDate = input.StartDate;
            var endDate = startDate.AddMinutes(appointmentType.DurationInMinutes);

            var appointment = await appointmentManager.CreateAsync(
                input.PatientId,
                input.DoctorId,
                input.DepartmentId,
                input.AppointmentTypeId,
                startDate,
                endDate,
                input.Note,
                input.AppointmentStatus,
                input.IsBlock
            );

            //Oluşturulan randevu bilgileri mail atılır
            var patient = await patientRepository.GetAsync(input.PatientId);
            var department = await departmentRepository.GetAsync(input.DepartmentId);

            await distributedEventBus.PublishAsync(new AppointmentSendMailEto
            {
                Email = patient.Email,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                DepartmentName = department.Name,
                StartDate = startDate,
                EndDate = endDate
            });
            
            return ObjectMapper.Map<Appointment, AppointmentDto>(appointment);
        }
       
        [Authorize(HealthCarePermissions.Appointments.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await appointmentRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Appointments.Edit)]
        public virtual async Task<AppointmentDto> UpdateAsync(AppointmentUpdateDto input)
        {
            if(input.AppointmentStatus == AppointmentStatus.Cancelled)
            {
                await appointmentRepository.DeleteAsync(input.Id);
            }
            return ObjectMapper.Map<Appointment, AppointmentDto>(
                await appointmentManager.UpdateAsync(
                    input.Id,
                    input.PatientId,
                    input.DoctorId,
                    input.DepartmentId,
                    input.AppointmentTypeId,
                    input.StartDate,
                    input.EndDate,
                    input.Note,
                    input.AppointmentStatus,
                    input.IsBlock
                ));
        }

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

        public virtual async Task CheckRulesAsync(AppointmentCreateDto input)
        {
            var patient = await patientRepository.GetAsync(input.PatientId);
            var patientAge = DateTime.Now.Year - patient.BirthDate.Year;
            var patientGender=patient.Gender.ToString();

            //seçilen departman ve doktor için tanımlanmış kuralların getirilmesi(Hepsi gezilmiyor sadece randevu için seçilenler)
            var departmentRules = await appointmentRuleRepository.GetRulesForDepartmentAsync(input.DepartmentId);
            var doctorRules = await appointmentRuleRepository.GetRulesForDoctorAsync(input.DoctorId);
            var allRules = departmentRules.Concat(doctorRules).ToList();


            foreach (var rule in allRules.Where(r =>
              (r.MinAge.HasValue && patientAge < r.MinAge) ||
              (r.MaxAge.HasValue && patientAge > r.MaxAge) ||
              (r.Gender.HasValue && patientGender != r.Gender.ToString())))
            {
                await appointmentBusinessRules.AppointmentCannotCreate();
            }

        }
    }
}
