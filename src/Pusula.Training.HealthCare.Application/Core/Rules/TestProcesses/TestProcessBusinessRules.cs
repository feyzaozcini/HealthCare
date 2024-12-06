using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.TestProcesses;

public class TestProcessBusinessRules(ITestProcessRepository testProcessRepository) : ITestProcessBusinessRules
{
    public async Task ValidateRecentTestsAsync(Guid labRequestId, Guid testGroupItemId)
    {
        var hasRecentTests = await testProcessRepository.AnyAsync(tp =>
      tp.LabRequestId == labRequestId &&
      tp.TestGroupItemId == testGroupItemId &&
      tp.CreationTime >= DateTime.UtcNow.AddHours(-24));

        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.CannotAddTheSameTestOnTheSameDay,
            hasRecentTests
        );

    }
}
