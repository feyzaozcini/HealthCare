using System;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentEmailDto
    {
        public string PatientName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
