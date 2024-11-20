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
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; } 

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
