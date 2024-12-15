using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Core.Rules.DoctorWorkSchedules;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkSchedulesAppService(
        IDoctorWorkScheduleRepository doctorWorkScheduleRepository,
        DoctorWorkScheduleManager doctorWorkScheduleManager,
        IDoctorRepository doctorRepository,
        IDistributedCache<DownloadTokenCacheItem, string> downloadTokenCache,
        IDoctorWorkScheduleBusinessRules doctorWorkScheduleBusinessRules
        ) : HealthCareAppService, IDoctorWorkSchedulesAppService
    {

        public virtual async Task<PagedResultDto<DoctorWorkScheduleWithNavigationPropertiesDto>> GetListAsync(GetDoctorWorkSchedulesInput input)
        {
            var totalCount = await doctorWorkScheduleRepository.GetCountAsync(input.FilterText, input.DoctorId, input.WorkingDays,input.StartHour,input.EndHour);
            var items = await doctorWorkScheduleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DoctorId,input.WorkingDays, input.StartHour, input.EndHour, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DoctorWorkScheduleWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DoctorWorkScheduleWithNavigationProperties>, List<DoctorWorkScheduleWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DoctorWorkScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var doctorWorkSchedule = await doctorWorkScheduleRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<DoctorWorkScheduleWithNavigationProperties, DoctorWorkScheduleWithNavigationPropertiesDto>(doctorWorkSchedule);
        }

        //Doctor id sine göre saatleri ve günleri getirir
        public virtual async Task<List<DoctorWorkScheduleDto>> GetScheduleForDoctorAsync(Guid doctorId)
        {
            var schedules = await doctorWorkScheduleRepository.GetWorkScheduleForDoctorAsync(doctorId);
            return ObjectMapper.Map<List<DoctorWorkSchedule>, List<DoctorWorkScheduleDto>>(schedules);
        }

        public virtual async Task<DoctorWorkScheduleDto> GetAsync(Guid id) => ObjectMapper.Map<DoctorWorkSchedule, DoctorWorkScheduleDto>(
                await doctorWorkScheduleRepository.GetAsync(id));

        public virtual async Task<DoctorWorkScheduleDto> CreateAsync(DoctorWorkScheduleCreateDto input)
        {
            await doctorWorkScheduleBusinessRules.DoctorWorkScheduleCannotCreate(input.DoctorId);

            var doctorWorkSchedule = await doctorWorkScheduleManager.CreateAsync(
                input.DoctorId,
                input.WorkingDays,
            input.StartHour,
            input.EndHour
            );

            return ObjectMapper.Map<DoctorWorkSchedule, DoctorWorkScheduleDto>(doctorWorkSchedule);
        }

        public virtual async Task<DoctorWorkScheduleDto> UpdateAsync(DoctorWorkScheduleUpdateDto input)
        {
            return ObjectMapper.Map<DoctorWorkSchedule, DoctorWorkScheduleDto>(await doctorWorkScheduleManager.UpdateAsync(
                input.Id,
                input.DoctorId,
                input.WorkingDays,
                input.StartHour,
                input.EndHour
            ));
        }

        public virtual async Task DeleteAsync(Guid id) => await doctorWorkScheduleRepository.DeleteAsync(id);

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

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new DownloadTokenCacheItem { Token = token },
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
