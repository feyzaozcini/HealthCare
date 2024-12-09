using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Pusula.Training.HealthCare.Core.Rules.PainTypes;
using Pusula.Training.HealthCare.Core.Rules.TestGroupItems;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.PainTypes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.PainTypes.Default)]
    public class PainTypesAppService(IPainTypeRepository painTypeRepository,
        PainTypeManager painTypeManager,
        IPainTypeBusinessRules painTypeBusinessRules)
        : HealthCareAppService, IPainTypesAppService
    {

        [Authorize(HealthCarePermissions.PainTypes.Create)]

        public async Task<PainTypeDto> CreateAsync(PainTypeCreateDto input)
        {
            await painTypeBusinessRules.PainTypeNameDuplicatedAsync(input.Name);
            return ObjectMapper.Map<PainType, PainTypeDto>(await painTypeManager.CreateAsync(input.Name));
        }
           


        [Authorize(HealthCarePermissions.PainTypes.Delete)]
        public async Task DeleteAllAsync(GetPainTypesInput input) => await painTypeRepository.DeleteAllAsync(input.FilterText, input.Name);


        [Authorize(HealthCarePermissions.PainTypes.Delete)]
        public async Task DeleteAsync(Guid id) => await painTypeRepository.DeleteAsync(id);

        [Authorize(HealthCarePermissions.PainTypes.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> painTypeIds) => await painTypeRepository.DeleteManyAsync(painTypeIds);

        public async Task<PainTypeDto> GetAsync(Guid id) => ObjectMapper.Map<PainType, PainTypeDto>(await painTypeRepository.GetAsync(id));

        public async Task<PagedResultDto<PainTypeDto>> GetListAsync(GetPainTypesInput input)
        {
            var totalCount = await painTypeRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await painTypeRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PainTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PainType>, List<PainTypeDto>>(items)
            };
        }

        [Authorize(HealthCarePermissions.PainTypes.Edit)]
        public async Task<PainTypeDto> UpdateAsync(PainTypeUpdateDto input) => ObjectMapper.Map<PainType, PainTypeDto>(await painTypeManager.UpdateAsync(input.Id, input.Name));
    }
}
