using Pusula.Training.HealthCare.Blazor.Containers.Abstracts;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

public class TestProcessStateContainer(ITestProcessState _state, ITestProcessNotifier _notifier) : IScopedDependency
{
    public List<TestProcessWithNavigationPropertiesDto> TestProcesses => _state.TestProcesses;

    public bool Refresh => _notifier.Refresh;

    public void SetTestProcesses(List<TestProcessWithNavigationPropertiesDto> processes)
    {
        _state.SetTestProcesses(processes);
        _notifier.NotifyStateChanged();
    }

    public void AddOrUpdateTestProcess(TestProcessWithNavigationPropertiesDto newProcess)
    {
        _state.AddOrUpdateTestProcess(newProcess);
        _notifier.NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        _notifier.NotifyStateChanged();
    }

    public void Subscribe(Action listener)
    {
        _notifier.OnStateChange += listener;
    }

    public void Unsubscribe(Action listener)
    {
        _notifier.OnStateChange -= listener;
    }
}



