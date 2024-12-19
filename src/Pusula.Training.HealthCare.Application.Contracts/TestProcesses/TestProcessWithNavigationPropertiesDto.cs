using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestValueRanges;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessWithNavigationPropertiesDto
{
    public TestProcessDto? TestProcess { get; set; }
    public LabRequestDto? LabRequest { get; set; }
    public TestGroupItemDto? TestGroupItem { get; set; }
    public TestValueRangeDto? TestValueRange { get; set; }
}
