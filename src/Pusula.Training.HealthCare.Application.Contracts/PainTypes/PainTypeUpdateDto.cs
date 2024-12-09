using Pusula.Training.HealthCare.Notes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PainTypes
{
    public class PainTypeUpdateDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(PainTypeConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

    }
}
