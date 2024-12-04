using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Core.Rules.Diagnoses;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Diagnoses
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Diagnosis.Default)]
    public class DiagnosisAppService(IDiagnosisRepository diagnosisRepository,
        DiagnosisManager diagnosisManager,
        IDiagnosisBusinessRules diagnosisBusinessRules)
        : HealthCareAppService, IDiagnosisAppService
    {
        public async Task<PagedResultDto<DiagnosisWithNavigationPropertiesDto>> GetListAsync(GetDiagnosisInput input)
        {
            var totalCount = await diagnosisRepository.GetCountAsync(input.FilterText, input.Name, input.Code, input.GroupId);

            var items = await diagnosisRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.GroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DiagnosisWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DiagnosisWithNavigationProperties>, List<DiagnosisWithNavigationPropertiesDto>>(items)
            };
        }

        [Authorize(HealthCarePermissions.Diagnosis.Create)]
        public async Task<DiagnosisDto> CreateAsync(DiagnosisCreateDto input)
        {
            await diagnosisBusinessRules.DiagnosisCodeDuplicatedAsync(input.Code);
            var diagnosis = await diagnosisManager.CreateAsync(input.Name, input.Code, input.GroupId);
            return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
        }


        [Authorize(HealthCarePermissions.Diagnosis.Delete)]
        public async Task DeleteAllAsync(GetDiagnosisInput input) => await diagnosisRepository.DeleteAllAsync(input.FilterText, input.Name, input.Code, input.GroupId);


        [Authorize(HealthCarePermissions.Diagnosis.Delete)]
        public async Task DeleteAsync(Guid id) => await diagnosisRepository.DeleteAsync(id);

        [Authorize(HealthCarePermissions.Diagnosis.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> diagnosesIds) => await diagnosisRepository.DeleteManyAsync(diagnosesIds);

        public async Task<DiagnosisDto> GetAsync(Guid id) => ObjectMapper.Map<Diagnosis, DiagnosisDto>(await diagnosisRepository.GetAsync(id));


        public async Task<DiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var diagnosis = await diagnosisRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<DiagnosisWithNavigationProperties, DiagnosisWithNavigationPropertiesDto>(diagnosis);
        }

        public async Task<DiagnosisDto> UpdateAsync(DiagnosisUpdateDto input) => ObjectMapper.Map<Diagnosis, DiagnosisDto>(
                await diagnosisManager.UpdateAsync(input.Id, input.Name, input.Code, input.GroupId));
    }
}
