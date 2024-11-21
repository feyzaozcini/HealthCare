using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public interface IDiagnosisRepository : IRepository<Diagnosis, Guid>
    {

        Task<DiagnosisWithNavigationProperties> GetWithNavigationPropertiesAsync(
          Guid id,
          CancellationToken cancellationToken = default);
        Task<List<DiagnosisWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
          string? filterText = null,
          string? code = null,
          string? name = null,
          Guid? groupId = null,
          string? sorting = null,
          int maxResultCount = int.MaxValue,
          int skipCount = 0,
          CancellationToken cancellationToken = default);

        Task<List<Diagnosis>> GetListAsync(
          string? filterText = null,
          string? name = null,
          string? code = null,
          Guid? groupId = null,
          string? sorting = null,
          int maxResultCount = int.MaxValue,
          int skipCount = 0,
          CancellationToken cancellationToken = default);

        Task DeleteAllAsync(
           string? filterText = null,
           string? name = null,
           string? code = null,
           Guid? groupId = null,
           CancellationToken cancellationToken = default);

      
        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            Guid? groupId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);
    }
}
