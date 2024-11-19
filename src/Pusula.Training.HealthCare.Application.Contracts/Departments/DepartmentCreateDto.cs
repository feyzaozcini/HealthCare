using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentCreateDto
{
    [Required]
    [StringLength(DepartmentConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public List<Guid> DoctorIds { get; set; }
}