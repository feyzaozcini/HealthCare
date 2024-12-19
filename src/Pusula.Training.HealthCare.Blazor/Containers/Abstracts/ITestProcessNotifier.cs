using System;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Blazor.Containers.Abstracts;

public interface ITestProcessNotifier : IScopedDependency
{
    event Action? OnStateChange;
    bool Refresh { get; }
    void NotifyStateChanged();
}
