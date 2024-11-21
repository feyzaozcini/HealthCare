using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public interface IDiagnosisGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<DiagnosisGroupDto>> GetListAsync(GetDiagnosisGroupsInput input);
        Task<DiagnosisGroupDto> GetAsync(Guid id);
        void DeleteAsync(Guid id);
        Task<DiagnosisGroupDto> CreateAsync(DiagnosisGroupCreateDto input);
        Task<DiagnosisGroupDto> UpdateAsync(DiagnosisGroupUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> diagnosisGroupIds);
        Task DeleteAllAsync(GetDiagnosisGroupsInput input);
    
    }
}
