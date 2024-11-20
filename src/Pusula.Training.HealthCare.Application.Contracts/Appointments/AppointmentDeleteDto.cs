using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentDeleteDto
    {
        public Guid Id { get; set; } 
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string? message { get; set; }
    }
}
