using Pusula.Training.HealthCare.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Cities
{
    public class CityCreateDto
    {
        [Required]
        public Guid CountryId { get; set; }

        [Required]
        [StringLength(CityConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
