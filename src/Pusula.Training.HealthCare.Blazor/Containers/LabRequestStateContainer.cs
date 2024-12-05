using Pusula.Training.HealthCare.LabRequests;
using System;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Blazor.Containers;

public class LabRequestStateContainer : IScopedDependency
{
    public LabRequestDto? SelectedLabRequest { get; set; }

}
