using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestDto : AuditedEntityDto<Guid>
{
    public Guid ProtocolId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid TestGroupItemId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public RequestStatusEnum Status { get; set; }
}
