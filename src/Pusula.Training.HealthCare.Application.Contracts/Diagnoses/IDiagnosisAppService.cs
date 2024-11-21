using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public interface IDiagnosisAppService : IApplicationService
    {
        Task<PagedResultDto<DiagnosisWithNavigationPropertiesDto>> GetListAsync(GetDiagnosisInput input);

        Task<DiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DiagnosisDto> GetAsync(Guid id);

        //Reminder: diagnosesgroup lookup if needed

        Task DeleteAsync(Guid id);

        Task<DiagnosisDto> CreateAsync(DiagnosisCreateDto input);

        Task<DiagnosisDto> UpdateAsync(DiagnosisUpdateDto input);

        Task DeleteByIdsAsync(List<Guid> diagnosesIds);

        Task DeleteAllAsync(GetDiagnosisInput input);
    }
}
