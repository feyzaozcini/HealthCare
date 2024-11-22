using Pusula.Training.HealthCare.Anamneses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public interface IPshychologicalStateRepository : IRepository<PshychologicalState, Guid>
    {
        Task<PshychologicalStateWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    }
}
