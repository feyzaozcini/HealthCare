using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestValueRanges;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessDto : FullAuditedEntityDto<Guid>
{
    public Guid LabRequestId { get; set; }
    public Guid TestGroupItemId { get; set; }
    public TestProcessStates Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }
}
