using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleCreateDto
    {
        public Guid? DoctorId { get; set; } = Guid.Empty;

        public Guid? DepartmentId { get; set; }

        public Gender Gender { get; set; } 

        public int Age { get; set; } 

        public string Description { get; set; } = string.Empty;
    }
}
