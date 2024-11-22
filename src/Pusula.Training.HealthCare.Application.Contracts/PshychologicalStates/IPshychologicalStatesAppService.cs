using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public interface IPshychologicalStatesAppService : IApplicationService
    {
        Task<PshychologicalStateWithNavigationDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PshychologicalStateDto> GetAsync(Guid id);

        Task<PshychologicalStateDto> GetWithProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);

        Task<PshychologicalStateDto> CreateAsync(PshychologicalStateCreateDto input);

        Task<PshychologicalStateDto> UpdateAsync(PshychologicalStateUpdateDto input);

    }
}
