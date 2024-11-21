using Pusula.Training.HealthCare.PshychologicalStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.FallRisks
{
    public interface IFallRisksAppService : IApplicationService
    {
        Task<FallRiskWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<FallRiskDto> GetAsync(Guid id);

        Task<FallRiskDto> GetWithProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task<FallRiskDto> CreateAsync(FallRiskCreateDto input);

        Task<FallRiskDto> UpdateAsync(FallRiskUpdateDto input);


    }
}
