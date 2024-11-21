using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisCreateDto
    {
        [Required]
        [StringLength(DiagnosisConsts.CodeMaxLength)]
        public virtual string Code { get; set; } = null!;

        [Required]
        [StringLength(DiagnosisConsts.NameMaxLength)]
        public virtual string Name { get; set; } = null!;

        public Guid GroupId { get; set; }
    }
}
