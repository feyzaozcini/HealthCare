using Pusula.Training.HealthCare.Blazor.Containers.Abstracts;
using Pusula.Training.HealthCare.LabRequests;
using System;

namespace Pusula.Training.HealthCare.Blazor.Containers.Concretes;

public class LabRequestStateContainer : ILabRequestState
{
    public LabRequestDto? SelectedLabRequest { get; set; }

    public event Action? OnStateChanged;

    public void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }

    public void SetSelectedLabRequest(LabRequestDto? labRequest)
    {
        SelectedLabRequest = labRequest;
        NotifyStateChanged();
    }
}
    