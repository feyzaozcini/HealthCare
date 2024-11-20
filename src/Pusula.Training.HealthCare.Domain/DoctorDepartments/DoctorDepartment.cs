using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DoctorDepartments
{
    public class DoctorDepartment : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }

        public Doctor Doctor { get; set; }
        public Department Department { get; set; }

        public override object?[] GetKeys()
        {
            return new object?[] { DoctorId, DepartmentId };
        }
    }
}
