using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleWithNavigationPropertiesDto
    {
        public DoctorWorkScheduleDto DoctorWorkSchedule { get; set; } = null!;
        public DoctorDto Doctor { get; set; } = null!;
    }
}
