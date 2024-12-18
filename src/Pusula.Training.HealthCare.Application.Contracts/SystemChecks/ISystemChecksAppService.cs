using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public interface ISystemChecksAppService : IApplicationService
    {
        Task<SystemCheckDto> GetAsync(Guid id);

        Task<SystemCheckDto> GetByProtocolIdAsync(Guid protocolId);

        Task DeleteAsync(Guid id);
        Task<SystemCheckDto> CreateAsync(SystemCheckCreateDto input);
        Task<SystemCheckDto> UpdateAsync(SystemCheckUpdateDto input);
    }
}
