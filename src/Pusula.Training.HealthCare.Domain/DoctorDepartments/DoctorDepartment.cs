using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DoctorDepartments
{
    public class DoctorDepartment : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }

        public Doctor Doctor { get; set; }
        public Department Department { get; set; }

        private DoctorDepartment()
        {
        }

        public DoctorDepartment(Guid doctorId, Guid departmentId)
        {
            DoctorId = doctorId;
            DepartmentId = departmentId;
        }

        public override object?[] GetKeys()
        {
            return new object?[] { DoctorId, DepartmentId };
        }
    }
}
