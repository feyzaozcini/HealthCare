using Pusula.Training.HealthCare.Doctors;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleWithNavigationProperties
    {
        public DoctorWorkSchedule DoctorWorkSchedule { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;
    }
}
