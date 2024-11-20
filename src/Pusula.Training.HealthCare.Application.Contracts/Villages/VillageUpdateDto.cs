using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Villages
{
    public class VillageUpdateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
