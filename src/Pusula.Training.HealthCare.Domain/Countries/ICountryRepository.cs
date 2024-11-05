using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Countries;

public interface ICountryRepository : IRepository<Country, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        string? code = null,
        CancellationToken cancellationToken = default);

    Task<List<Country>> GetListAsync(
                string? filterText = null,
                string? name = null,
                string? code = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        string? code = null,
        CancellationToken cancellationToken = default);
}
