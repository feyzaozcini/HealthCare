using Pusula.Training.HealthCare.TestGroups;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemWithNavigationProperties
{
    public TestGroupItem TestGroupItem { get; set; } = null!;
    public TestGroup TestGroup { get; set; } = null!;
}
