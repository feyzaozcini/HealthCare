using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Districts
{
    public class DistrictCreateDto
    {
        [Required]
        public Guid CityId { get; set; }

        [Required]
        [StringLength(DistrictConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
