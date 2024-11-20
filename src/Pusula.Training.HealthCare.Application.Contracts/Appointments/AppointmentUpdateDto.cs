using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentUpdateDto
    {
        [Required] 
        public Guid Id { get; set; } = default!;
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

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
