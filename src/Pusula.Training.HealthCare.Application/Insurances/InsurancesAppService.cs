using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Insurances
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Insurances.Default)]
    public class InsurancesAppService(IInsuranceRepository insuranceRepository,
        InsuranceManager insuranceManager, IDistributedCache<InsuranceDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IInsurancesAppService
    {
        public virtual async Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input)
        {
            var totalCount = await insuranceRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await insuranceRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<InsuranceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Insurance>, List<InsuranceDto>>(items)
            };
        }

        public virtual async Task<InsuranceDto> GetAsync(Guid id) => ObjectMapper.Map<Insurance, InsuranceDto>(await insuranceRepository.GetAsync(id));


        [Authorize(HealthCarePermissions.Insurances.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await insuranceRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Insurances.Create)]
        public virtual async Task<InsuranceDto> CreateAsync(InsuranceCreateDto input) => ObjectMapper.Map<Insurance, InsuranceDto>(await insuranceManager.CreateAsync(input.Name));


        [Authorize(HealthCarePermissions.Insurances.Edit)]
        public virtual async Task<InsuranceDto> UpdateAsync(InsuranceUpdateDto input) => ObjectMapper.Map<Insurance, InsuranceDto>(await insuranceManager.UpdateAsync(input.Id, input.Name, input.ConcurrencyStamp));


        [Authorize(HealthCarePermissions.Insurances.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> insuranceIds) => await insuranceRepository.DeleteManyAsync(insuranceIds);


        [Authorize(HealthCarePermissions.Insurances.Delete)]
        public virtual async Task DeleteAllAsync(GetInsurancesInput input) => await insuranceRepository.DeleteAllAsync(input.FilterText, input.Name);


        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new InsuranceDownloadTokenCacheItem { Token = token },
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
