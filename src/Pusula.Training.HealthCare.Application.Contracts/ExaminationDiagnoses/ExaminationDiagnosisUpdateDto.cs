using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisUpdateDto
    {
        public Guid Id { get; set; } = default!;
        public DiagnosisType DiagnosisType { get; set; }
        public DateTime InitialDate { get; set; }
        public string Note { get; set; } = null!;
        public Guid ProtocolId { get; set; }

        public Guid DiagnosisId { get; set; }
    }
}
