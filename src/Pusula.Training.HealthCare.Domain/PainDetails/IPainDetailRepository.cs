using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PainDetails
{
    public interface IPainDetailRepository : IRepository<PainDetail,Guid>
    {
        Task<PainDetail> GetWithNavigationPropertiesAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<PainDetail> GetWithNavigationPropertiesByProtocolIdAsync(Guid protocolId,
            CancellationToken cancellationToken = default);

    }
}
