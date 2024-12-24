using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public interface IFamilyHistoryRepository : IRepository<FamilyHistory,Guid>
    {
        Task<FamilyHistory> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    }
}
