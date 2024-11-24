using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public List<Guid> DoctorDepartments { get; set; } = new();
}