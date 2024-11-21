using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupCreateDto
    {
        [Required]
        [StringLength(DiagnosisGroupConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        //[StringLength(DiagnosisGroupConsts.CodeMaxLength)] ADIM1
        public string Code { get; set; } = null!;
    }
}
