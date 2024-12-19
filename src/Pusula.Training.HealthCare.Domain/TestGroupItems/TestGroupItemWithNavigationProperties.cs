using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.TestValueRanges;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemWithNavigationProperties
{
    public TestProcess? TestProcess { get; set; }
    public TestGroupItem? TestGroupItem { get; set; }
    public TestGroup? TestGroup { get; set; }
    public TestValueRange? TestValueRange { get; set; }

}
