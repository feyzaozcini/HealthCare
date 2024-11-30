using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using System;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestWithNavigationProperties
{
    public Guid Id { get; set; }
    public Guid ProtocolId { get; set; }
    public string ProtocolType { get; set; }
    public DateTime? ProtocolStartDate { get; set; }
    public string? ProtocolEndDate { get; set; }

    public Guid DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? DoctorSurname { get; set; }

    public DateTime Date { get; set; }
    public RequestStatusEnum Status { get; set; }
    public string? Description { get; set; }
}
