using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class TestValueRange : AuditedEntity<Guid>
{
    [NotNull]
    public Guid TestGroupItemId { get; private set; }
    public TestGroupItem TestGroupItem { get; private set; }

    [NotNull]
    public decimal MinValue { get; private set; }

    [NotNull]
    public decimal MaxValue { get; private set; }

    [NotNull]
    public TestUnitTypes Unit { get; private set; }

    public TestValueRange()
    {

    }

    public TestValueRange(Guid id, Guid testGroupItemId, decimal minValue, decimal maxValue, TestUnitTypes unit)
    {
        Id = id;
        SetTestGroupItemId(testGroupItemId);
        SetMinValue(minValue);
        SetMaxValue(maxValue);
        SetUnitType(unit);
    }

    public void SetTestGroupItemId(Guid testGroupItemId) => TestGroupItemId = Check.NotNull(testGroupItemId, nameof(testGroupItemId));
    public void SetMinValue(decimal minValue) => MinValue = Check.NotNull(minValue, nameof(minValue));
    public void SetMaxValue(decimal maxValue) => MaxValue = Check.NotNull(maxValue, nameof(maxValue));
    public void SetUnitType(TestUnitTypes status) => Unit = Enum.IsDefined(typeof(TestUnitTypes), status) ? status : TestUnitTypes.ArbitraryUnit;
}
