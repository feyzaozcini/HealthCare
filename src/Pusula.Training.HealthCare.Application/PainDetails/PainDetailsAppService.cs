using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Shared;
using Volo.Abp.Application.Dtos;
using Pusula.Training.HealthCare.PainTypes;
using System.Linq.Dynamic.Core;

namespace Pusula.Training.HealthCare.PainDetails
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class PainDetailsAppService(
    IPainDetailRepository painDetailRepository,
    PainDetailManager painDetailManager,
    IPainTypeRepository painTypeRepository) : HealthCareAppService, IPainDetailsAppService
    {
        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<PainDetailDto> CreateAsync(PainDetailCreateDto input)
        {
            var painDetail = await painDetailManager.CreateAsync(input.Area,input.ProtocolId, input.Score,input.PainTypeId,
                input.Description, input.PainRhythm, input.OtherPain, input.StartDate);

            return ObjectMapper.Map<PainDetail, PainDetailDto>(painDetail);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await painDetailRepository.DeleteAsync(id);

        public async Task<PainDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PainDetail, PainDetailDto>(await painDetailRepository.GetAsync(id));
        }

        public async Task<PainDetailDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var painDetail = await painDetailRepository.GetWithNavigationPropertiesAsync(id);
            return ObjectMapper.Map<PainDetail, PainDetailDto>(painDetail);
        }

        public async Task<PainDetailDto> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId)
        {
            var painDetail = await painDetailRepository.GetWithNavigationPropertiesByProtocolIdAsync(protocolId);
            if (painDetail == null)
            {
                return new PainDetailDto();
            }
            return ObjectMapper.Map<PainDetail, PainDetailDto>(painDetail);
        }


        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<PainDetailDto> UpdateAsync(PainDetailUpdateDto input)
        {
            var painDetail = await painDetailManager.UpdateAsync(input.Id, input.Area, input.ProtocolId, input.Score, 
                input.PainTypeId,input.Description, input.PainRhythm, input.OtherPain, input.StartDate);

            return ObjectMapper.Map<PainDetail, PainDetailDto>(painDetail);
        }

        public  async Task<PagedResultDto<LookupDto<Guid>>> GetPainTypeLookupAsync(LookupRequestDto input)
        {
            var query = (await painTypeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PainType>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PainType>, List<LookupDto<Guid>>>(lookupData)
            };
        }
    }
}
