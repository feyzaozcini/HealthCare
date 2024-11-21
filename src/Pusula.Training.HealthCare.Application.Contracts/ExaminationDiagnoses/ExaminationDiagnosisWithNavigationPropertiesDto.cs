using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisWithNavigationPropertiesDto
    {
        public ExaminationDiagnosisDto ExaminationDiagnosis { get; set; } = null!;

        public ProtocolDto Protocol { get; set; } = null!;

        public DiagnosisDto Diagnosis { get; set; } = null!;


    }
}
