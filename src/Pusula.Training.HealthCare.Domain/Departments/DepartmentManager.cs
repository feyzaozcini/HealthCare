using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentManager(IDepartmentRepository departmentRepository) : DomainService
{
    public virtual async Task<Department> CreateAsync(
    string name,
    List<Guid> doctorIds)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength);

        var department = new Department(
            GuidGenerator.Create(),
            name
        );

        await SetDoctors(department, doctorIds);

        return await departmentRepository.InsertAsync(department);
    }


    public virtual async Task<Department> UpdateAsync(
        Guid id,
        string name, [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength);

        var department = await departmentRepository.GetAsync(id);

        department.Name = name;

        department.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await departmentRepository.UpdateAsync(department);
    }

    public virtual async Task SetDoctors(
    Department department,
    List<Guid> doctorIds
)
    {
        department.Doctors.Clear();

        foreach (var doctorId in doctorIds)
        {
            department.AddDoctor(doctorId);
        }
    }

}