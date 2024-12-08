using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
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

namespace Pusula.Training.HealthCare.AppointmentRules
{
    [RemoteService(IsEnabled = false)]
    public class AppointmentRulesAppService(
        IAppointmentRuleRepository appointmentRuleRepository,
        AppointmentRuleManager appointmentRuleManager,
        IDistributedCache<AppointmentRuleDownloadTokenCacheItem, string> downloadTokenCache,
        IDoctorRepository doctorRepository,
        IDepartmentRepository departmentRepository
        ) : HealthCareAppService, IAppointmentRulesAppService
    {

        public virtual async Task<PagedResultDto<AppointmentRuleWithNavigationPropertiesDto>> GetListAsync(GetAppointmentRulesInput input)
        {
            var totalCount = await appointmentRuleRepository.GetCountAsync(input.FilterText, input.DoctorId,input.DepartmentId,input.Gender,input.Age,input.Description);
            var items = await appointmentRuleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DoctorId,input.DepartmentId,input.Gender,input.Age,input.Description ,input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentRuleWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentRuleWithNavigationProperties>, List<AppointmentRuleWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<AppointmentRuleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var appointmentRule = await appointmentRuleRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<AppointmentRuleWithNavigationProperties, AppointmentRuleWithNavigationPropertiesDto>(appointmentRule);
        }

        public virtual async Task<List<AppointmentRuleDto>> GetRulesForDepartmentAsync(Guid departmentId)
        {
            var rules = await appointmentRuleRepository.GetRulesForDepartmentAsync(departmentId);
            return ObjectMapper.Map<List<AppointmentRule>, List<AppointmentRuleDto>>(rules);
        }

        public virtual async Task<List<AppointmentRuleDto>> GetRulesForDoctorAsync(Guid doctorId)
        {
            var rules = await appointmentRuleRepository.GetRulesForDoctorAsync(doctorId);
            return ObjectMapper.Map<List<AppointmentRule>, List<AppointmentRuleDto>>(rules);
        }

        public virtual async Task<AppointmentRuleDto> GetAsync(Guid id) => ObjectMapper.Map<AppointmentRule, AppointmentRuleDto>(
                await appointmentRuleRepository.GetAsync(id));


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
        public virtual async Task<AppointmentRuleDto> CreateAsync(AppointmentRuleCreateDto input)
        {
            var appointmentRule = await appointmentRuleManager.CreateAsync(
                input.DoctorId,
                input.DepartmentId,
                input.Gender,
                input.Age,
                input.Description
            );

            return ObjectMapper.Map<AppointmentRule, AppointmentRuleDto>(appointmentRule);
        }
        public virtual async Task<AppointmentRuleDto> UpdateAsync(AppointmentRuleUpdateDto input)
        {
            return ObjectMapper.Map<AppointmentRule, AppointmentRuleDto>(
                await appointmentRuleManager.UpdateAsync(
                    input.Id,
                    input.DoctorId,
                    input.DepartmentId,
                    input.Gender,
                    input.Age,
                    input.Description
                ));
        }
        public virtual async Task DeleteAsync(Guid id) => await appointmentRuleRepository.DeleteAsync(id);

        public virtual async  Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
             token,
             new AppointmentRuleDownloadTokenCacheItem { Token = token },
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
