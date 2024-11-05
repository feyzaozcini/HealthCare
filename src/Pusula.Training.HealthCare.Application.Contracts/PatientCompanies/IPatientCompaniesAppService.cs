using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public interface IPatientCompaniesAppService : IApplicationService
    {
        Task<PagedResultDto<PatientCompanyDto>> GetListAsync(GetPatientCompaniesInput input);

        Task<PatientCompanyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);  

        Task<PatientCompanyDto> CreateAsync(PatientCompanyCreateDto input);

        Task<PatientCompanyDto> UpdateAsync(PatientCompanyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientCompanyExcelDownloadDto input);

        Task DeleteByIdAsync(List<Guid> patientCompanyIds);
        Task DeleteAllAsync(GetPatientCompaniesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
