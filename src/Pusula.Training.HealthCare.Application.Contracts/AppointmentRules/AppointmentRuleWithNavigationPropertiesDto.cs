using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleWithNavigationPropertiesDto
    {
        public AppointmentRuleDto AppointmentRule { get; set; } = null!;
        public DoctorDto Doctor { get; set; } = null!;
        public DepartmentDto Department { get; set; } = null!;
    }
}
