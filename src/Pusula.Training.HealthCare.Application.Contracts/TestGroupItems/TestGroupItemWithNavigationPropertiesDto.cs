using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.TestValueRanges;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemWithNavigationPropertiesDto
{
    public TestProcessDto? TestProcess { get; set; }
    public TestGroupItemDto TestGroupItem { get; set; } = null!;
    public TestGroupDto TestGroup { get; set; } = null!;
    public TestValueRangeDto TestValueRange { get; set; } = null!;
}
