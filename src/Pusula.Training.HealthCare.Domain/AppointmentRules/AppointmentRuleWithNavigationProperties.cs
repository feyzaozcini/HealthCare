using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleWithNavigationProperties
    {
        public AppointmentRule AppointmentRule { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public Department Department { get; set; } = null!;
    }
}
