using Pusula.Training.HealthCare.LabRequests;
using System;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Blazor.Containers.Abstracts;

public interface ILabRequestState : IScopedDependency
{
    LabRequestDto? SelectedLabRequest { get; set; }
    event Action? OnStateChanged;

    void SetSelectedLabRequest(LabRequestDto? labRequest);
    void NotifyStateChanged();
}
