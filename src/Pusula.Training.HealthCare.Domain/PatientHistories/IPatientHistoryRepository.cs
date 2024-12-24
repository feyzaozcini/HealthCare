using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public interface IPatientHistoryRepository : IRepository<PatientHistory, Guid>
    {
        Task<PatientHistory> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    }
}
