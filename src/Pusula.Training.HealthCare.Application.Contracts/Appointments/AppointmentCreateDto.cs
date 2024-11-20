using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentCreateDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "StartTime must be in the format hh:mm:ss")]
        public string StartTime { get; set; } // TimeSpan yerine string

        [Required]
        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$", ErrorMessage = "EndTime must be in the format hh:mm:ss")]
        public string EndTime { get; set; } // TimeSpan yerine string

        [StringLength(AppointmentConst.NoteMaxLength)]
        public string Note { get; set; } = null!;

        [Required]
        public AppointmentStatus AppointmentStatus { get; set; }

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AppointmentTypeId { get; set; }
    }
}
