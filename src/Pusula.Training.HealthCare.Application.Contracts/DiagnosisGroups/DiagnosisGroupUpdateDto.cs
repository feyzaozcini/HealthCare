using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupUpdateDto
    {
        [Required]
        public Guid Id { get; set; } = default!;

        [Required]
        [StringLength(DiagnosisGroupConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DiagnosisGroupConsts.CodeMaxLength)]
        public string Code { get; set; } = null!;
    }
}
