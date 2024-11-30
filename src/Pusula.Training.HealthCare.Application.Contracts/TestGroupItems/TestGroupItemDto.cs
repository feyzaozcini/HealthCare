﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemDto : AuditedEntityDto<Guid>
{
    [Required]
    public Guid TestGroupId { get; set; }

    public string TestGroupName { get; set; } = null!;
    [Required]
    public string Name { get; private set; } = null!;

    [Required]
    public string Code { get; private set; } = null!;

    [Required]
    public string TestType { get; private set; } = null!; 
    public string? Description { get; private set; }
    [Required]
    public int TurnaroundTime { get; set; }
}
