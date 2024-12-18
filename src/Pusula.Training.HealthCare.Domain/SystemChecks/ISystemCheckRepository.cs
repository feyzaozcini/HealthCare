using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public interface ISystemCheckRepository : IRepository<SystemCheck, Guid>
    {
        Task<SystemCheck> GetWithNavigationPropertiesAsync(Guid id,CancellationToken cancellationToken = default);

        Task<SystemCheck> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default);

        Task<SystemCheck> GetByProtocolIdAsync(Guid protocolId, CancellationToken cancellationToken = default);
    }
}
