using Pusula.Training.HealthCare.Blazor.Containers.Abstracts;
using Pusula.Training.HealthCare.TestProcesses;
using System.Collections.Generic;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Containers.Concretes;

public class TestProcessState : ITestProcessState
{
    private List<TestProcessWithNavigationPropertiesDto> _testProcesses = new();

    public List<TestProcessWithNavigationPropertiesDto> TestProcesses => _testProcesses;

    public void SetTestProcesses(List<TestProcessWithNavigationPropertiesDto> processes)
    {
        _testProcesses = processes;
    }

    public void AddOrUpdateTestProcess(TestProcessWithNavigationPropertiesDto newProcess)
    {
        var existingProcess = _testProcesses.FirstOrDefault(tp => tp.TestProcess!.Id == newProcess.TestProcess!.Id);

        if (existingProcess != null)
        {
            existingProcess.TestProcess = newProcess.TestProcess;
            existingProcess.LabRequest = newProcess.LabRequest ?? existingProcess.LabRequest;
        }
        else
        {
            _testProcesses.Add(newProcess);
        }
    }
}
