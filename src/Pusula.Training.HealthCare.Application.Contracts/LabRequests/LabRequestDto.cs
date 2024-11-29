using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestDto : AuditedEntityDto<Guid>
{
    [Required]
    public Guid ProtocolId { get; set; }
    [Required]
    public Guid DoctorId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public RequestStatusEnum Status { get; set; }
    [StringLength(LabRequestConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    public LabRequestDto()
    {
    }
}
