using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Insurances
{
    public interface IInsurancesAppService : IApplicationService
    {
        Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input);
        Task<InsuranceDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<InsuranceDto> CreateAsync(InsuranceCreateDto input);
        Task<InsuranceDto> UpdateAsync(InsuranceUpdateDto input);
        Task DeleteByIdsAsync(List<Guid> insuranceIds);
        Task DeleteAllAsync(GetInsurancesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}