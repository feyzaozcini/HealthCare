using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceCreateDto
    {
        [Required]
        [StringLength(InsuranceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
