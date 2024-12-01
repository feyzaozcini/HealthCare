using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class FlatAppointmentDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string DepartmentId { get; set; }
        public Guid AppointmentTypeId { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
