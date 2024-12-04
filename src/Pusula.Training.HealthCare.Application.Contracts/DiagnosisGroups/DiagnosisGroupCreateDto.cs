using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupCreateDto
    {
        [Required(ErrorMessage = DiagnosisGroupConsts.NameRequired)]
        [StringLength(DiagnosisGroupConsts.NameMaxLength, ErrorMessage = DiagnosisGroupConsts.NameLength, MinimumLength = DiagnosisGroupConsts.NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DiagnosisGroupConsts.CodeMaxLength)]
        public string Code { get; set; } = null!;
    }
}
