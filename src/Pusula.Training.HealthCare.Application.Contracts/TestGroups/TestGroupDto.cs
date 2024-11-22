using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupDto : AuditedEntityDto<Guid>
{
    [Required]
    [StringLength(TestGroupConsts.NameMaxLength, MinimumLength = TestGroupConsts.NameMinLength)]
    public string Name { get; set; } = null!;
}