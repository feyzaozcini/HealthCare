using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public interface IExaminationDiagnosisAppService : IApplicationService
    {
        Task<PagedResultDto<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListAsync(GetExaminationDiagnosisInput input);

        Task<List<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListNotPagedAsync(GetExaminationDiagnosisInput input);
        Task<ExaminationDiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ExaminationDiagnosisDto> GetAsync(Guid id);

        //Task<ExaminationDiagnosisDto> GetWithProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task DeleteByDiagnosisIdAsync(Guid id);


        Task<ExaminationDiagnosisDto> CreateAsync(ExaminationDiagnosisCreateDto input);

        Task<ExaminationDiagnosisDto> UpdateAsync(ExaminationDiagnosisUpdateDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetDiagnosisLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<DiagnosisCountDto>> GetDiagnosisCountsAsync(GetExaminationDiagnosisInput input);
    }
}
