using System;

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
