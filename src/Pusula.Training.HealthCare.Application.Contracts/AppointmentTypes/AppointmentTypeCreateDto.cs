using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeCreateDto
    {
        [Required]
        [StringLength(AppointmentTypeConst.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int DurationInMinutes { get; set; }

        public List<Guid> DoctorIds { get; set; } = new List<Guid>();
    }
}
