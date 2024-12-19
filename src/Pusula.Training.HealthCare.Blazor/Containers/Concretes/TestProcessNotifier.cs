using Pusula.Training.HealthCare.Blazor.Containers.Abstracts;
using System;

namespace Pusula.Training.HealthCare.Blazor.Containers.Concretes;

public class TestProcessNotifier : ITestProcessNotifier
{
    public event Action? OnStateChange;
    private bool _refresh;

    public bool Refresh
    {
        get => _refresh;
        private set => _refresh = value;
    }

    public void NotifyStateChanged()
    {
        Refresh = true; 
        OnStateChange?.Invoke();
        Refresh = false; 
    }
}
