using JetBrains.Annotations;
using Pusula.Training.HealthCare.DoctorDepartments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Departments;

public class Department : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    public virtual ICollection<DoctorDepartment> Doctors { get; set; }

    protected Department()
    {
        Name = string.Empty;
        Doctors = new Collection<DoctorDepartment>();
    }

    public Department(Guid id, string name)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength, 0);
        Name = name;
        Doctors = new Collection<DoctorDepartment>();
    }

    public void AddDoctor(Guid doctorId)
    {
        if (Doctors.Any(dd => dd.DoctorId == doctorId))
        {
            return;
        }

        Doctors.Add(new DoctorDepartment(doctorId,Id));
    }

    private bool IsInDoctor(Guid doctorId) 
    { 
        return Doctors.Any(dd => dd.DoctorId == doctorId); 
    }

}