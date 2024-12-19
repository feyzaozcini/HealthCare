using Pusula.Training.HealthCare.Core.Helpers;
using Pusula.Training.HealthCare.TestProcesses;

namespace Pusula.Training.HealthCare.Blazor.Models;

public class TestResultCalculateModel
{
    public string TextStyle { get; set; }
    public string TextClass { get; set; }
    public string Icon { get; set; }
    public string Text { get; set; }

    public TestResultCalculateModel(TestProcessWithNavigationPropertiesDto testProcess)
    {
        var resultStatus = TestResultCalculator.GetResultStatus(testProcess);
        TextStyle = resultStatus.TextStyle;
        TextClass = resultStatus.TextClass;
        Icon = resultStatus.Icon;
        Text = resultStatus.Text;
    }
}
