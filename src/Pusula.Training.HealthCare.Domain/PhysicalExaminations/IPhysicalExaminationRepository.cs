using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public interface IPhysicalExaminationRepository : IRepository<PhysicalExamination, Guid>
    {
        Task<PhysicalExaminationWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<PhysicalExaminationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            decimal? weight = null,
            decimal? height = null,
            Guid? protocolId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
            );

        Task<List<PhysicalExamination>> GetListAsync(
            string? filterText = null,
            decimal? weight = null,
            decimal? height = null,
            Guid? protocolId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            decimal? weight = null,
            decimal? height = null,
            Guid? protocolId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

    }
}
