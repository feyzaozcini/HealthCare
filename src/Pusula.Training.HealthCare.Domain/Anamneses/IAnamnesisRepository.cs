using Pusula.Training.HealthCare.Diagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Anamneses
{
    public interface IAnamnesisRepository : IRepository<Anamnesis,Guid>
    {
        Task<AnamnesisWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default);

        Task<List<AnamnesisWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
          string? filterText = null,
          string? complaint = null,
          DateTime? startDate = null,
          string? story = null,
          Guid? protocolId = null,
          string? sorting = null,
          int maxResultCount = int.MaxValue,
          int skipCount = 0,
          CancellationToken cancellationToken = default);


        Task<List<Anamnesis>> GetListAsync(
         string? filterText = null,
         string? complaint = null,
         DateTime? startDate = null,
         string? story = null,
         Guid? protocolId = null,
         string? sorting = null,
         int maxResultCount = int.MaxValue,
         int skipCount = 0,
         CancellationToken cancellationToken = default);

        Task DeleteAllAsync(
          string? filterText = null,
          string? complaint = null,
          DateTime? startDate = null,
          string? story = null,
          Guid? protocolId = null,
          CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
           string? filterText = null,
           string? complaint = null,
           DateTime? startDate = null,
           string? story = null,
           Guid? protocolId = null,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default);

    }
}
