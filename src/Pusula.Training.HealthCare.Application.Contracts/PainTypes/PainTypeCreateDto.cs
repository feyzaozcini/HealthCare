using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PainTypes
{
    public class PainTypeCreateDto
    {
        [Required]
        [StringLength(PainTypeConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
