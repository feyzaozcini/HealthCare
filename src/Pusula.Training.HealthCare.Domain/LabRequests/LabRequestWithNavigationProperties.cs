using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using System;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestWithNavigationProperties
{
    public LabRequest? LabRequest { get; set; }
    public Protocol? Protocol { get; private set; }
    public Doctor? Doctor { get; private set; }
}