using System;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeDeleteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Message { get; set; }
    }
}
