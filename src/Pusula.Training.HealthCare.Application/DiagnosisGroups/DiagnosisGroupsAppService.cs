using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.DiagnosisGroups.Default)]
    public class DiagnosisGroupsAppService(IDiagnosisGroupRepository diagnosisGroupRepository,
        DiagnosisGroupManager diagnosisGroupManager)
        : HealthCareAppService, IDiagnosisGroupsAppService
    {
        public async Task<PagedResultDto<DiagnosisGroupDto>> GetListAsync(GetDiagnosisGroupsInput input)
        {
            var totalCount = await diagnosisGroupRepository.GetCountAsync(input.FilterText, input.Name, input.Code);
            var items = await diagnosisGroupRepository.GetListAsync(input.FilterText, input.Name, input.Code, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DiagnosisGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DiagnosisGroup>, List<DiagnosisGroupDto>>(items)
            };
        }
        public async Task<DiagnosisGroupDto> GetAsync(Guid id) => ObjectMapper.Map<DiagnosisGroup, DiagnosisGroupDto>(await diagnosisGroupRepository.GetAsync(id));

        [Authorize(HealthCarePermissions.DiagnosisGroups.Create)]
        public async Task<DiagnosisGroupDto> CreateAsync(DiagnosisGroupCreateDto input)
        {
            var diagnosisGroup = await diagnosisGroupManager.CreateAsync(input.Name, input.Code);

            return ObjectMapper.Map<DiagnosisGroup, DiagnosisGroupDto>(diagnosisGroup);
        }


        [Authorize(HealthCarePermissions.DiagnosisGroups.Edit)]
        public async Task<DiagnosisGroupDto> UpdateAsync(DiagnosisGroupUpdateDto input)
        {
            var title = await diagnosisGroupManager.UpdateAsync(input.Id, input.Name, input.Code);

            return ObjectMapper.Map<DiagnosisGroup, DiagnosisGroupDto>(title);
        }

        [Authorize(HealthCarePermissions.DiagnosisGroups.Delete)]
        public async Task DeleteAsync(Guid id) => await diagnosisGroupRepository.DeleteAsync(id);

        [Authorize(HealthCarePermissions.DiagnosisGroups.Delete)]
        public async Task DeleteAllAsync(GetDiagnosisGroupsInput input) => await diagnosisGroupRepository.DeleteAllAsync(input.FilterText, input.Name, input.Code);

        [Authorize(HealthCarePermissions.DiagnosisGroups.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> diagnosisGroupIds) => await diagnosisGroupRepository.DeleteManyAsync(diagnosisGroupIds);



    }
}
