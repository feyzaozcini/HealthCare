using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleCreateDto
    {
        [Required]
        public virtual Guid DoctorId { get; set; }

        [Required]
        public virtual int[] WorkingDays { get; set; }

        [Required]
        public virtual string StartHour { get; set; }

        [Required]
        public virtual string EndHour { get; set; }

        public DoctorWorkScheduleCreateDto()
        {
            WorkingDays = new int[] {1,2,3,4,5 };
            StartHour = "09:00";
            EndHour = "18:00";
        }
    }
}
