﻿using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestValueRanges;

public interface ITestValueRangeRepository : IRepository<TestValueRange, Guid>
{
    Task<List<TestValueRange>> GetListAsync(
                string? filterText = null,
                Guid? testGroupItemId = null,
                decimal? minValue = null,
                decimal? maxValue = null,
                TestUnitTypes? unit = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

    Task<long> GetCountAsync(
                string? filterText = null,
                Guid? testGroupItemId = null,
                decimal? minValue = null,
                decimal? maxValue = null,
                TestUnitTypes? unit = null,
                CancellationToken cancellationToken = default);

    Task<TestValueRange> GetWithNavigationPropertiesAsync(
                Guid id,
                CancellationToken cancellationToken = default);

    Task<List<TestValueRange>> GetListWithNavigationPropertiesAsync(
                string? filterText = null,
                Guid? testGroupItemId = null,
                decimal? minValue = null,
                decimal? maxValue = null,
                TestUnitTypes? unit = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
   );
}
