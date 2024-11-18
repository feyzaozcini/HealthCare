using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestUpdateDto
{
    public Guid Id { get; set; }

    [Required]
    public Guid ProtocolId { get; set; }
    [Required]
    public Guid DoctorId { get; set; }
    [Required]
    public Guid TestGroupItemId { get; set; }
    [Required]
    [StringLength(LabRequestConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required]
    public RequestStatusEnum Status { get; set; }

}
