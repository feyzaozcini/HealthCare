using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.BlackLists
{
    public interface IBlackListRepository : IRepository<BlackList, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            CancellationToken cancellationToken = default);

        Task<BlackListWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<BlackListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
           CancellationToken cancellationToken = default
         );

        Task<List<BlackList>> GetListAsync(
            string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
            );

        Task<long> GetCountAsync(
            string? filterText = null,
            BlackListStatus? blackListStatus = null,
            string? note = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            CancellationToken cancellationToken = default);
    }
}
