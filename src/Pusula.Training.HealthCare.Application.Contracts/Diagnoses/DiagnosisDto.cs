using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisDto : EntityDto<Guid>
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
