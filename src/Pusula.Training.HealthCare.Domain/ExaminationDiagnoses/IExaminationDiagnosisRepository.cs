using Pusula.Training.HealthCare.Diagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public interface IExaminationDiagnosisRepository : IRepository<ExaminationDiagnosis, Guid>
    {
        Task<ExaminationDiagnosisWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default);

        Task<List<ExaminationDiagnosisWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        DateTime? initialDate = null,
        string? note = null,
        Guid? protocolId = null,
        Guid? diagnosisId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

        Task<List<ExaminationDiagnosis>> GetListAsync(
        string? filterText = null,
        DateTime? initialDate = null,
        string? note = null,
        Guid? protocolId = null,
        Guid? diagnosisId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

        Task DeleteAllAsync(
           string? filterText = null,
           DateTime? initialDate = null,
           string? note = null,
           Guid? protocolId = null,
           Guid? diagnosisId = null,
           CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
          string? filterText = null,
          DateTime? initialDate = null,
          string? note = null,
          Guid? protocolId = null,
          Guid? diagnosisId = null,
          string? sorting = null,
          int maxResultCount = int.MaxValue,
          int skipCount = 0,
          CancellationToken cancellationToken = default);

        Task<List<(string DiagnosisName, int Count)>> GetDiagnosisCountsAsync(
        int skipCount,
        int maxResultCount,
        CancellationToken cancellationToken = default);
    }
}
