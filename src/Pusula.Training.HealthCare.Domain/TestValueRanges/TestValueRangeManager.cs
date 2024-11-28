using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class TestValueRangeManager(ITestValueRangeRepository testValueRangeRepository) : DomainService
{
    public virtual async Task<TestValueRange> CreateAsync(
       Guid testGroupItemId,
       decimal minValue,
       decimal maxValue,
       TestUnitTypes unit
       )
    {
        var testValueRange = new TestValueRange(
        GuidGenerator.Create(),
        testGroupItemId,
        minValue,
        maxValue,
        unit
        );

        return await testValueRangeRepository.InsertAsync(testValueRange);
    }

    public virtual async Task<TestValueRange> UpdateAsync(
        Guid id,
        Guid testGroupItemId,
        decimal minValue,
        decimal maxValue,
       TestUnitTypes unit
        )
    {
        var testValueRange = await testValueRangeRepository.GetAsync(id);

        testValueRange.SetTestGroupItemId(testGroupItemId);
        testValueRange.SetMinValue(minValue);
        testValueRange.SetMaxValue(maxValue);
        testValueRange.SetUnitType(unit);

        return await testValueRangeRepository.UpdateAsync(testValueRange);
    }
}
