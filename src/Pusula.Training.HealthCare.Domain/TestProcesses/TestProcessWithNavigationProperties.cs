using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestValueRanges;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessWithNavigationProperties
{
    public TestProcess? TestProcess { get; set; }
    public LabRequest? LabRequest { get; set; }
    public TestGroupItem? TestGroupItem { get; set; }
    public TestValueRange? TestValueRange { get; set; }
}
