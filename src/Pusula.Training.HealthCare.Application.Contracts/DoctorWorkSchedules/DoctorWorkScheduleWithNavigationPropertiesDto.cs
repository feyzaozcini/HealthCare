using Pusula.Training.HealthCare.Doctors;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleWithNavigationPropertiesDto
    {
        public DoctorWorkScheduleDto DoctorWorkSchedule { get; set; } = null!;
        public DoctorDto Doctor { get; set; } = null!;
    }
}
