using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeDeleteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Message { get; set; }
    }
}
