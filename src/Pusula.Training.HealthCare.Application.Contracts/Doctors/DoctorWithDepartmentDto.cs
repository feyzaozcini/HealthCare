using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorWithDepartmentDto : Entity<Guid>
    {
        public string Name { get; set; } = null!;
        public List<DepartmentDto> Departments { get; set; } = new();
    }
}
