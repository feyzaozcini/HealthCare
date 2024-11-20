using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Districts
{
    public class DistrictUpdateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
