using Pusula.Training.HealthCare.TestProcesses;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Blazor.Containers.Abstracts;

public interface ITestProcessState : IScopedDependency
{
    List<TestProcessWithNavigationPropertiesDto> TestProcesses { get; }
    void SetTestProcesses(List<TestProcessWithNavigationPropertiesDto> processes);
    void AddOrUpdateTestProcess(TestProcessWithNavigationPropertiesDto newProcess);
}
