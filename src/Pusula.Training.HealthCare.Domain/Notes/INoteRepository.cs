using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Notes
{
    public interface INoteRepository : IRepository<Note, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? text = null,
            CancellationToken cancellationToken = default);
        Task<List<Note>> GetListAsync(
                    string? filterText = null,
                    string? text = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? text = null,
            CancellationToken cancellationToken = default);
    }
}
