using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleDto : Entity<Guid>
    {
        public Guid? DoctorId { get; set; }

        public Guid? DepartmentId { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
