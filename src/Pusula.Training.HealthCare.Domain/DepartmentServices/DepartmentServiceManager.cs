using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using Volo.Abp.Data;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class DepartmentServiceManager(IDepartmentServiceRepository departmentServiceRepository) : DomainService
    {
        public virtual async Task<DepartmentService> CreateAsync(
        string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DepartmentServiceConsts.NameMaxLength);

            var departmentService = new DepartmentService(
             GuidGenerator.Create(),
             name
             );

            return await departmentServiceRepository.InsertAsync(departmentService);
        }

        public virtual async Task<DepartmentService> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DepartmentServiceConsts.NameMaxLength);

            var departmentService = await departmentServiceRepository.GetAsync(id);

            departmentService.Name = name;

            departmentService.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await departmentServiceRepository.UpdateAsync(departmentService);
        }

    }
}
