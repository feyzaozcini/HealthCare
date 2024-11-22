﻿using Pusula.Training.HealthCare.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemsCreateDto
{
    public Guid TestGroupId { get; set; }

    [Required]
    [StringLength(TestGroupItemConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(TestGroupItemConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;
    [Required]
    public string TestType { get; set; } = null!;
    [StringLength(TestGroupItemConsts.DescriptionMaxLength)]
    public string? Description { get; set; } 
    public int? TurnaroundTime { get; set; }
}
