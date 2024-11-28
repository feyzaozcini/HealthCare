using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessDto : AuditedEntityDto<Guid>
{
    [Required]
    public Guid LabRequestId { get; set; }
    [Required]
    public Guid TestGroupItemId { get; set; }
    [Required]
    public TestProcessStates Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }
}
