using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleWithNavigationProperties
    {
        public AppointmentRule AppointmentRule { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
