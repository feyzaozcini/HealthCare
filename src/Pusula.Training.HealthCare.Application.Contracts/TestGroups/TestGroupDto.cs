using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
}