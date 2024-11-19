using JetBrains.Annotations;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Departments;

public class Department : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }

    protected Department()
    {
        Name = string.Empty;
        DoctorDepartments = new List<DoctorDepartment>();
    }

    public Department(Guid id, string name)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength, 0);
        Name = name;
        DoctorDepartments = new List<DoctorDepartment>();
    }

}