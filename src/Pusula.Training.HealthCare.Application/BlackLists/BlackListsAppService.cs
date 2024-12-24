using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core.Rules.BlackLists;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.BlackLists
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.BlackLists.Default)]
    public class BlackListsAppService(
        IBlackListRepository blackListRepository,
        BlackListManager blackListManager,
        IDistributedCache<BlackListDownloadTokenCacheItem, string> downloadTokenCache,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IBlackListBusinessRules blackListBusinessRules) : HealthCareAppService, IBlackListsAppService
    {
        public virtual async Task<PagedResultDto<BlackListWithNavigationPropertiesDto>> GetListAsync(GetBlackListInput input)
        {
            var totalCount = await blackListRepository.GetCountAsync(input.FilterText, input.BlackListStatus, input.Note, input.PatientId,input.DoctorId);
            var items = await blackListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.BlackListStatus, input.Note, input.PatientId, input.DoctorId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BlackListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BlackListWithNavigationProperties>, List<BlackListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<BlackListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var blackList = await blackListRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<BlackListWithNavigationProperties, BlackListWithNavigationPropertiesDto>(blackList);
        }

        public virtual async Task<BlackListDto> GetAsync(Guid id) => ObjectMapper.Map<BlackList, BlackListDto>(
                await blackListRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.BlackLists.Create)]
        public virtual async Task<BlackListDto> CreateAsync(BlackListCreateDto input)
        {
            await blackListBusinessRules.DublicateBlackList(input.PatientId, input.DoctorId);
            var blackList = await blackListManager.CreateAsync(
                input.PatientId,
                input.DoctorId,
                input.BlackListStatus,
                input.Note
            );

            return ObjectMapper.Map<BlackList, BlackListDto>(blackList);
        }

        [Authorize(HealthCarePermissions.BlackLists.Edit)]
        public virtual async Task<BlackListDto> UpdateAsync(BlackListUpdateDto input)
        {
            return ObjectMapper.Map<BlackList, BlackListDto>(await blackListManager.UpdateAsync(
                input.Id,
                input.PatientId,
                input.DoctorId,
                input.BlackListStatus,
                input.Note
            ));
        }

        [Authorize(HealthCarePermissions.BlackLists.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await blackListRepository.DeleteAsync(id);

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

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new BlackListDownloadTokenCacheItem { Token = token },
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
