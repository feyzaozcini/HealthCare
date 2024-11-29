using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessManager(ITestProcessRepository testProcessRepository) : DomainService
{
    public virtual async Task<TestProcess> CreateAsync(
       Guid labRequestId,
       Guid testGroupItemId,
       TestProcessStates status,
       decimal? result,
       DateTime? resultDate
       )
    {
        var testProcess = new TestProcess(
        GuidGenerator.Create(),
        labRequestId,
        testGroupItemId,
        status,
        result,
        resultDate
        );

        return await testProcessRepository.InsertAsync(testProcess);
    }

    public virtual async Task<TestProcess> UpdateAsync(
        Guid id,
        Guid labRequestId,
        Guid testGroupItemId,
        TestProcessStates status,
        decimal? result,
        DateTime? resultDate
        )
    {
        var testProcess = await testProcessRepository.GetAsync(id);

        testProcess.SetLabRequestId(labRequestId);
        testProcess.SetTestGroupItemId(testGroupItemId);
        testProcess.SetTestProcessStates(status);
        testProcess.SetResult(result);
        testProcess.SetResultDate(resultDate);

        return await testProcessRepository.UpdateAsync(testProcess);
    }
}
