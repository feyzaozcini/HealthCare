using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisCreateDto
    {
        [Required(ErrorMessage = "Tani tipi alanı zorunludur.")]

        public DiagnosisType DiagnosisType { get; set; }

        [Required(ErrorMessage = "Tani tarihi alanı zorunludur.")]

        public DateTime InitialDate { get; set; }

        [Required(ErrorMessage = "Tani notu alanı zorunludur.")]

        public string Note { get; set; } = null!;
        public Guid ProtocolId { get; set; }

        [Required(ErrorMessage = "Tani alani bos birakilamaz.")]

        public Guid DiagnosisId { get; set; }
    }
}
